using AutoMapper;
using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.DTO.DtoOutput;
using Hrmanagement.Core.Misc;
using Hrmanagement.Core.Models;
using Hrmanagement.Data.DBContext;
using Hrmanagement.Data.Entities;
using Hrmanagement.Data.UnitOfWork;
using Hrmanagement.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Service
{
    public class UserMemoService : IUserMemoService
    {
        public IUnitOfWork _unitOfWork;
        private readonly HrManagementContext _context;
        public IMapper _mapper;
        //public IWebHostEnvironment _environment;
        //public ISystemSettingService _systemSettingService;
        public UserMemoService(IUnitOfWork unitOfWork, IMapper mapper, HrManagementContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }


        public async Task<ApiResponseModel<UserMemoOutput>> AddUpdateUserMemo(UserMemoInput model)

        {

            try
            {
                UserMemo userMemo = _mapper.Map<UserMemo>(model);
                if (userMemo.id != 0)
                {
                    userMemo = await _unitOfWork.UserMemo.GetByIdAsync(model.id);
                    userMemo.Title = model.Title;
                    userMemo.Description = model.Description;
                    userMemo.Url = model.Url;
                    userMemo.AvailableForPublic = model.AvailableForPublic;
                    userMemo.UpdatedAt = DateTime.UtcNow;

                }
                else
                {
                    userMemo.UserId = model.UserId;
                    userMemo.CreatedAt = DateTime.UtcNow;
                    userMemo.AvailableForPublic = model.AvailableForPublic;
                    await _unitOfWork.UserMemo.AddAsync(userMemo);
                }
                await _unitOfWork.SaveAsync();

                UserMemoOutput userMemoOutput = _mapper.Map<UserMemoOutput>(userMemo);
                return new ApiResponseModel<UserMemoOutput>
                {
                    succeed = true,
                    message = "Success",
                    data = userMemoOutput
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<ApiResponseModel<List<UserMemoOutput>>> getAllUserMemo(int userId)
        {
            try
            {
                List<UserMemo> userMemos;
                if (userId != 0)
                {
                    userMemos = _unitOfWork.UserMemo.GetWhere(x => x.UserId == userId).ToList(); 
                    if (userMemos == null || !userMemos.Any())
                    {
                        return new ApiResponseModel<List<UserMemoOutput>>
                        {
                            succeed = false,
                            message = "No user Memo found."
                        };
                    }
                }
                else
                {
                    userMemos = (await _unitOfWork.UserMemo.GetAllAsync()).ToList();
                }

                List<int> listUserId = userMemos.Select(x => x.UserId).Distinct().ToList();
                List<User> users = _unitOfWork.User.GetWhere(x => listUserId.Contains(x.Id)).ToList();

                var userMemoOutputs = from um in userMemos
                                      join u in users on um.UserId equals u.Id
                                      select new UserMemoOutput
                                      {
                                          id = um.id,
                                          UserId = u.Id,
                                          userName = u.FirstName,
                                          Title = um.Title,
                                          Description = um.Description,
                                          Url = um.Url,
                                          AvailableForPublic = um.AvailableForPublic,
                                          CreatedAt = um.CreatedAt,
                                          UpdatedAt = um.UpdatedAt
                                      };

                var result = _mapper.Map<List<UserMemoOutput>>(userMemoOutputs);
                return new ApiResponseModel<List<UserMemoOutput>>
                {
                    succeed = true,
                    message = "success",
                    data = result

                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponseModel<List<UserMemoOutput>>> getAllMemos()
        {
            List<UserMemo> userMemos = _unitOfWork.UserMemo.GetWhere(x => x.AvailableForPublic).ToList();
            List<int> listUserId = userMemos.Select(x => x.UserId).Distinct().ToList();
            List<User> users = _unitOfWork.User.GetWhere(x => listUserId.Contains(x.Id)).ToList();

            var userMemoOutputs = from um in userMemos
                                  join u in users on um.UserId equals u.Id
                                  select new UserMemoOutput
                                  {
                                      id = um.id,
                                      UserId = u.Id,
                                      userName = u.FirstName,
                                      Title = um.Title,
                                      Description = um.Description,
                                      Url = um.Url,
                                      AvailableForPublic = um.AvailableForPublic,
                                      CreatedAt = um.CreatedAt,
                                      UpdatedAt = um.UpdatedAt
                                  };

            var result = _mapper.Map<List<UserMemoOutput>>(userMemoOutputs);
            //var result = _mapper.Map<List<UserMemoOutput>>(userMemos);

            return new ApiResponseModel<List<UserMemoOutput>>
            {
                succeed = true,
                message = "success",
                data = result

            };

        }


        public async Task<ApiResponseModel<bool>> DeleteUserMemoById(int id)
        {
            try
            {
                UserMemo userMemo = await _unitOfWork.UserMemo.GetByIdAsync(id);
                if (userMemo == null)
                    throw new Exception("User Memo was not Found.");
                _unitOfWork.UserMemo.Remove(userMemo);
                await _unitOfWork.SaveAsync();
                return new ApiResponseModel<bool>
                {
                    succeed = true,
                    message = "User Memo Delete Successfully"
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponseModel<UserMemoOutput>> GetUserMemoByid(int id)
        {
            try
            {
                UserMemo userMemo = await _unitOfWork.UserMemo.GetByIdAsync(id);
                if (userMemo == null)
                {
                    throw new Exception($"user Memo was not found.");
                }
                var userMemoList = _mapper.Map<UserMemoOutput>(userMemo);
                return new ApiResponseModel<UserMemoOutput>
                {
                    succeed = true,
                    message = "success",
                    data = userMemoList
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<ApiResponseModel<bool>> AvailableForPublic(int id)
        {
            try
            {
                UserMemo userMemo = await _unitOfWork.UserMemo.GetByIdAsync(id);
                if (userMemo == null)
                {
                    return new ApiResponseModel<bool> { succeed = false, message = "project was not found" };

                }
                userMemo.AvailableForPublic = !userMemo.AvailableForPublic;

                await _unitOfWork.SaveAsync();

                return new ApiResponseModel<bool> { succeed = true, message = "success", data = userMemo.AvailableForPublic };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}

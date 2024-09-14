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
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
//using Microsoft.AspNetCore.Hosting;

namespace Hrmanagement.Service
{
    public class UserService : IUserService
    {
        public IUnitOfWork _unitOfWork;
        private readonly HrManagementContext _context;
        public IMapper _mapper;
        public IWebHostEnvironment _environment;
        public ISystemSettingService _systemSettingService;
        public IEmailService _emailService;
        public UserService(HrManagementContext context, IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment environment, ISystemSettingService systemSettingService, IEmailService emailService)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _environment = environment;
            _systemSettingService = systemSettingService;
            _emailService = emailService;
        }

        public async Task<ApiResponseModel<UserOutput>> AddUpdateUser(UserInput model)
        {

            UserOutput userOutput;
            try
            {

                string imageUrl = "";
                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    string dirpath = Path.Combine(_environment.WebRootPath, "images", "Image");
                    string filepath = MiscMethods.uploadFileToLocal(model.ImageFile, dirpath);
                    imageUrl = filepath;

                }
                User user = _mapper.Map<User>(model);
                if (user.Id != 0)
                {
                    User duplicateData = _unitOfWork.User.GetWhere(x => x.Id != user.Id && (x.Email == user.Email || x.Mobile == user.Mobile)).FirstOrDefault();
                    if (duplicateData != null)
                        return new ApiResponseModel<UserOutput>
                        {
                            succeed = false,
                            message = "Email is already exist"
                        };
                    user = await _unitOfWork.User.GetByIdAsync(model.Id);
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Mobile = model.Mobile;
                    user.Email = model.Email;
                    user.Role = model.Role;
                    user.IsActive = model.IsActive;
                    
                    

                    if (model.ImageFile != null)
                    {
                        user.ProfileImage = imageUrl;

                    }
                    user.UpdatedAt = DateTime.UtcNow;

                }

                if (user.Id == 0)
                {
                    User duplicateData = _unitOfWork.User.GetWhere(x => x.Email == user.Email || x.Mobile == user.Mobile).FirstOrDefault();
                    if (duplicateData != null)
                        return new ApiResponseModel<UserOutput>
                        {
                            succeed = false,
                            message = "Email or mobile is already exist"
                        };
                    user.ProfileImage = imageUrl;
                    user.IsActive = false;
                    user.CreatedAt = DateTime.UtcNow;
                    user.Password = MiscMethods.MD5Hash(model.Password);
                    await _unitOfWork.User.AddAsync(user);
                }
                await _unitOfWork.SaveAsync();


                // send email
                string subject =  $"Dear {user.Email},\\n\\nThank you for registering with us. Your account has been successfully created";
                string Message = $"<html><body>" +
                    $"<h1>Dear {user.FirstName + " " + user.LastName}</h1>" +
                    $"<h5>Email :-  {user.Email}</h5>" +
                    $"<p>Subject :-  {subject}</p>" +
                    $" <p>FromDate :- {user.CreatedAt}</p>" +
                    // $" <a href='http://192.168.0.29:5002/Admin/Auth/Login/'>Click here to view the page </a>" +
                    $" <a href='http://hrm.tagosys.com/Admin/Auth/Login/'>Click here to view the page </a>" +
                    $"</body></html>";
                string toEmail = user.Email;
                var rslt = _emailService.SendEmail(subject, Message, toEmail);

                userOutput = _mapper.Map<UserOutput>(user);
                return new ApiResponseModel<UserOutput>
                {
                    succeed = true,
                    message = "Success",
                    data = userOutput
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<ApiResponseModel<List<UserOutput>>> getAllUser()
        {
            try
            {
                // Fetching local image endpoint
                ImageEndpointModel res = await _systemSettingService.getLocalEndpoint();

                List<User> users = (await _unitOfWork.User.GetAllAsync()).ToList();
                var result = _mapper.Map<List<UserOutput>>(users);

                // Updating profile image URLs
                foreach (var profile in result)
                {
                    var localImage = profile.ProfileImage;
                    profile.LocalOrgImage = res.LocalImagEndPoint + localImage;
                }

                return new ApiResponseModel<List<UserOutput>>
                {
                    succeed = true,
                    message = "Success",
                    data = result
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponseModel<bool>> DeleteById(int id)
        {
            try
            {
                User user = await _unitOfWork.User.GetByIdAsync(id);
                if (user == null)
                    throw new Exception($"user was not Found.");
                _unitOfWork.User.Remove(user);
                await _unitOfWork.SaveAsync();
                return new ApiResponseModel<bool>
                {
                    succeed = true,
                    message = "User Delete Successfully"
                };

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponseModel<UserOutput>> getUserById(int id)
        {
            try
            {
                ImageEndpointModel response = await _systemSettingService.getLocalEndpoint();
                User user = await _unitOfWork.User.GetByIdAsync(id);
                if (user == null)
                    throw new Exception("user Not found");
                var res = _mapper.Map<UserOutput>(user);
                if (res != null)
                {
                    var localImage = res.ProfileImage;
                    res.LocalOrgImage = response.LocalImagEndPoint + localImage;
                }
                return new ApiResponseModel<UserOutput>
                {
                    succeed = true,
                    message = "success",
                    data = res
                };

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponseModel<bool>> ActiveInActive(int id)
        {
            try
            {
                User user = await _unitOfWork.User.GetByIdAsync(id);
                if (user == null)
                    throw new Exception("user not found");
                user.IsActive = !user.IsActive;
                await _unitOfWork.SaveAsync();
                return new ApiResponseModel<bool>
                {
                    succeed = true,
                    message = "success",
                    data = user.IsActive
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

     

        public async Task<ApiResponseModel<bool>> ChangePassword(ChangePasswordInput model, int userId)
        {
            User user = await _unitOfWork.User.GetByIdAsync(userId);
            if (user == null)
            {
                return new ApiResponseModel<bool>
                {
                    succeed = false,
                    message = "No user found",
                };
            }

            // Verify the user-provided old password against the stored hashed password
            if (!MiscMethods.VerifyPassword(model.OldPassword, user.Password))
            {
                return new ApiResponseModel<bool>
                {
                    succeed = false,
                    message = "Old password does not match",
                };
            }
            else
            {
                // Hash the new password and save it
                user.Password = MiscMethods.MD5Hash(model.NewPassword);
                await _unitOfWork.SaveAsync();

                return new ApiResponseModel<bool>
                {
                    succeed = true,
                    message = "Password changed successfully",
                };
            }
        }




    }
}

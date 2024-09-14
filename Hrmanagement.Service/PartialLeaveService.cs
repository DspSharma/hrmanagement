using AutoMapper;
using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.DTO.DtoOutput;
using Hrmanagement.Core.Models;
using Hrmanagement.Data.DBContext;
using Hrmanagement.Data.Entities;
using Hrmanagement.Data.UnitOfWork;
using Hrmanagement.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Service
{
    public class PartialLeaveService : IPartialLeaveService
    {
        public IUnitOfWork _unitOfWork;
        private readonly HrManagementContext _context;
        public IMapper _mapper;
        public IEmailService _emailService;
        public IConfiguration _Configuration { get; }

        public PartialLeaveService(HrManagementContext context, IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService, IConfiguration configuration)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
            _Configuration = configuration;

        }

        public async Task<ApiResponseModel<PartialLeaveOutput>> AddUpdatePartialLeave(PartialLeaveInput value)
        {
            try
            {
                PartialLeave formValue = _mapper.Map<PartialLeave>(value);
                // var user = await _unitOfWork.PartialLeave.GetByIdAsync(value.UserId);
                Attendance attendance = _unitOfWork.Attendance.GetWhere(x => x.UserId == value.UserId && x.CheckIn.Date == DateTime.Now.Date).FirstOrDefault();
                if (attendance == null  || attendance.IsCheckOut)
                {
                    return new ApiResponseModel<PartialLeaveOutput>
                    {
                        succeed = false,
                        message = "User have Already CheckOut"
                    };
                }
                else
                {


                    if (formValue.id != 0)
                    {
                        PartialLeave existingPartialLeave = _unitOfWork.PartialLeave.GetWhere(x => x.id == formValue.id && x.UserId == formValue.UserId).FirstOrDefault();


                        if (existingPartialLeave == null)
                        {
                            return new ApiResponseModel<PartialLeaveOutput>
                            {
                                succeed = false,
                                message = "Partial leave record not found for the user."
                            };
                        }
                        if (existingPartialLeave.ResumeTime == null)
                        {

                            // existingPartialLeave.Detail = existingPartialLeave.Detail;
                            existingPartialLeave.ResumeTime = DateTime.UtcNow;
                            existingPartialLeave.UpdatedAt = DateTime.UtcNow;
                            await _unitOfWork.SaveAsync();
                            var updatedPartialLeaves = await _unitOfWork.PartialLeave.GetByIdAsync(value.UserId);

                            PartialLeaveOutput partialLeaveOutputs = _mapper.Map<PartialLeaveOutput>(updatedPartialLeaves);
                            return new ApiResponseModel<PartialLeaveOutput>
                            {
                                succeed = true,
                                message = "Partial leave is resumed.",
                                data = partialLeaveOutputs
                            };
                        }
                    }
                    else
                    {
                        PartialLeave existingPartialLeave = _unitOfWork.PartialLeave.GetWhere(x => x.UserId == formValue.UserId && x.ResumeTime == null).OrderByDescending(x => x.ResumeTime).FirstOrDefault();

                        if (existingPartialLeave != null)
                        {
                            return new ApiResponseModel<PartialLeaveOutput>
                            {
                                succeed = false,
                                message = "Please resume the previous partial leave before taking a new one.",
                                data = null
                            };
                        }

                    }

                    if (formValue.id == 0)
                    {
                        formValue.PauseTime = DateTime.UtcNow;
                        formValue.UpdatedAt = DateTime.UtcNow;
                        await _unitOfWork.PartialLeave.AddAsync(formValue);
                    }

                    await _unitOfWork.SaveAsync();
                    var updatedPartialLeave = await _unitOfWork.PartialLeave.GetByIdAsync(value.UserId);

                    PartialLeaveOutput partialLeaveOutput = _mapper.Map<PartialLeaveOutput>(updatedPartialLeave);
                    return new ApiResponseModel<PartialLeaveOutput>
                    {
                        succeed = true,
                        message = "New partial leave added successfully.",
                        data = partialLeaveOutput
                    };
                }

            }
            catch (Exception ex)
            {
                return new ApiResponseModel<PartialLeaveOutput>
                {
                    succeed = false,
                    message = ex.Message
                };
            }
        }

        public async Task<ApiResponseModel<List<PartialLeaveOutput>>> GetAllPartialLeaves(int userId)
        {
            try
            {
                List<PartialLeave> PartialLeaves;
                if (userId != 0)
                {
                    PartialLeaves = _unitOfWork.PartialLeave.GetWhere(x => x.UserId == userId).ToList();
                    if (PartialLeaves == null || !PartialLeaves.Any())
                    {
                        return new ApiResponseModel<List<PartialLeaveOutput>>
                        {
                            succeed = false,
                            message = "No user Memo found."
                        };
                    }
                }
                else
                {
                    PartialLeaves = (await _unitOfWork.PartialLeave.GetAllAsync()).ToList();
                }


                List<int> listUserId = PartialLeaves.Select(x => x.UserId).Distinct().ToList();
                List<User> users = _unitOfWork.User.GetWhere(x => listUserId.Contains(x.Id)).ToList();
                var PartialLeaveOutputs = from l in PartialLeaves
                                          join u in users on l.UserId equals u.Id
                                          select new PartialLeaveOutput
                                          {
                                              id = l.id,
                                              UserId = u.Id,
                                              Username = u.FirstName,
                                              Detail = l.Detail,
                                              PauseTime = l.PauseTime,
                                              ResumeTime = l.ResumeTime,
                                              UpdatedAt = l.UpdatedAt,

                                          };
                var partialleaveList = _mapper.Map<List<PartialLeaveOutput>>(PartialLeaveOutputs);

                return new ApiResponseModel<List<PartialLeaveOutput>> { succeed = true, message = "success", data = partialleaveList };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //public async Task<ApiResponseModel<Dictionary<string, object>>> CalculateUserLeaveHours(int userId)
        //{
        //    try
        //    {

        //        // Fetch all partial leaves for the user.
        //        var partialLeaves = _unitOfWork.PartialLeave.GetWhere(x => x.UserId == userId).ToList();

        //        if (partialLeaves == null || !partialLeaves.Any())
        //        {
        //            return new ApiResponseModel<Dictionary<string, object>>
        //            {
        //                succeed = false,
        //                message = "No partial leaves found for the user."
        //            };
        //        }

        //        // Step 1: Calculate the total time for each partial leave.
        //        var leavesWithTotalHours = partialLeaves.Select(leave => new
        //        {
        //            leave.id,
        //            TotalHours = (leave.ResumeTime.HasValue ? leave.ResumeTime.Value : DateTime.Now) - leave.PauseTime
        //        }).ToList();

        //        // Step 2: Calculate the total hours of leaves taken at the end of each day.
        //        var dailyTotals = leavesWithTotalHours
        //            .GroupBy(leave => leave.PauseTime.Date)
        //            .Select(group => new
        //            {
        //                Date = group.Key,
        //                TotalHours = group.Sum(leave => leave.TotalHours.TotalHours)
        //            }).ToList();

        //        // Step 3: Calculate the total hours of leaves taken in the current month.
        //        var currentMonth = DateTime.Now.Month;
        //        var currentYear = DateTime.Now.Year;
        //        var monthlyTotalHours = leavesWithTotalHours
        //            .Where(leave => leave.PauseTime.Month == currentMonth && leave.Pause.Year == currentYear)
        //            .Sum(leave => leave.TotalHours.TotalHours);

        //        var resultData = new Dictionary<string, object>
        //{
        //    {"LeavesWithTotalHours", leavesWithTotalHours},
        //    {"DailyTotals", dailyTotals},
        //    {"MonthlyTotalHours", monthlyTotalHours}
        //};

        //        return new ApiResponseModel<Dictionary<string, object>>
        //        {
        //            succeed = true,
        //            message = "Success",
        //            data = resultData
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"An error occurred: {ex.Message}");
        //    }
        //}

        //public async Task<ApiResponseModel<List<PartialLeaveOutput>>> GetAllPartialLeave(int userId)
        //{
        //    try
        //    {
        //        // Fetch partial leaves and users efficiently with a single query
        //        var userPartialLeaves = await _unitOfWork.PartialLeave
        //            .Include(pl => pl.User)
        //            .Where(pl => userId == 0 || pl.UserId == userId)
        //            .ToListAsync();

        //        if (!userPartialLeaves.Any())
        //        {
        //            return new ApiResponseModel<List<PartialLeaveOutput>>
        //            {
        //                succeed = false,
        //                message = "No user partial leaves found."
        //            };
        //        }

        //        // Calculate daily, monthly, and total hours in a single LINQ expression
        //        var partialLeaveOutputs = userPartialLeaves
        //            .GroupBy(pl => pl.UserId)
        //            .Select(userGroup => new PartialLeaveOutput
        //            {
        //                UserId = userGroup.Key,
        //                Username = userGroup.First().User.FirstName, // Access user name directly
        //                DailyHours = userGroup.Where(pl => pl.PauseTime.Date == DateTime.Now.Date)
        //                    .Sum(pl => (pl.ResumeTime - pl.PauseTime).TotalHours),
        //                MonthlyHours = userGroup.Where(pl => pl.PauseTime.Month == DateTime.Now.Month)
        //                    .Sum(pl => (pl.ResumeTime - pl.PauseTime).TotalHours),
        //                TotalHours = userGroup.Sum(pl => (pl.ResumeTime - pl.PauseTime).TotalHours),
        //                LeavesExceeded = (userGroup.Sum(pl => (pl.ResumeTime - pl.PauseTime).TotalHours) > dailyThreshold ||
        //                                  userGroup.Where(pl => pl.PauseTime.Month == DateTime.Now.Month)
        //                                      .Sum(pl => (pl.ResumeTime - pl.PauseTime).TotalHours) > monthlyThreshold ||
        //                                  userGroup.Sum(pl => (pl.ResumeTime - pl.PauseTime).TotalHours) > totalThreshold)
        //            });

        //        var partialleaveList = partialLeaveOutputs.ToList();

        //        return new ApiResponseModel<List<PartialLeaveOutput>> { succeed = true, message = "success", data = partialleaveList };
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}


    }
}

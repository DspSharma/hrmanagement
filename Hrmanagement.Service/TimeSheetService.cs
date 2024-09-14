using AutoMapper;
using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.DTO.DtoOutput;
using Hrmanagement.Core.Models;
using Hrmanagement.Data.DBContext;
using Hrmanagement.Data.Entities;
using Hrmanagement.Data.UnitOfWork;
using Hrmanagement.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Service
{
    public class TimeSheetService : ITimeSheetService
    {
        public IUnitOfWork _unitOfWork;
        private readonly HrManagementContext _context;
        public IMapper _mapper;


        public TimeSheetService(HrManagementContext context, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        public async Task<ApiResponseModel<TimeSheetOutput>> AddTimeSheet(TimeSheetInput value)
        {

            try
            {

                Attendance attendance = _unitOfWork.Attendance.GetWhere(x => x.UserId == value.UserId && x.CheckIn.Date == DateTime.Now.Date ).FirstOrDefault();
                if (attendance == null)
                {
                    return new ApiResponseModel<TimeSheetOutput>
                    {
                        succeed = false,
                        message = "Check-in required.",
                    };
                }

                if (attendance.CreatedAt.Date <= DateTime.Now.Date)
                {
                    if (attendance.IsCheckOut == true)
                    {
                        return new ApiResponseModel<TimeSheetOutput>
                        {
                            succeed = false,
                            message = "After checkout, editing or adding any timesheet is not allowed.",

                        };
                    }
                }
                

                TimeSheet formValue = _mapper.Map<TimeSheet>(value);
                if (formValue.id != 0)
                {
                    TimeSheet timeSheets = await _unitOfWork.TimeSheet.GetByIdAsync(value.id);

                    if (timeSheets == null)
                        throw new Exception($"Timesheet was not found.");

                    timeSheets.TaskTitle = value.TaskTitle;
                    timeSheets.TaskDescription = value.TaskDescription;
                    timeSheets.ProjectId = value.ProjectId;
                    timeSheets.UserId = value.UserId;
                    timeSheets.Status = value.Status;
                    timeSheets.Hours = value.Hours;
                    timeSheets.UpdatedAt = DateTime.UtcNow;
                    await _unitOfWork.SaveAsync();

                }
                else
                {
                    formValue.CreatedAt = DateTime.Now;
                    formValue.UpdatedAt = DateTime.UtcNow;
                    await _unitOfWork.TimeSheet.AddAsync(formValue);
                    await _unitOfWork.SaveAsync();
                }


                TimeSheetOutput timeSheetOutput = _mapper.Map<TimeSheetOutput>(formValue);
                return new ApiResponseModel<TimeSheetOutput>
                {
                    succeed = true,
                    message = "Success",
                    data = timeSheetOutput
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponseModel<List<TimeSheetOutput>>> GetAllTimeSheet(int userId)
        {

            try
            {

                List<TimeSheet> timeSheets;
                if (userId != 0)
                {

                    timeSheets = _unitOfWork.TimeSheet.GetWhere(x => x.UserId == userId).ToList();
                    if (timeSheets == null || !timeSheets.Any())
                    {
                        return new ApiResponseModel<List<TimeSheetOutput>>
                        {
                            succeed = false,
                            message = "No Timesheet found for the given UserId."
                        };
                    }

                }
                else
                {
                    timeSheets = (await _unitOfWork.TimeSheet.GetAllAsync()).ToList();

                }
                List<int> listUserId = timeSheets.Select(x => x.UserId).Distinct().ToList();
                List<User> users = _unitOfWork.User.GetWhere(x => listUserId.Contains(x.Id)).ToList();

                List<int> listprojectId = timeSheets.Select(x => x.ProjectId).Distinct().ToList();
                List<Project> projects = _unitOfWork.Project.GetWhere(x => listprojectId.Contains(x.Id)).ToList();


                var timesheetsList = from t in timeSheets
                                     join u in users on t.UserId equals u.Id
                                     join p in projects on t.ProjectId equals p.Id

                                     select new TimeSheetOutput
                                     {

                                         id = t.id,
                                         UserId = u.Id,
                                         UserName = u.FirstName,
                                         ProjectId = p.Id,
                                         ProjectName = p.Title,
                                         TaskTitle = t.TaskTitle,
                                         TaskDescription = t.TaskDescription,
                                         Hours = t.Hours,
                                         Status = t.Status,
                                         CreatedAt = t.CreatedAt,
                                         UpdatedAt = t.UpdatedAt,
                                         dayname = t.CreatedAt.ToString("dddd"),

                                     };


                var rsult = _mapper.Map<List<TimeSheetOutput>>(timesheetsList);
                return new ApiResponseModel<List<TimeSheetOutput>>
                {
                    succeed = true,
                    message = "Successfully retrieved  All Time Sheets",
                    data = rsult
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponseModel<TimeSheetOutput>> GetTimeSheetByid(int id)
        {

            try
            {
                TimeSheet timeSheets = await _unitOfWork.TimeSheet.GetByIdAsync(id);
                if (timeSheets == null)

                {
                    throw new Exception($"timeSheets was not found.");
                }
                var rsult = _mapper.Map<TimeSheetOutput>(timeSheets);
                return new ApiResponseModel<TimeSheetOutput>
                {
                    succeed = true,
                    message = "Success",
                    data = rsult
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<ApiResponseModel<bool>> DeleteTimeSheetByid(int id)
        {

            try
            {

                //Attendance attendance = _unitOfWork.Attendance.GetWhere(x =>x.id != id && x.CheckIn.Date <= DateTime.Now.Date).FirstOrDefault();
                //if (attendance.IsCheckOut == true)
                //{
                //    return new ApiResponseModel<bool>
                //    {
                //        succeed = false,
                //        message = "after checkout not allow delete timesheet",

                //    };
                //}

                TimeSheet timeSheets = await _unitOfWork.TimeSheet.GetByIdAsync(id);
                if (timeSheets == null)

                {
                    throw new Exception($"timeSheets was not found.");
                }

                _unitOfWork.TimeSheet.Remove(timeSheets);
                await _unitOfWork.SaveAsync();
                return new ApiResponseModel<bool>
                {
                    succeed = true,
                    message = "Time sheet  Delete Successfully"

                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }


    }
}

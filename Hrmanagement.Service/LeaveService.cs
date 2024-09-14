using AutoMapper;
using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.DTO.DtoOutput;
using Hrmanagement.Core.Models;
using Hrmanagement.Data.DBContext;
using Hrmanagement.Data.Entities;
using Hrmanagement.Data.Repositories.Interfaces;
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
    public class LeaveService : ILeaveService
    {
        public IUnitOfWork _unitOfWork;
        private readonly HrManagementContext _context;
        public IMapper _mapper;
        public IEmailService _emailService;
        public IConfiguration _Configuration { get; }

        public LeaveService(HrManagementContext context, IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService, IConfiguration configuration)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
            _Configuration = configuration;

        }

        public async Task<ApiResponseModel<LeaveOutput>> AddLeave(LeaveInput value)
        {
            try
            {
                Leave formValue = _mapper.Map<Leave>(value);
                // Check if there are any existing leaves for the same date range
                Leave leaveExists = _unitOfWork.Leave.GetWhere(x => x.UserId == formValue.UserId && x.LeaveFromDate <= formValue.LeaveToDate && x.LeaveToDate >= formValue.LeaveFromDate).FirstOrDefault();

                User user = _unitOfWork.User.GetWhere(x => x.Id == formValue.UserId).FirstOrDefault();
                User adminUser = _unitOfWork.User.GetWhere(x => x.Role == "admin" && x.IsActive).FirstOrDefault();
                if (leaveExists != null)
                {
                    return new ApiResponseModel<LeaveOutput>
                    {
                        succeed = false,
                        message = "Leave for the same date range already exists.",
                    };
                }

                if (formValue.LeaveFromDate > formValue.LeaveToDate)
                {
                    return new ApiResponseModel<LeaveOutput>
                    {
                        succeed = true,
                        message = "Leave From Date not not greater than Leave To Date",
                    };
                }
                else
                {
                    //formValue.UserId = userId;
                    //formValue.UserId = value.UserId;
                    formValue.Status = "pending";
                    formValue.IsRejected = false;
                    formValue.IsLeaveApproved = false;
                    await _unitOfWork.Leave.AddAsync(formValue);
                    await _unitOfWork.SaveAsync();

                    // send Email Work Start
                    string userName = user.FirstName + " " + user.LastName;
                    string userEmail = user.Email;
                    string userSubject = formValue.Title;
                    string userMessage = formValue.Message;
                    DateTime fromDate = formValue.LeaveFromDate;
                    DateTime toDate = formValue.LeaveToDate;
                    int totalDays = (int)(toDate - fromDate).TotalDays + 1;
                    string subject = "This is to inform you that we have received a request for leave from one of our users";
                    string Message = $"<html><body>" +
                        $"<h1>Dear {userName}</h1>" +
                        $"<h5>Email :-  {userEmail}</h5>" +
                        $"<p>Subject :-  {userSubject}</p>" +
                        $" <p>Message :- {userMessage}</p> " +
                        $" <p>FromDate :- {fromDate}</p>" +
                        $" <p>ToDate :- {toDate}</p>" +
                        $" <p>TotalDays :- {totalDays}</p>" +
                        // $" <a href='http://192.168.0.29:5002/Admin/Auth/Login/'>Click here to view the page </a>" +
                        $" <a href='http://hrm.tagosys.com/Admin/Auth/Login/'>Click here to view the page </a>" +
                        $"</body></html>";
                    string toEmail = adminUser.Email;

                    var rslt = _emailService.SendEmail(subject, Message, toEmail);
                    // send Email Work End
                }

                LeaveOutput leaveOutputs = _mapper.Map<LeaveOutput>(formValue);
                return new ApiResponseModel<LeaveOutput>
                {
                    succeed = true,
                    message = "success",
                    data = leaveOutputs
                };

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponseModel<List<LeaveOutput>>> GetAllLeaves()
        {
            try
            {

                List<Leave> leaves;
                leaves = (await _unitOfWork.Leave.GetAllAsync()).ToList();
                List<int> listUserId = leaves.Select(x => x.UserId).Distinct().ToList();
                List<User> users = _unitOfWork.User.GetWhere(x => listUserId.Contains(x.Id)).ToList();
                var LeaveOutputs = from l in leaves
                                   join u in users on l.UserId equals u.Id
                                   select new LeaveOutput
                                   {
                                       Id = l.Id,
                                       UserId = u.Id,
                                       UserName = u.FirstName,
                                       Title = l.Title,
                                       Message = l.Message,
                                       Remark = l.Remark,
                                       LeaveFromDate = l.LeaveFromDate,
                                       LeaveToDate = l.LeaveToDate,
                                       IsLeaveApproved = l.IsLeaveApproved,
                                       IsRejected = l.IsRejected,
                                       CreatedAt = l.CreatedAt,
                                       UpdatedAt = l.UpdatedAt,
                                       //Status = l.Status,
                                       TotalLeaveDays = (int)(l.LeaveToDate - l.LeaveFromDate).TotalDays
                                   };
                var leaveList = _mapper.Map<List<LeaveOutput>>(LeaveOutputs);
                return new ApiResponseModel<List<LeaveOutput>>
                {
                    succeed = true,
                    message = "success",
                    data = leaveList
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponseModel<List<LeaveOutput>>> GetAllLeaves(string status)
        {
            try
            {
                List<Leave> leaves = (await _unitOfWork.Leave.GetAllAsync()).ToList();
                List<int> listUserId = leaves.Select(x => x.UserId).Distinct().ToList();
                List<User> users = _unitOfWork.User.GetWhere(x => listUserId.Contains(x.Id)).ToList();

                IEnumerable<Leave> filteredLeaves;

                switch (status.ToLower())
                {
                    case "all":
                        filteredLeaves = leaves;
                        break;
                    case "approved":
                        filteredLeaves = leaves.Where(l => l.IsLeaveApproved);
                        break;
                    case "pending":
                        filteredLeaves = leaves.Where(l => !l.IsLeaveApproved && !l.IsRejected);
                        break;
                    case "rejected":
                        filteredLeaves = leaves.Where(l => l.IsRejected);
                        break;
                    default:
                        // Default to pending if status is not recognized
                        filteredLeaves = leaves.Where(l => !l.IsLeaveApproved && !l.IsRejected);
                        break;
                }

                var LeaveOutputs = from l in filteredLeaves
                                   join u in users on l.UserId equals u.Id
                                   select new LeaveOutput
                                   {
                                       Id = l.Id,
                                       UserId = u.Id,
                                       UserName = u.FirstName,
                                       Title = l.Title,
                                       Message = l.Message,
                                       Remark = l.Remark,
                                       LeaveFromDate = l.LeaveFromDate,
                                       LeaveToDate = l.LeaveToDate,
                                       IsLeaveApproved = l.IsLeaveApproved,
                                       IsRejected = l.IsRejected,
                                       CreatedAt = l.CreatedAt,
                                       UpdatedAt = l.UpdatedAt,
                                       TotalLeaveDays = (int)(l.LeaveToDate - l.LeaveFromDate).TotalDays,
                                       Status = status
                                   };

                var leaveList = _mapper.Map<List<LeaveOutput>>(LeaveOutputs);
                return new ApiResponseModel<List<LeaveOutput>>
                {
                    succeed = true,
                    message = "success",
                    data = leaveList
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<ApiResponseModel<bool>> DeleteByIdLeave(int id)
        {
            try
            {
                Leave leave = await _unitOfWork.Leave.GetByIdAsync(id);
                if (leave == null)
                {
                    return new ApiResponseModel<bool> { succeed = false, message = "leave was not Found" };
                }

                _unitOfWork.Leave.Remove(leave);
                await _unitOfWork.SaveAsync();
                return new ApiResponseModel<bool> { succeed = true, message = $"Delete Successfully" };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<ApiResponseModel<LeaveOutput>> GetLeaveById(int id)
        {
            try
            {
                Leave leave = await _unitOfWork.Leave.GetByIdAsync(id);
                if (leave == null)
                {
                    return new ApiResponseModel<LeaveOutput> { succeed = false, message = $"Leave record not found " };
                }

                var result = _mapper.Map<LeaveOutput>(leave);
                return new ApiResponseModel<LeaveOutput> { succeed = true, message = "success", data = result };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<ApiResponseModel<List<LeaveOutput>>> GetLeavesByUserId(int userId, int year, int month)
        {
            try
            {
                List<Leave> userLeaves;
                if (year != 0 || month != 0)
                {
                    userLeaves = _unitOfWork.Leave.GetWhere(x => x.UserId == userId && x.LeaveFromDate.Year == year && x.LeaveFromDate.Month == month).ToList();

                }
                else
                {

                    userLeaves = _unitOfWork.Leave.GetWhere(x => x.UserId == userId).ToList();

                }
                if (userLeaves == null || userLeaves.Count == 0)
                {
                    return new ApiResponseModel<List<LeaveOutput>> { succeed = false, message = ($"No leave records found for user with ID {userId}.") };
                }

                var leaveOutputs = _mapper.Map<List<LeaveOutput>>(userLeaves);

                foreach (var leave in leaveOutputs)
                {
                    double totalDay = (leave.LeaveToDate - leave.LeaveFromDate).TotalDays + 1;
                    leave.TotalLeaveDays = totalDay;
                }

                return new ApiResponseModel<List<LeaveOutput>> { succeed = true, message = "Success", data = leaveOutputs };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // by Admin
        public async Task<ApiResponseModel<LeaveOutput>> updateLeavestatus(int id, bool IsApproved, bool IsRejected, string Remark)
        {
            Leave leave = _unitOfWork.Leave.GetWhere(x => x.Id == id).FirstOrDefault();
            if (leave.IsLeaveApproved == IsApproved)
            {
                leave.Id = id;
                leave.IsLeaveApproved = false;
                leave.UpdatedAt = DateTime.UtcNow;
                await _unitOfWork.SaveAsync();
            }
            else if (leave.IsRejected == IsRejected)
            {
                leave.Id = id;
                leave.IsRejected = false;
                leave.UpdatedAt = DateTime.UtcNow;
                await _unitOfWork.SaveAsync();
            }
            leave.Id = id;
            leave.IsLeaveApproved = IsApproved;
            leave.IsRejected = IsRejected;
            leave.Remark = Remark;
            leave.UpdatedAt = DateTime.UtcNow;
            await _unitOfWork.SaveAsync();
            LeaveOutput leaveOutput = _mapper.Map<LeaveOutput>(leave);
            return new ApiResponseModel<LeaveOutput>
            {
                succeed = true,
                message = "success",
                data = leaveOutput
            };
        }

    }
}

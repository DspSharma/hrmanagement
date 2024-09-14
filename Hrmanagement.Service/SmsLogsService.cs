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
    public class SmsLogsService : ISmsLogsService
    {
        public IUnitOfWork _unitOfWork;
        private readonly HrManagementContext _context;
        public IMapper _mapper;


        public SmsLogsService(HrManagementContext context, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        public async Task<ApiResponseModel<SMSLogsOutput>> AddSmsLogs(SMSlogsInput value)
        {
            try
            {
                SMSLogs formValue = _mapper.Map<SMSLogs>(value);

                formValue.CreatedAt = DateTime.UtcNow;
                formValue.UpdatedAt = DateTime.UtcNow;
                await _unitOfWork.SmsLogs.AddAsync(formValue);
                await _unitOfWork.SaveAsync();

                SMSLogsOutput sMSLogsOutput = _mapper.Map<SMSLogsOutput>(formValue);
                return new ApiResponseModel<SMSLogsOutput>
                {
                    succeed = true,
                    message = "Success",
                    data = sMSLogsOutput
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponseModel<List<SMSLogsOutput>>> GetAllSmsLogs()
        {
            try
            {
                List<SMSLogs> smsLogs = (await _unitOfWork.SmsLogs.GetAllAsync()).ToList();
                List<int> listUserId = smsLogs.Select(x => x.userId).Distinct().ToList();
                List<User> users = _unitOfWork.User.GetWhere(x => listUserId.Contains(x.Id)).ToList();
                List<int> listprojectId = smsLogs.Select(x => x.ProjectId).Distinct().ToList();
                List<Project> projects = _unitOfWork.Project.GetWhere(x => listprojectId.Contains(x.Id)).ToList();
                var timesheetsList = from sl in smsLogs
                                     join u in users on sl.userId equals u.Id
                                     join p in projects on sl.ProjectId equals p.Id
                                     select new SMSLogsOutput
                                     {

                                         id = sl.id,
                                         userId = u.Id,
                                         UserName = u.FirstName,
                                         ProjectId = p.Id,
                                         ProjectName = p.Title,
                                         Pid = sl.Pid,
                                         Title = sl.Title,
                                         Message = sl.Message,
                                         From = sl.From,
                                         To = sl.To,
                                         Status = sl.Status,
                                         CreatedAt = sl.CreatedAt,
                                         UpdatedAt = sl.UpdatedAt,
                                     };


                var rsult = _mapper.Map<List<SMSLogsOutput>>(timesheetsList);
                return new ApiResponseModel<List<SMSLogsOutput>>
                {
                    succeed = true,
                    message = "Successfully retrieved  All Sms Logs",
                    data = rsult
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}

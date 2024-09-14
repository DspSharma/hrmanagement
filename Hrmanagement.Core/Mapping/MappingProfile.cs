using AutoMapper;
using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.DTO.DtoOutput;
using Hrmanagement.Core.Models;
using Hrmanagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, LoggedInUserModel>();
            CreateMap<User, UserOutput>();

            CreateMap<ApiCredentialsInput, ApiCredentials>();
            CreateMap<ApiCredentialsOutput, ApiCredentials>();
            CreateMap<ApiCredentials, ApiCredentialsOutput>();

            CreateMap<UserInput, User>();
            CreateMap<UserOutput, User>();
            CreateMap<User, UserOutput>();

            CreateMap<UserMemoInput, UserMemo>();
            CreateMap<UserMemoOutput, UserMemo>();
            CreateMap<UserMemo, UserMemoOutput>();

            CreateMap<HolidayInput, Holiday>();
            CreateMap<HolidayOutput, Holiday>();
            CreateMap<Holiday, HolidayOutput>();

            CreateMap<LeaveInput, Leave>();
            CreateMap<LeaveOutput, Leave>();
            CreateMap<Leave, LeaveOutput>();

            CreateMap<AttendanceInput, Attendance>();
            CreateMap<AttendanceOutput, Attendance>();
            CreateMap<Attendance, AttendanceOutput>();

            CreateMap<SystemSettingInput, SystemSetting>();
            CreateMap<SystemSettingOutput, SystemSetting>();
            CreateMap<SystemSetting, SystemSettingOutput>();

            CreateMap<ProjectInput, Project>();
            CreateMap<ProjectOutput, Project>();
            CreateMap<Project, ProjectOutput>();

            CreateMap<SMSlogsInput, SMSLogs>();
            CreateMap<SMSLogsOutput, SMSLogs>();
            CreateMap<SMSLogs, SMSLogsOutput>();

            CreateMap<TimeSheetInput, TimeSheet>();
            CreateMap<TimeSheetOutput, TimeSheet>();
            CreateMap<TimeSheet, TimeSheetOutput>();

            CreateMap<PartialLeaveInput, PartialLeave>();
            CreateMap<PartialLeaveOutput, PartialLeave>();
            CreateMap<PartialLeave, PartialLeaveOutput>();
        }
    }
}

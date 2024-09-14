using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.DTO.DtoOutput;
using Hrmanagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Service.Interfaces
{
    public interface IAttendanceService
    {
        Task<ApiResponseModel<AttendanceOutput>>AddUpdateAttendance(AttendanceInput value);
        //Task<ApiResponseModel<AttendanceModels>> getAttendances(int id, int year, int month);

        Task<ApiResponseModel<AttendanceModels>> getAttendances(int id, int year, int month);

        //Task<ApiResponseModel<AttendanceModels>> AllAttendances(int userId, int year, int month);

        Task<ApiResponseModel<AttendanceOutput>> GetMonthlyAttendanceSummary(int userId);


        Task<ApiResponseModel<AttendanceOutput>> AddUpdateAttendanceByAdmin(AttendanceInput value);

        Task<ApiResponseModel<bool>> IsCheckedIn(int id);
        Task<ApiResponseModel<bool>> IsPause(int id);

        //Task<ApiResponseModel<AttendanceOutput>> AddUpdateAttendances(AttendanceInput value);

        Task<ApiResponseModel<AttendanceOutput>> GetAttendanceByid(int id);

        

    }
}

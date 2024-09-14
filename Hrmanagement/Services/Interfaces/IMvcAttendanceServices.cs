using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.DTO.DtoOutput;
using Hrmanagement.Core.Models;

namespace Hrmanagement.Services.Interfaces
{
    public interface IMvcAttendanceServices
    {
        //Task<ApiResponseModel<AttendanceOutput>> addUpdateAttendance(int id);
        Task<ApiResponseModel<AttendanceOutput>> addUpdateAttendance(AttendanceInput value);
        Task<ApiResponseModel<AttendanceModels>> attendanceGetUserBy(int id, int year, int month);
        //Task<ApiResponseModel<AttendanceModels>> attendanceGetUserBy(int id, int year, int month);
        //Task<ApiResponseModel<AttendanceOutput>> attendanceGetMonthSummary(int id);
        //Task<ApiResponseModel<List<AttendanceModels>>> attendanceGetUserBy(int id, int year, int month);


        Task<ApiResponseModel<AttendanceOutput>> attendanceGetMonthSummary();
        Task<ApiResponseModel<AttendanceOutput>> addUpdateAttendanceAdmin(AttendanceInput value);
        Task<ApiResponseModel<PartialLeaveOutput>> AddUpdatePartialLeave(PartialLeaveInput value);
        Task<ApiResponseModel<List<PartialLeaveOutput>>> partialLeaveList(int id);
        Task<ApiResponseModel<AttendanceOutput>> attendanceById(int id);
    }
}

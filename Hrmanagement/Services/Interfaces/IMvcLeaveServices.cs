using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.DTO.DtoOutput;
using Hrmanagement.Core.Models;

namespace Hrmanagement.Services.Interfaces
{
    public interface IMvcLeaveServices
    {
        Task<ApiResponseModel<LeaveOutput>> addUpdateLeave(LeaveInput value);
        Task<ApiResponseModel<List<LeaveOutput>>> LeaveList(string status);
        Task<ApiResponseModel<List<LeaveOutput>>> getLeaveUserById(int userId, int year, int month);
        Task<ApiResponseModel<LeaveOutput>> leaveById(int id);
        Task<ApiResponseModel<LeaveOutput>> updateLeavestatus(int id, bool IsApproved, bool IsRejected, string Remark);
    }
}

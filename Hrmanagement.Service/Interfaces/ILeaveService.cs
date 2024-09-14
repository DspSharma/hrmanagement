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
    public interface ILeaveService
    {
        Task<ApiResponseModel<LeaveOutput>> AddLeave(LeaveInput value);
        Task<ApiResponseModel<List<LeaveOutput>>> GetAllLeaves();
        Task<ApiResponseModel<List<LeaveOutput>>> GetAllLeaves(string status);
        Task<ApiResponseModel<bool>> DeleteByIdLeave(int id);
        Task<ApiResponseModel<LeaveOutput>> GetLeaveById(int id);
        Task<ApiResponseModel<List<LeaveOutput>>> GetLeavesByUserId(int userId, int year, int month);
        Task<ApiResponseModel<LeaveOutput>> updateLeavestatus(int id, bool IsApproved, bool IsRejected, string Remark);
    }
}

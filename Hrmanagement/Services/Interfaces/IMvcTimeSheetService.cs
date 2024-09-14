using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.DTO.DtoOutput;
using Hrmanagement.Core.Models;

namespace Hrmanagement.Services.Interfaces
{
    public interface IMvcTimeSheetService
    {
        Task<ApiResponseModel<TimeSheetOutput>> addUpdateTimeSheet(TimeSheetInput value);
        Task<ApiResponseModel<List<TimeSheetOutput>>> timeSheetList(int userid);
        Task<ApiResponseModel<TimeSheetOutput>> editTimeSheet(int id);
        Task<ApiResponseModel<bool>> timeSheetdeleteById(int id);
    }
}

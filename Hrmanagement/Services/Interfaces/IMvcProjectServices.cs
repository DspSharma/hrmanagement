using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.DTO.DtoOutput;
using Hrmanagement.Core.Models;

namespace Hrmanagement.Services.Interfaces
{
    public interface IMvcProjectServices
    {
        Task<ApiResponseModel<ProjectOutput>> addUpdateProject(ProjectInput value);
        Task<ApiResponseModel<List<ProjectOutput>>> projectList();
        Task<ApiResponseModel<ProjectOutput>> editProject(int id);
        Task<ApiResponseModel<bool>> activeInActiveHoliday(int id);
        Task<ApiResponseModel<bool>> deleteByIdProject(int id);
    }
}

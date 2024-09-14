using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.DTO.DtoOutput;
using Hrmanagement.Core.Models;

namespace Hrmanagement.Services.Interfaces
{
    public interface IMvcSystemSettingServices
    {
        Task<ApiResponseModel<List<SystemSettingOutput>>> getSystemSetting();
        Task<ApiResponseModel<SystemSettingOutput>> AddUpdateSystemSetting(SystemSettingInput model);
        Task<ApiResponseModel<SystemSettingOutput>> editSystemSetting(int id);
        Task<ApiResponseModel<bool>> deleteById(int id);
        Task<ApiResponseModel<bool>> activeInActive(int id);
    }
}

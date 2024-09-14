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
    public interface ISystemSettingService
    {
        Task<ApiResponseModel<SystemSettingOutput>> AddUpdateSystemSetting(SystemSettingInput input);
        Task<ApiResponseModel<List<SystemSettingOutput>>> GetAllSystemSetting();
        Task<ApiResponseModel<bool>> DeleteSystemSettingById(int id);
        Task<ApiResponseModel<bool>> ActiveInActive(int id);
        Task<ApiResponseModel<SystemSettingOutput>> GetSystemSettingById(int id);

        Task<ImageEndpointModel> getLocalEndpoint();

    }
}

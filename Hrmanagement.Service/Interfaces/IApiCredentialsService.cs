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
    public interface IApiCredentialsService
    {
        Task<ApiResponseModel<ApiCredentialsOutput>> AddUpdateApiCredential(ApiCredentialsInput value);
        Task<ApiResponseModel<List<ApiCredentialsOutput>>> GetAllApiCredential();
        Task<ApiResponseModel<bool>> DeleteById(int id);
        Task<ApiResponseModel<ApiCredentialsOutput>> getApiCredentiaById(int id);
    }
}

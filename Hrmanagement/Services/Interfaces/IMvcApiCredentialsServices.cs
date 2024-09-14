using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.DTO.DtoOutput;
using Hrmanagement.Core.Models;

namespace Hrmanagement.Services.Interfaces
{
    public interface IMvcApiCredentialsServices
    {
        Task<ApiResponseModel<ApiCredentialsOutput>> addUpdateApiCredential(ApiCredentialsInput value);
        Task<ApiResponseModel<List<ApiCredentialsOutput>>> apiCredentialList();
        Task<ApiResponseModel<ApiCredentialsOutput>> editApiCredential(int id);
        Task<ApiResponseModel<bool>> deleteByIdApiCredential(int id);
    }
}

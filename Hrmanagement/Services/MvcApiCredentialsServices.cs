using Hrmanagement.Core.Constants;
using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.DTO.DtoOutput;
using Hrmanagement.Core.Models;
using Hrmanagement.Service.Interfaces;
using Hrmanagement.Services.Interfaces;
using Newtonsoft.Json;

namespace Hrmanagement.Services
{
    public class MvcApiCredentialsServices : IMvcApiCredentialsServices
    {
        public IApiServices _apiServices;
        public MvcApiCredentialsServices(IApiServices apiServices)
        {
            _apiServices = apiServices;
        }
        public async Task<ApiResponseModel<ApiCredentialsOutput>> addUpdateApiCredential(ApiCredentialsInput value)
        {
            var postApi = ApiCredentialsEndPoint.apiCredentialAddUpdate;
            var response = await _apiServices.postApi(postApi, value);
            ApiResponseModel<ApiCredentialsOutput> rslt = JsonConvert.DeserializeObject<ApiResponseModel<ApiCredentialsOutput>>(response);
            return rslt;
        }

        public async Task<ApiResponseModel<List<ApiCredentialsOutput>>> apiCredentialList()
        {
            var getApi = ApiCredentialsEndPoint.apiCredentialGetAll;
            var response = await _apiServices.getApiWeb(getApi);
            var rslt = await response.Content.ReadAsStringAsync();
            ApiResponseModel<List<ApiCredentialsOutput>> userData = JsonConvert.DeserializeObject<ApiResponseModel<List<ApiCredentialsOutput>>>(rslt);
            return new ApiResponseModel<List<ApiCredentialsOutput>>
            {
                succeed = true,
                message = "success",
                data = userData.data,
            };
        }
        public async Task<ApiResponseModel<ApiCredentialsOutput>> editApiCredential(int id)
        {
            var getByIdApi = ApiCredentialsEndPoint.apiCredentialGetById;
            var response = await _apiServices.getApiWeb($"{getByIdApi}/{id}");
            var rslt = await response.Content.ReadAsStringAsync();
            ApiResponseModel<ApiCredentialsOutput> cityId = JsonConvert.DeserializeObject<ApiResponseModel<ApiCredentialsOutput>>(rslt);
            return new ApiResponseModel<ApiCredentialsOutput>
            {
                succeed = true,
                message = "success",
                data = cityId.data
            };
        }
        public async Task<ApiResponseModel<bool>> deleteByIdApiCredential(int id)
        {
            var deleteApi = ApiCredentialsEndPoint.apiCredentialDeleteById;
            var response = await _apiServices.deleteApi($"{deleteApi}/{id}");
            return new ApiResponseModel<bool>
            {
                succeed = true,
                message = "success",
                data = true
            };
        }
    }
}

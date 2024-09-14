using Hrmanagement.Core.Constants;
using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.DTO.DtoOutput;
using Hrmanagement.Core.Models;
using Hrmanagement.Services.Interfaces;
using Newtonsoft.Json;

namespace Hrmanagement.Services
{
    public class MvcSystemSettingServices : IMvcSystemSettingServices
    {
        public IApiServices _apiServices;
        public IWebHostEnvironment _environment;
        public MvcSystemSettingServices(IApiServices apiServices, IWebHostEnvironment environment)
        {
            _apiServices = apiServices;
            _environment = environment;
        }
        public async Task<ApiResponseModel<List<SystemSettingOutput>>> getSystemSetting()
        {
            var getApi = SystemSettingEndPoint.systemSettingGetAll;
            //var response = await _apiService.getApi($"{getApi}?record={record}&page={page}");
            var response = await _apiServices.getApiWeb(getApi);
            var rslt = await response.Content.ReadAsStringAsync();
            ApiResponseModel<List<SystemSettingOutput>> countryList = JsonConvert.DeserializeObject<ApiResponseModel<List<SystemSettingOutput>>>(rslt);
            return new ApiResponseModel<List<SystemSettingOutput>>
            {
                succeed = true,
                message = "success",
                data = countryList.data
               
            };
        }
        public async Task<ApiResponseModel<SystemSettingOutput>> AddUpdateSystemSetting(SystemSettingInput model)
        {
            var postApi = SystemSettingEndPoint.systemSettingAddUpdate;
            var response = await _apiServices.postApi(postApi, model);
            ApiResponseModel<SystemSettingOutput> rslt = JsonConvert.DeserializeObject<ApiResponseModel<SystemSettingOutput>>(response);
            return rslt;
        }
        public async Task<ApiResponseModel<SystemSettingOutput>> editSystemSetting(int id)
        {
            var getByIdApi = SystemSettingEndPoint.systemSettingGetById;
            var response = await _apiServices.getApiWeb($"{getByIdApi}/{id}");
            var rslt = await response.Content.ReadAsStringAsync();
            ApiResponseModel<SystemSettingOutput> stateId = JsonConvert.DeserializeObject<ApiResponseModel<SystemSettingOutput>>(rslt);
            return new ApiResponseModel<SystemSettingOutput>
            {
                succeed = true,
                message = "success",
                data = stateId.data
            };
        }
        public async Task<ApiResponseModel<bool>> deleteById(int id)
        {
            var deleteApi = SystemSettingEndPoint.systemSettingDeleteById;

            var response = await _apiServices.deleteApi($"{deleteApi}/{id}");
            return new ApiResponseModel<bool>
            {
                succeed = true,
                message = "success",
                data = true
            };
        }
        public async Task<ApiResponseModel<bool>> activeInActive(int id)
        {
            var activeInActiveApi = SystemSettingEndPoint.systemSettingActiveInActive;
            var response = await _apiServices.putApi($"{activeInActiveApi}/{id}");
            var rslt = await response.Content.ReadAsStringAsync();
            ApiResponseModel<bool> stateId = JsonConvert.DeserializeObject<ApiResponseModel<bool>>(rslt);
            return new ApiResponseModel<bool>
            {
                succeed = true,
                message = "success",
                data = stateId.data
            };
        }
    }
}

using Hrmanagement.Core.Constants;
using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.DTO.DtoOutput;
using Hrmanagement.Core.Models;
using Hrmanagement.Service.Interfaces;
using Hrmanagement.Services.Interfaces;
using Newtonsoft.Json;

namespace Hrmanagement.Services
{
    public class UserServices : IUserServices
    {
        public IApiServices _apiService;
        public UserServices(IApiServices apiService)
        {
            _apiService = apiService;
        }
        public async Task<ApiResponseModel<UserOutput>> addUpdateUser(UserInput value)
        {
            var postApi = UserEndPoint.userAddUpdate;
            var response = await _apiService.PostFormData(postApi, value);
            ApiResponseModel<UserOutput> rslt = JsonConvert.DeserializeObject<ApiResponseModel<UserOutput>>(response);
            return rslt;
        }
        public async Task<ApiResponseModel<List<UserOutput>>> userList()
        {
            var getApi = UserEndPoint.userGetAll;
            var response = await _apiService.getApiWeb(getApi);
           // var response = await _apiService.getApi(getApi);
            var rslt = await response.Content.ReadAsStringAsync();
            ApiResponseModel<List<UserOutput>> userData = JsonConvert.DeserializeObject<ApiResponseModel<List<UserOutput>>>(rslt);
            return new ApiResponseModel<List<UserOutput>>
            {
                succeed = true,
                message = "success",
                data = userData.data,
            };
        }

        public async Task<ApiResponseModel<UserOutput>> editUser(int id)
        {
            var getByIdApi = UserEndPoint.userGetById;
            var response = await _apiService.getApiWeb($"{getByIdApi}/{id}");
            var rslt = await response.Content.ReadAsStringAsync();
            ApiResponseModel<UserOutput> cityId = JsonConvert.DeserializeObject<ApiResponseModel<UserOutput>>(rslt);
            return new ApiResponseModel<UserOutput>
            {
                succeed = true,
                message = "success",
                data = cityId.data
            };
        }
        public async Task<ApiResponseModel<UserOutput>> getUserById()
        {
            int id = 0;
            var getByIdApi = UserEndPoint.userGetById;
            var response = await _apiService.getApiWeb($"{getByIdApi}/{id}");
            var rslt = await response.Content.ReadAsStringAsync();
            ApiResponseModel<UserOutput> cityId = JsonConvert.DeserializeObject<ApiResponseModel<UserOutput>>(rslt);
            return new ApiResponseModel<UserOutput>
            {
                succeed = true,
                message = "success",
                data = cityId.data
            };
        }
        public async Task<ApiResponseModel<bool>> deleteByIdUser(int id)
        {
            var deleteApi = UserEndPoint.userDeleteById;
            var response = await _apiService.deleteApi($"{deleteApi}/{id}");
            return new ApiResponseModel<bool>
            {
                succeed = true,
                message = "success",
                data = true
            };
        }
        public async Task<ApiResponseModel<bool>> activeInActiveUser(int id)
        {
            var activeInActiveApi = UserEndPoint.userActiveInActive;
            var response = await _apiService.putApi($"{activeInActiveApi}/{id}");
            var rslt = await response.Content.ReadAsStringAsync();
            ApiResponseModel<bool> stateId = JsonConvert.DeserializeObject<ApiResponseModel<bool>>(rslt);
            return new ApiResponseModel<bool>
            {
                succeed = true,
                message = "success",
                data = stateId.data
            };
        }


        //    gd changepassword work 
        public async Task<ApiResponseModel<bool>> changepassword(ChangePasswordInput model)
        {
            var changePassword = UserEndPoint.userchangepassword;
            var response = await _apiService.putApi($"{changePassword}/{model}");
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

using Hrmanagement.Core.Constants;
using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.DTO.DtoOutput;
using Hrmanagement.Core.Models;
using Hrmanagement.Services.Interfaces;
using Newtonsoft.Json;

namespace Hrmanagement.Services
{
    public class MvcUserMemoServices : IMvcUserMemoServices
    {
        public IApiServices _apiServices;
        public MvcUserMemoServices(IApiServices apiServices)
        {
            _apiServices = apiServices;
        }
        public async Task<ApiResponseModel<UserMemoOutput>> addUpdateUserMemo(UserMemoInput value)
        {
            var postApi = UserMemoEndpoint.userMemoAddUpdate;
            var response = await _apiServices.postApi(postApi, value);
            ApiResponseModel<UserMemoOutput> rslt = JsonConvert.DeserializeObject<ApiResponseModel<UserMemoOutput>>(response);
            return rslt;
        }
        public async Task<ApiResponseModel<List<UserMemoOutput>>> getUserMemoById(int userId)
        {
            //int userId = 0;
            var getApi = UserMemoEndpoint.UserMemoGetAll;
            var response = await _apiServices.getApiWeb($"{getApi}?userId={userId}");
            var rslt = await response.Content.ReadAsStringAsync();
            ApiResponseModel<List<UserMemoOutput>> userData = JsonConvert.DeserializeObject<ApiResponseModel<List<UserMemoOutput>>>(rslt);
            return new ApiResponseModel<List<UserMemoOutput>>
            {
                succeed = true,
                message = "success",
                data = userData.data,
            };
        }

        //public async Task<ApiResponseModel<List<UserMemoOutput>>>getUserMemoPublic()
        //{
        //    var getApi = UserMemoEndpoint.userMemoGetAllForPublic;
        //    var response = await _apiServices.getApiWeb(getApi);
        //    var rslt = await response.Content.ReadAsStringAsync();
        //    ApiResponseModel<List<UserMemoOutput>>userData = JsonConvert.DeserializeObject<ApiResponseModel<List<UserMemoOutput >>>(rslt);
        //    return new ApiResponseModel<List<UserMemoOutput>>
        //    {
        //        succeed = true,
        //        message = "success",
        //        data = userData.data,
        //    };
        //}

        public async Task<ApiResponseModel<List<UserMemoOutput>>> getUserMemoPublic()
        {
            var getApi = UserMemoEndpoint.userMemoGetAllForPublic;
            var response = await _apiServices.getApiWeb(getApi);
            var rslt = await response.Content.ReadAsStringAsync();
            ApiResponseModel<List<UserMemoOutput>> userData = JsonConvert.DeserializeObject<ApiResponseModel<List<UserMemoOutput>>>(rslt);
            return new ApiResponseModel<List<UserMemoOutput>>
            {
                succeed = true,
                message = "success",
                data = userData.data,
            };
        }

        public async Task<ApiResponseModel<UserMemoOutput>> editUserMemo(int id)
        {
            var getByIdApi = UserMemoEndpoint.userMemoGetById;
            var response = await _apiServices.getApiWeb($"{getByIdApi}/{id}");
            var rslt = await response.Content.ReadAsStringAsync();
            ApiResponseModel<UserMemoOutput> cityId = JsonConvert.DeserializeObject<ApiResponseModel<UserMemoOutput>>(rslt);
            return new ApiResponseModel<UserMemoOutput>
            {
                succeed = true,
                message = "success",
                data = cityId.data
            };
        }
        public async Task<ApiResponseModel<bool>> deleteByIdUserMemo(int id)
        {
            var deleteApi = UserMemoEndpoint.userMemoDeletebyId;
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

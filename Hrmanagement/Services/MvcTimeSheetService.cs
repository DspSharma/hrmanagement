using Hrmanagement.Core.Constants;
using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.DTO.DtoOutput;
using Hrmanagement.Core.Models;
using Hrmanagement.Services.Interfaces;
using Newtonsoft.Json;

namespace Hrmanagement.Services
{
    public class MvcTimeSheetService : IMvcTimeSheetService
    {
        public IApiServices _apiServices;
        public MvcTimeSheetService(IApiServices apiServices)
        {
            _apiServices = apiServices;
        }

        public async Task<ApiResponseModel<TimeSheetOutput>> addUpdateTimeSheet(TimeSheetInput value)
        {
            var postApi = TimeSheetEndPoint.timeSheetAddUpdate;
            var response = await _apiServices.postApi(postApi, value);
            ApiResponseModel<TimeSheetOutput> rslt = JsonConvert.DeserializeObject<ApiResponseModel<TimeSheetOutput>>(response);
            return rslt;
        }

        public async Task<ApiResponseModel<List<TimeSheetOutput>>>timeSheetList(int userid)
        {
            
            var getApi = TimeSheetEndPoint.timeSheetGetAll;
            var response = await _apiServices.getApiWeb($"{getApi}?userid={userid}");
            //var response = await _apiServices.getApiWeb(getApi);
            var rslt = await response.Content.ReadAsStringAsync();
            ApiResponseModel<List<TimeSheetOutput>> userData = JsonConvert.DeserializeObject<ApiResponseModel<List<TimeSheetOutput>>>(rslt);
            return new ApiResponseModel<List<TimeSheetOutput>>
            {
                succeed = true,
                message = "success",
                data = userData.data,
            };
        }

        public async Task<ApiResponseModel<TimeSheetOutput>> editTimeSheet(int id)
        {
            var getByIdApi = TimeSheetEndPoint.timeSheetGetById;
            var response = await _apiServices.getApiWeb($"{getByIdApi}/{id}");
            var rslt = await response.Content.ReadAsStringAsync();
            ApiResponseModel<TimeSheetOutput> timeSheetId = JsonConvert.DeserializeObject<ApiResponseModel<TimeSheetOutput>>(rslt);
            return new ApiResponseModel<TimeSheetOutput>
            {
                succeed = true,
                message = "success",
                data = timeSheetId.data
            };
        }
        public async Task<ApiResponseModel<bool>>timeSheetdeleteById(int id)
        {
            var deleteApi = TimeSheetEndPoint.timeSheetDeleteById;
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

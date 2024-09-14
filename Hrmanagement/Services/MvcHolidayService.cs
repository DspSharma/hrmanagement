using Hrmanagement.Core.Constants;
using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.DTO.DtoOutput;
using Hrmanagement.Core.Models;
using Hrmanagement.Services.Interfaces;
using Newtonsoft.Json;

namespace Hrmanagement.Services
{
    public class MvcHolidayService : IMvcHolidayService
    {
        public IApiServices _apiServices;
        public MvcHolidayService(IApiServices apiServices)
        {
            _apiServices = apiServices;
        }
        public async Task<ApiResponseModel<HolidayOutput>> addUpdateHoliday(HolidayInput value)
        {
            var postApi = HolidayEndPoint.holidayAddUpdate;
            var response = await _apiServices.postApi(postApi, value);
            ApiResponseModel<HolidayOutput> rslt = JsonConvert.DeserializeObject<ApiResponseModel<HolidayOutput>>(response);
            return rslt;
        }
        public async Task<ApiResponseModel<List<HolidayOutput>>> holidayList()
        {
            var getApi = HolidayEndPoint.holidayGetAll;
            var response = await _apiServices.getApiWeb(getApi);
            var rslt = await response.Content.ReadAsStringAsync();
            ApiResponseModel<List<HolidayOutput>> userData = JsonConvert.DeserializeObject<ApiResponseModel<List<HolidayOutput>>>(rslt);
            return new ApiResponseModel<List<HolidayOutput>>
            {
                succeed = true,
                message = "success",
                data = userData.data,
            };
        }
        public async Task<ApiResponseModel<HolidayOutput>> editHoliday(int id)
        {
            var getByIdApi = HolidayEndPoint.holidayGetById;
            var response = await _apiServices.getApiWeb($"{getByIdApi}/{id}");
            var rslt = await response.Content.ReadAsStringAsync();
            ApiResponseModel<HolidayOutput> cityId = JsonConvert.DeserializeObject<ApiResponseModel<HolidayOutput>>(rslt);
            return new ApiResponseModel<HolidayOutput>
            {
                succeed = true,
                message = "success",
                data = cityId.data
            };
        }
        public async Task<ApiResponseModel<bool>> deleteByIdHoliday(int id)
        {
            var deleteApi = HolidayEndPoint.holidayDeleteById;
            var response = await _apiServices.deleteApi($"{deleteApi}/{id}");
            return new ApiResponseModel<bool>
            {
                succeed = true,
                message = "success",
                data = true
            };
        }
        public async Task<ApiResponseModel<bool>> activeInActiveHoliday(int id)
        {
            var activeInActiveApi = HolidayEndPoint.holidayActiveInActive;
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

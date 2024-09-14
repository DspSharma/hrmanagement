using Hrmanagement.Core.Constants;
using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.DTO.DtoOutput;
using Hrmanagement.Core.Models;
using Hrmanagement.Data.Entities;
using Hrmanagement.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hrmanagement.Services
{
    public class MvcLeaveServices : IMvcLeaveServices
    {
        public IApiServices _apiServices;
        public MvcLeaveServices(IApiServices apiServices)
        {
            _apiServices = apiServices;
        }
        public async Task<ApiResponseModel<LeaveOutput>> addUpdateLeave(LeaveInput value)
        {
            var postApi = LeaveEndPoint.leaveAddUpdate;
            var response = await _apiServices.postApi(postApi, value);
            ApiResponseModel<LeaveOutput> rslt = JsonConvert.DeserializeObject<ApiResponseModel<LeaveOutput>>(response);
            return rslt;
        }
        public async Task<ApiResponseModel<List<LeaveOutput>>> LeaveList(string status)
        {
            var getApi = LeaveEndPoint.leaveGetAll;
            //var response = await _apiServices.getApiWeb(getApi);
            var response = await _apiServices.getApiWeb($"{getApi}?status={status}");
            var rslt = await response.Content.ReadAsStringAsync();
            ApiResponseModel<List<LeaveOutput>> userData = JsonConvert.DeserializeObject<ApiResponseModel<List<LeaveOutput>>>(rslt);
            return new ApiResponseModel<List<LeaveOutput>>
            {
                succeed = true,
                message = "success",
                data = userData.data,
            };
        }

        public async Task<ApiResponseModel<List<LeaveOutput>>> getLeaveUserById(int userId,int year,int month)
        {
            var getApi = LeaveEndPoint.leaveGetUserById;
            var response = await _apiServices.getApiWeb($"{getApi}?userId={userId}&year={year}&month={month}");
            //var response = await _apiServices.getApi($"{getApi}?userId={userId}&year={year}&month={month}");
            var rslt = await response.Content.ReadAsStringAsync();
            ApiResponseModel<List<LeaveOutput>> userData = JsonConvert.DeserializeObject<ApiResponseModel<List<LeaveOutput>>>(rslt);
            return new ApiResponseModel<List<LeaveOutput>>
            {
                succeed = true,
                message = "success",
                data = userData.data,
            };
        }

        public async Task<ApiResponseModel<LeaveOutput>> leaveById(int id)
        {
            var getByIdApi = LeaveEndPoint.leaveGetById;
            var response = await _apiServices.getApiWeb($"{getByIdApi}/{id}");
            var rslt = await response.Content.ReadAsStringAsync();
            ApiResponseModel<LeaveOutput> cityId = JsonConvert.DeserializeObject<ApiResponseModel<LeaveOutput>>(rslt);
            return new ApiResponseModel<LeaveOutput>
            {
                succeed = true,
                message = "success",
                data = cityId.data
            };
        }

        public async Task<ApiResponseModel<LeaveOutput>> updateLeavestatus(int id, bool IsApproved, bool IsRejected, string Remark)
        {
            var postApi = LeaveEndPoint.leaveUpdateStatus;
            var response = await _apiServices.PostApiById($"{postApi}?id={id}&isApproved={IsApproved}&isRejected={IsRejected}&remark={Remark}");
            //var response = await _apiServices.PostApiById($"{postApi}?id={id}");
            var rslt = await response.Content.ReadAsStringAsync();
            ApiResponseModel<LeaveOutput> cityId = JsonConvert.DeserializeObject<ApiResponseModel<LeaveOutput>>(rslt);
            return new ApiResponseModel<LeaveOutput>
            {
                succeed = true,
                message = "success",
                data = cityId.data
            };

        }

    }
}

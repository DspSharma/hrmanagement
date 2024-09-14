using Hrmanagement.Core.Constants;
using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.DTO.DtoOutput;
using Hrmanagement.Core.Models;
using Hrmanagement.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hrmanagement.Services
{
    public class MvcAttendanceServices : IMvcAttendanceServices
    {
        public IApiServices _apiServices;
        public MvcAttendanceServices(IApiServices apiServices)
        {
            _apiServices = apiServices;
        }
        //public async Task<ApiResponseModel<AttendanceOutput>> addUpdateAttendance(int id)
        //{
        //    var postApi = AttendanceEndPoint.attendanceAddUpdate;
        //    var response = await _apiServices.PostApiById($"{postApi}?id={id}");
        //    var rslt = await response.Content.ReadAsStringAsync();
        //    ApiResponseModel<AttendanceOutput> attendance = JsonConvert.DeserializeObject<ApiResponseModel<AttendanceOutput>>(rslt);
        //    return attendance;
        //    //return new ApiResponseModel<AttendanceOutput>
        //    //{
        //    //    succeed = true,
        //    //    message = "success",
        //    //    data = cityId.data
        //    //};
        //}


        public async Task<ApiResponseModel<AttendanceOutput>> addUpdateAttendance(AttendanceInput value)
        {
            var postApi = AttendanceEndPoint.attendanceAddUpdate;
            var response = await _apiServices.postApi(postApi, value);
            //var response = await _apiServices.PostApiById($"{postApi}?id={id}");
            //var rslt = await response.Content.ReadAsStringAsync();
            ApiResponseModel<AttendanceOutput> attendance = JsonConvert.DeserializeObject<ApiResponseModel<AttendanceOutput>>(response);
            return attendance;
            //return new ApiResponseModel<AttendanceOutput>
            //{
            //    succeed = true,
            //    message = "success",
            //    data = cityId.data
            //};
        }

        public async Task<ApiResponseModel<AttendanceOutput>> attendanceById(int id)
        {
            var getByIdApi = AttendanceEndPoint.attendanceGetById;
            var response = await _apiServices.getApiWeb($"{getByIdApi}/{id}");
            var rslt = await response.Content.ReadAsStringAsync();
            ApiResponseModel<AttendanceOutput> attendanceId = JsonConvert.DeserializeObject<ApiResponseModel<AttendanceOutput>>(rslt);
            return new ApiResponseModel<AttendanceOutput>
            {
                succeed = true,
                message = "success",
                data = attendanceId.data
            };
        }

        public async Task<ApiResponseModel<AttendanceOutput>> addUpdateAttendanceAdmin(AttendanceInput value)
        {
            var postApi = AttendanceEndPoint.attendanceAddUpdateAdmin;
            var response = await _apiServices.postApi(postApi, value);
            ApiResponseModel<AttendanceOutput> rslt = JsonConvert.DeserializeObject<ApiResponseModel<AttendanceOutput>>(response);
            return rslt;
        }
        public async Task<ApiResponseModel<AttendanceModels>> attendanceGetUserBy(int id, int year, int month)
        {
            var getApi = AttendanceEndPoint.attendanceGetUserBy;
            var response = await _apiServices.getApiWeb($"{getApi}?id={id}&year={year}&month={month}");
            var rslt = await response.Content.ReadAsStringAsync();
            ApiResponseModel<AttendanceModels> userData = JsonConvert.DeserializeObject<ApiResponseModel<AttendanceModels>>(rslt);
            return new ApiResponseModel<AttendanceModels>
            {
                succeed = true,
                message = "success",
                data = userData.data,
            };
        }
        public async Task<ApiResponseModel<AttendanceOutput>> attendanceGetMonthSummary()
        {
            var getApi = AttendanceEndPoint.attendanceGetMonthSummary;
            var response = await _apiServices.getApiWeb(getApi);
            //var response = await _apiServices.getApi($"{getApi}?userId={id}");
            var rslt = await response.Content.ReadAsStringAsync();
            ApiResponseModel<AttendanceOutput> userData = JsonConvert.DeserializeObject<ApiResponseModel<AttendanceOutput>>(rslt);
            return new ApiResponseModel<AttendanceOutput>
            {
                succeed = true,
                message = "success",
                data = userData.data,
            };
        }

        //public async Task<ApiResponseModel<List<PartialLeaveOutput>>> partialLeaveList()
        //{
        //    var getApi = AttendanceEndPoint.attendanceGetPartialLeave;
        //    var response = await _apiServices.getApiWeb(getApi);
        //    //var response = await _apiServices.getApi($"{getApi}?userId={id}");
        //    var rslt = await response.Content.ReadAsStringAsync();
        //    ApiResponseModel<List<PartialLeaveOutput>> userData = JsonConvert.DeserializeObject<ApiResponseModel<List<PartialLeaveOutput>>>(rslt);
        //    return new ApiResponseModel<List<PartialLeaveOutput>>
        //    {
        //        succeed = true,
        //        message = "success",
        //        data = userData.data,
        //    };
        //}

        public async Task<ApiResponseModel<List<PartialLeaveOutput>>> partialLeaveList(int id)
        {
            var getApi = AttendanceEndPoint.attendanceGetPartialLeave;
            //var response = await _apiServices.getApiWeb(getApi);
            var response = await _apiServices.getApiWeb($"{getApi}?userId={id}");
            var rslt = await response.Content.ReadAsStringAsync();
            ApiResponseModel<List<PartialLeaveOutput>> userData = JsonConvert.DeserializeObject<ApiResponseModel<List<PartialLeaveOutput>>>(rslt);
            return new ApiResponseModel<List<PartialLeaveOutput>>
            {
                succeed = true,
                message = "success",
                data = userData.data,
            };
        }


        public async Task<ApiResponseModel<PartialLeaveOutput>> AddUpdatePartialLeave(PartialLeaveInput value)
        {
            var postApi = AttendanceEndPoint.attendanceAddUpdatePartialLeave;
            var response = await _apiServices.postApi(postApi, value);
            ApiResponseModel<PartialLeaveOutput> rslt = JsonConvert.DeserializeObject<ApiResponseModel<PartialLeaveOutput>>(response);
            return rslt;
        }

      

    }
}

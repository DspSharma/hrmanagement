using Hrmanagement.Core.Constants;
using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.DTO.DtoOutput;
using Hrmanagement.Core.Helper;
using Hrmanagement.Core.Models;
using Hrmanagement.Services.Interfaces;
using Newtonsoft.Json;

namespace Hrmanagement.Services
{
    public class AuthServices : IAuthServices
    {
        public IApiServices _apiServices;
        public AuthServices(IApiServices apiServices)
        {
            _apiServices = apiServices;
        }

        public async Task<ApiResponseModel<LoggedInUserModel>> Login(UserLoginInput value)
        {
            var postApi = AuthEndPoint.TokenLogin;
            var response = await _apiServices.postApi(postApi, value);
            ApiResponseModel<LoggedInUserModel> rslt = JsonConvert.DeserializeObject<ApiResponseModel<LoggedInUserModel>>(response);
            return rslt;
        }
        public async Task<ApiResponseModel<bool>>forgotPassword(ForgotPasswordInput value)
        {
            var postApi = AuthEndPoint.forgotPassword;
            var response = await _apiServices.postApi(postApi, value);
            ApiResponseModel<bool> rslt = JsonConvert.DeserializeObject<ApiResponseModel<bool>>(response);
            return rslt;
        }
        public async Task<ApiResponseModel<ResetPasswordOutput>> resetPassword(string Token, string toEmail)
        {
            var getByIdApi = AuthEndPoint.getForgotPassWord;
            var response = await _apiServices.getApiWeb($"{getByIdApi}?Token={Token}&toEmail={toEmail}");
            var rslt = await response.Content.ReadAsStringAsync();
            ApiResponseModel<ResetPasswordOutput> cityId = JsonConvert.DeserializeObject<ApiResponseModel<ResetPasswordOutput>>(rslt);
            return cityId;
        }
        public async Task<ApiResponseModel<ResetPasswordOutput>> resetPasswordUpdate(ResetPasswordInput value)
        {
            var postApi = AuthEndPoint.addUpdateResetPassword;
            var response = await _apiServices.postApi(postApi, value);
            ApiResponseModel<ResetPasswordOutput> rslt = JsonConvert.DeserializeObject<ApiResponseModel<ResetPasswordOutput>>(response);
            return rslt;
        }
    }
}

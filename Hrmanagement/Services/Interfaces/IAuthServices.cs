using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.DTO.DtoOutput;
using Hrmanagement.Core.Models;

namespace Hrmanagement.Services.Interfaces
{
    public interface IAuthServices
    {
       Task<ApiResponseModel<LoggedInUserModel>> Login(UserLoginInput value);
       // Task<ApiResponseModel<UserOutput>> Login(UserLoginInput value);
        Task<ApiResponseModel<bool>> forgotPassword(ForgotPasswordInput value);
        Task<ApiResponseModel<ResetPasswordOutput>> resetPassword(string Token, string toEmail);
        Task<ApiResponseModel<ResetPasswordOutput>> resetPasswordUpdate(ResetPasswordInput value);
    }
}

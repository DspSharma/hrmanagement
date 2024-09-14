using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.DTO.DtoOutput;
using Hrmanagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Service.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponseModel<LoggedInUserModel>> login(UserLoginInput model);
        Task<ApiResponseModel<bool>> forgotPassword(ForgotPasswordInput model);
        Task<ApiResponseModel<ResetPasswordOutput>> resetPasswordUpdate(ResetPasswordInput value);
        Task<ApiResponseModel<ResetPasswordOutput>> resetPassword(string Token, string toEmail);
    }
}

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
    public interface IUserService
    {
        Task<ApiResponseModel<UserOutput>> AddUpdateUser(UserInput model);
        Task<ApiResponseModel<List<UserOutput>>> getAllUser();
        Task<ApiResponseModel<bool>> DeleteById(int id);
        Task<ApiResponseModel<bool>> ActiveInActive(int id);
        Task<ApiResponseModel<UserOutput>> getUserById(int id);
        //Task<ApiResponseModel<UserOutput>> login(UserLoginInput value);
        Task<ApiResponseModel<bool>> ChangePassword(ChangePasswordInput model, int userId);

    }
}

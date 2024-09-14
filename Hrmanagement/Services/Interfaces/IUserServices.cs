using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.DTO.DtoOutput;
using Hrmanagement.Core.Models;

namespace Hrmanagement.Services.Interfaces
{
    public interface IUserServices
    {
        Task<ApiResponseModel<UserOutput>> addUpdateUser(UserInput value);
        Task<ApiResponseModel<List<UserOutput>>> userList();
        //Task<ApiResponseModel<List<UserOutput>>> userList();
        Task<ApiResponseModel<bool>> deleteByIdUser(int id);
        Task<ApiResponseModel<UserOutput>> editUser(int id);
        Task<ApiResponseModel<bool>> activeInActiveUser(int id);
        Task<ApiResponseModel<UserOutput>> getUserById();
        Task<ApiResponseModel<bool>> changepassword(ChangePasswordInput model);


    }
}

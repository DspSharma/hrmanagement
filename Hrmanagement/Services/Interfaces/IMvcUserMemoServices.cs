using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.DTO.DtoOutput;
using Hrmanagement.Core.Models;

namespace Hrmanagement.Services.Interfaces
{
    public interface IMvcUserMemoServices
    {
        Task<ApiResponseModel<UserMemoOutput>> addUpdateUserMemo(UserMemoInput value);
        Task<ApiResponseModel<List<UserMemoOutput>>> getUserMemoById(int userId);
        Task<ApiResponseModel<UserMemoOutput>> editUserMemo(int id);
        Task<ApiResponseModel<bool>> deleteByIdUserMemo(int id);
        Task<ApiResponseModel<List<UserMemoOutput>>> getUserMemoPublic();


    }
}

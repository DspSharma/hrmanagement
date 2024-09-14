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
    public interface IUserMemoService
    {
        Task<ApiResponseModel<UserMemoOutput>> AddUpdateUserMemo(UserMemoInput model);
        Task<ApiResponseModel<List<UserMemoOutput>>> getAllUserMemo(int userId);
        Task<ApiResponseModel<bool>> DeleteUserMemoById(int id);
        Task<ApiResponseModel<UserMemoOutput>> GetUserMemoByid(int id);
        Task<ApiResponseModel<bool>> AvailableForPublic(int id);
        Task<ApiResponseModel<List<UserMemoOutput>>> getAllMemos();
    }
}

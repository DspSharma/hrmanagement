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
    public interface IPartialLeaveService
    {
        Task<ApiResponseModel<PartialLeaveOutput>> AddUpdatePartialLeave(PartialLeaveInput value);
        Task<ApiResponseModel<List<PartialLeaveOutput>>> GetAllPartialLeaves(int userId);
    }
}

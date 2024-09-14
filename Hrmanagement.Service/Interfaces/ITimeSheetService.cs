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
    public interface ITimeSheetService
    {
        Task<ApiResponseModel<TimeSheetOutput>> AddTimeSheet(TimeSheetInput value);
        Task<ApiResponseModel<List<TimeSheetOutput>>> GetAllTimeSheet( int userId);
        Task<ApiResponseModel<TimeSheetOutput>> GetTimeSheetByid(int id);
        Task<ApiResponseModel<bool>> DeleteTimeSheetByid(int id);
    }
}

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
    public interface IHolidayService
    {
        Task<ApiResponseModel<HolidayOutput>> AddUpdateHoliday(HolidayInput value);
        Task<ApiResponseModel<bool>> ActiveInActive(int id);
        Task<ApiResponseModel<bool>> DeleteHolidayByid(int id);
        Task<ApiResponseModel<HolidayOutput>> GetHolidayByid(int id);
        Task<ApiResponseModel<List<HolidayOutput>>> GetAllHolidays();
    }
}

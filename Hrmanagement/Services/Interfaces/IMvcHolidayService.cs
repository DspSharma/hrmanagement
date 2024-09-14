using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.DTO.DtoOutput;
using Hrmanagement.Core.Models;

namespace Hrmanagement.Services.Interfaces
{
    public interface IMvcHolidayService
    {
        Task<ApiResponseModel<HolidayOutput>> addUpdateHoliday(HolidayInput value);
        Task<ApiResponseModel<List<HolidayOutput>>> holidayList();
        Task<ApiResponseModel<HolidayOutput>> editHoliday(int id);
        Task<ApiResponseModel<bool>> deleteByIdHoliday(int id);
        Task<ApiResponseModel<bool>> activeInActiveHoliday(int id);
    }
}

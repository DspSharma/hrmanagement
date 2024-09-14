using AutoMapper;
using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.DTO.DtoOutput;
using Hrmanagement.Core.Misc;
using Hrmanagement.Core.Models;
using Hrmanagement.Data.DBContext;
using Hrmanagement.Data.Entities;
using Hrmanagement.Data.UnitOfWork;
using Hrmanagement.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Service
{
    public class HolidayService : IHolidayService
    {
        public IUnitOfWork _unitOfWork;
        private readonly HrManagementContext _context;
        public IMapper _mapper;

        public HolidayService(HrManagementContext context, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        public async Task<ApiResponseModel<HolidayOutput>> AddUpdateHoliday(HolidayInput value)
        {

            try
            {
                Holiday formValue = _mapper.Map<Holiday>(value);

                // Extract year from the HolidayFromDate property
                int holidayYear = formValue.HolidayFromDate.Year;

                // Check if a holiday with the same title already exists in the same year
                Holiday isHolidayExists = _unitOfWork.Holiday.GetWhere(x => x.Id != formValue.Id && (x.Title == formValue.Title && x.HolidayFromDate.Year == holidayYear)).FirstOrDefault();
                if (isHolidayExists != null)
                {
                    return new ApiResponseModel<HolidayOutput>
                    {
                        succeed = false,
                        message = "Holiday with the same title already exists in the same year"
                    };
                }

                if (formValue.Id != 0)
                {
                    Holiday existingHoliday = await _unitOfWork.Holiday.GetByIdAsync(formValue.Id);
                    if (existingHoliday == null)
                    {
                        throw new Exception($"Holiday with ID {formValue.Id} was not found.");
                    }

                    // Update properties
                    existingHoliday.Title = formValue.Title;
                    existingHoliday.HolidayFromDate = formValue.HolidayFromDate;
                    existingHoliday.HolidayToDate = formValue.HolidayToDate;
                    existingHoliday.UpdatedAt = DateTime.UtcNow;
                    existingHoliday.IsActive = formValue.IsActive;
                    await _unitOfWork.SaveAsync();

                }
                else
                {
                   
                    formValue.IsActive = false;
                    formValue.CreatedAt = DateTime.UtcNow;
                    await _unitOfWork.Holiday.AddAsync(formValue);
                    await _unitOfWork.SaveAsync();
                }

                HolidayOutput holidayOutputs = _mapper.Map<HolidayOutput>(formValue);
                return new ApiResponseModel<HolidayOutput>
                {
                    succeed = true,
                    message = "Success",
                    data = holidayOutputs
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }
        public async Task<ApiResponseModel<List<HolidayOutput>>> GetAllHolidays()
        {
            try
            {

                List<Holiday> holidays = (await _unitOfWork.Holiday.GetAllAsync()).ToList();
                var holidayList = _mapper.Map<List<HolidayOutput>>(holidays);
                foreach (var holiday in holidayList)
                {
                    double totaldays = (holiday.HolidayToDate - holiday.HolidayFromDate).TotalDays + 1;
                    holiday.TotalDays = totaldays;
                }
                return new ApiResponseModel<List<HolidayOutput>>
                {
                    succeed = true,
                    message = "success",
                    data = holidayList,
                    
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponseModel<HolidayOutput>> GetHolidayByid(int id)
        {
            try
            {
                Holiday holiday = await _unitOfWork.Holiday.GetByIdAsync(id);
                if (holiday == null)
                {
                    throw new Exception($"holiday was not found.");
                }
                var holidayList = _mapper.Map<HolidayOutput>(holiday);
                return new ApiResponseModel<HolidayOutput>
                {
                    succeed = true,
                    message = "success",
                    data = holidayList
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponseModel<bool>> DeleteHolidayByid(int id)
        {
            try
            {
                Holiday holiday = await _unitOfWork.Holiday.GetByIdAsync(id);
                if (holiday == null)
                {
                    throw new Exception($"holiday was not found.");
                }
                _unitOfWork.Holiday.Remove(holiday);
                await _unitOfWork.SaveAsync();
                return new ApiResponseModel<bool>
                {
                    succeed = true,
                    message = "success",
                   
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponseModel<bool>> ActiveInActive(int id)
        {
            try
            {
                Holiday holiday = await _unitOfWork.Holiday.GetByIdAsync(id);
                if (holiday == null)
                {
                    throw new Exception($"holiday was not found.");
                }
                holiday.IsActive = !holiday.IsActive;
                await _unitOfWork.SaveAsync();
                return new ApiResponseModel<bool>
                {
                    succeed = true,
                    message = "success",
                    data = holiday.IsActive

                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

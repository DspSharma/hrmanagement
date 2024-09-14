using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrmanagement_Api.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
   
    public class HolidayController : ControllerBase
    {
        public IHolidayService _holidayService;

        public HolidayController(IHolidayService holidayService)
        {
            _holidayService = holidayService;
        }
        private object? Json(string message)
        {
            throw new NotImplementedException();
        }



        [HttpPost("AddUpdateHoliday")]
        public async Task<IActionResult> AddUpdateHoliday([FromBody] HolidayInput model)
        {
            try
            {
                var res = await _holidayService.AddUpdateHoliday(model);

                return Ok(res);
            }
            catch (System.Exception ex)
            {
                return BadRequest(Json(ex.Message));
            }

        }


        [HttpGet("GetAllHolidays")]
        public async Task<IActionResult> GetAllHolidays()
        {
            try
            {
                var res = await _holidayService.GetAllHolidays();

                return Ok(res);
            }
            catch (System.Exception ex)
            {
                return BadRequest(Json(ex.Message));
            }

        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetHolidayByid(int id)
        {
            try
            {
                var res = await _holidayService.GetHolidayByid( id);

                return Ok(res);
            }
            catch (System.Exception ex)
            {
                return BadRequest(Json(ex.Message));
            }

        }

        [HttpPut("ActiveInActive/{id}")]
        public async Task<IActionResult> ActiveInActive(int id)
        {
            try
            {
                var res = await _holidayService.ActiveInActive(id);

                return Ok(res);
            }
            catch (System.Exception ex)
            {
                return BadRequest(Json(ex.Message));
            }

        }


        [HttpDelete("Get/{id}")]
        public async Task<IActionResult> DeleteHolidayByid(int id)
        {
            try
            {
                var res = await _holidayService.DeleteHolidayByid(id);

                return Ok(res);
            }
            catch (System.Exception ex)
            {
                return BadRequest(Json(ex.Message));
            }

        }
    }
}

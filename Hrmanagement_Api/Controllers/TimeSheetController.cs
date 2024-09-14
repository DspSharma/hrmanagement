using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.Misc;
using Hrmanagement.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrmanagement_Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSheetController : ControllerBase
    {
        public ITimeSheetService _timeSheetService;

        public TimeSheetController(ITimeSheetService timeSheetService)
        {
            _timeSheetService = timeSheetService;
        }
        private object? Json(string message)
        {
            throw new NotImplementedException();
        }

        // [Authorize(Roles = "Public")]
        [HttpPost("AddUpdateTimeSheet")]
        public async Task<IActionResult> AddUpdateTimeSheet([FromBody] TimeSheetInput model)
        {
            try
            {
                var u = MiscMethods.getLoginDetailByToken(HttpContext);
                if (model.UserId == 0)
                    model.UserId = u.Id;

                var res = await _timeSheetService.AddTimeSheet(model);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GetAllTimeSheet")]
        public async Task<IActionResult> GetAllTimeSheet( int userid)
        {
            try
            {
                var u = MiscMethods.getLoginDetailByToken(HttpContext);
                if (userid == 0)
                    userid = u.Id;

                var res = await _timeSheetService.GetAllTimeSheet(userid);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetTimeSheetByid(int id)
        {
            try
            {
                var u = MiscMethods.getLoginDetailByToken(HttpContext);
                if (id == 0)
                    id = u.Id;

                var res = await _timeSheetService.GetTimeSheetByid(id);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteTimeSheetByid(int id)
        {
            try
            {
                var u = MiscMethods.getLoginDetailByToken(HttpContext);
                if (id == 0)
                    id = u.Id;

                var res = await _timeSheetService.DeleteTimeSheetByid(id);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}

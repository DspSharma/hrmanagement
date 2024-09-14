using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.Misc;
using Hrmanagement.Core.Models;
using Hrmanagement.Data.Entities;
using Hrmanagement.Service;
using Hrmanagement.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net.Http;


namespace Hrmanagement_Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")] 
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        public IAttendanceService _attendanceService;
        private readonly IHttpContextAccessor _httpContextAccessor;
      
        public AttendanceController(IAttendanceService attendanceService, IHttpContextAccessor httpContextAccessor)
        {
            _attendanceService = attendanceService;
            _httpContextAccessor = httpContextAccessor;
        }

        private object? Json(string message)
        {
            throw new NotImplementedException();
        }

        
        //[Authorize(Roles = "Public")]
        [HttpPost("AddUpdateAttendance")]
        public async Task<IActionResult> AddUpdateAttendance(AttendanceInput model)
        {
            try
            {
                var u = MiscMethods.getLoginDetailByToken(HttpContext);
                //string clientAddress = HttpContext.Request.get
                model.UserId = u.Id;
                if (model.UserId != null)
                {
                    var result = await _attendanceService.AddUpdateAttendance(model);
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new ApiResponseModel<bool>() { succeed = false, data = false, message = "User is not valid." });
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(Json(ex.Message));
            }

        }


        [Authorize(Roles = "Public,admin")]
        [HttpGet("GetAttendance")]
        public async Task<IActionResult> GetAttendance([FromQuery] int id ,[FromQuery] int year = 0, [FromQuery] int month = 0)
        {

            try
            {
                var loggedInUser = MiscMethods.getLoginDetailByToken(HttpContext);

                int userId;

                if (loggedInUser.Role == "admin")
                {
                    userId = id;
                    if(userId == 0)
                    {
                        var result = await _attendanceService.getAttendances(loggedInUser.Id, year, month);
                        return Ok(result);
                    }
                }
                else
                {
                    userId = loggedInUser.Id;
                }

                var res = await _attendanceService.getAttendances(userId, year, month);
                return Ok(res);
            }
            catch (System.Exception ex)
            {
                return BadRequest(Json(ex.Message));
            }
        }
       


        [Authorize(Roles = "Public,admin")]
        [HttpGet("GetMonthlyAttendanceSummary")]
        public async Task<IActionResult> GetMonthlyAttendanceSummary(int userId)
        {
            try
            {
                var u = MiscMethods.getLoginDetailByToken(HttpContext);
                if (userId == 0)
                    userId = u.Id;

                var res = await _attendanceService.GetMonthlyAttendanceSummary(userId);
                return Ok(res);
            }
            catch (System.Exception ex)
            {
                return BadRequest(Json(ex.Message));
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost("AddUpdateAttendanceByAdmin")]
        public async Task<IActionResult> AddUpdateAttendanceByAdmin([FromBody] AttendanceInput value)
        {
            try
            {
                var u = MiscMethods.getLoginDetailByToken(HttpContext);
                if (value.UserId == 0)
                    value.UserId = u.Id;

                var res = await _attendanceService.AddUpdateAttendanceByAdmin(value);

                return Ok(res);
            }
            catch (System.Exception ex)
            {
                return BadRequest(Json(ex.Message));
            }

        }

        [HttpGet("IsCheckedIn")]
        public async Task<IActionResult> IsCheckedIn()
        {
            try
            {
                var u = MiscMethods.getLoginDetailByToken(HttpContext);

                if (u != null)
                {
                    var result = await _attendanceService.IsCheckedIn(u.Id);
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new ApiResponseModel<bool>() { succeed = false, data = false, message = "User is not valid." });
                }

            }
            catch (System.Exception ex)
            {
                return BadRequest(Json(ex.Message));
            }

        }

        [HttpGet("IsPause")]
        public async Task<IActionResult> IsPause()
        {
            try
            {
                var u = MiscMethods.getLoginDetailByToken(HttpContext);

                if (u != null)
                {
                    var result = await _attendanceService.IsPause(u.Id);
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new ApiResponseModel<bool>() { succeed = false, data = false, message = "User is not valid." });
                }

            }
            catch (System.Exception ex)
            {
                return BadRequest(Json(ex.Message));
            }

        }


        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetAttendanceByid(int id)
        {
            try
            {
                var u = MiscMethods.getLoginDetailByToken(HttpContext);

                if (u != null)
                {
                    var result = await _attendanceService.GetAttendanceByid(id);
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new ApiResponseModel<bool>() { succeed = false, data = false, message = "User is not valid." });
                }

            }
            catch (System.Exception ex)
            {
                return BadRequest(Json(ex.Message));
            }

        }
        
        
    }
}

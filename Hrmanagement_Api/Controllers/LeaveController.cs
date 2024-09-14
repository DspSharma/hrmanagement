using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.Misc;
using Hrmanagement.Data.Entities;
using Hrmanagement.Service;
using Hrmanagement.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Hrmanagement_Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveController : ControllerBase
    {
        public ILeaveService _leaveService;

        public LeaveController(ILeaveService leaveService)
        {
            _leaveService = leaveService;
        }
        private object? Json(string message)
        {
            throw new NotImplementedException();
        }

        // [Authorize(Roles = "Public")]
        [HttpPost("AddLeave")]
        public async Task<IActionResult> AddLeave([FromBody] LeaveInput model)
        {
            try
            {
                var u = MiscMethods.getLoginDetailByToken(HttpContext);
               // if (userId == 0)
                    model.UserId = u.Id;

                var res = await _leaveService.AddLeave(model);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        // [Authorize(Roles = "Public,admin")]
        [HttpGet("GetAllUserlLeaves")]
        public async Task<IActionResult> GetAllUserlLeaves()
        {
            try
            {
                var res = await _leaveService.GetAllLeaves();

                return Ok(res);
            }
            catch (System.Exception ex)
            {
                return BadRequest(Json(ex.Message));
            }

        }


        [HttpGet("GetAllLeaves")]
        public async Task<IActionResult> GetAllLeaves(string status = "pending")
        {
            try
            {
                var res = await _leaveService.GetAllLeaves(status);

                return Ok(res);
            }
            catch (System.Exception ex)
            {
                return BadRequest(Json(ex.Message));
            }

        }

        //[HttpDelete("Get/{id}")]
        //public async Task<IActionResult> DeleteByIdLeave(int id )
        //{
        //    try
        //    {
        //        var res = await _leaveService.DeleteByIdLeave(id);

        //        return Ok(res);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return BadRequest(Json(ex.Message));
        //    }

        //}

       
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetLeaveById(int id)
        {
            try
            {
                var res = await _leaveService.GetLeaveById(id);

                return Ok(res);
            }
            catch (System.Exception ex)
            {
                return BadRequest(Json(ex.Message));
            }

        }


        [Authorize(Roles = "Public,admin")]
        [HttpGet("GetLeavesByUserId")]
        public async Task<IActionResult> GetLeavesByUserId(int userid, [FromQuery]int year=0,[FromQuery] int month=0)
        {
            try
            {
                var loggedInUser = MiscMethods.getLoginDetailByToken(HttpContext);

                int userId;

                if (loggedInUser.Role == "admin")
                {
                    userId = userid;
                    if (userId == 0)
                    {
                        var result = await _leaveService.GetLeavesByUserId(loggedInUser.Id, year, month);
                        return Ok(result);
                    }
                }
                else
                {
                    userId = loggedInUser.Id;
                }
                //var u = MiscMethods.getLoginDetailByToken(HttpContext);
                //if (userId == 0)
                //    userId = u.Id;

                var res = await _leaveService.GetLeavesByUserId(userId, year, month);

                return Ok(res);
            }
            catch (System.Exception ex)
            {
                return BadRequest(Json(ex.Message));
            }

        }

        [Authorize(Roles = "admin")]
        [HttpPost("updateLeavestatus")]

        public async Task<IActionResult> updateLeavestatus(int id, [FromQuery] bool IsApproved, [FromQuery] bool IsRejected, [FromQuery] string Remark = "null")
        {
            try
            {
                var res = await _leaveService.updateLeavestatus(id, IsApproved, IsRejected, Remark);
                return Ok(res);
            }
            catch (System.Exception ex)
            {
                return BadRequest(Json(ex.Message));
            }

        }
    }
}

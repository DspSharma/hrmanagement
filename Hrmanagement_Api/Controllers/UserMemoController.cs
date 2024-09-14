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

namespace Hrmanagement_Api.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserMemoController : ControllerBase
    {
        public IUserMemoService _userMemoService;

        public UserMemoController(IUserMemoService userMemoService)
        {
            _userMemoService = userMemoService;
        }

        private object? Json(string message)
        {
            throw new NotImplementedException();
        }

        [Authorize(Roles = "Public")]
        [HttpPost("AddUpdateUserMemo")]
        public async Task<IActionResult> AddUpdateUserMemo([FromBody] UserMemoInput model)
        {
            try
            {
                var u = MiscMethods.getLoginDetailByToken(HttpContext);

                model.UserId = u.Id;

                var res = await _userMemoService.AddUpdateUserMemo(model);

                return Ok(res);
            }
            catch (System.Exception ex)
            {
                return BadRequest(Json(ex.Message));
            }

        }

        [Authorize(Roles = "Public")]
        [HttpGet("getAllUserMemo")]
        public async Task<IActionResult> getAllUserMemo(int userId)
        {
            try
            {
                var u = MiscMethods.getLoginDetailByToken(HttpContext);
                if (userId == 0)
                    userId = u.Id;
                var res = await _userMemoService.getAllUserMemo(userId);

                return Ok(res);
            }
            catch (System.Exception ex)
            {
                return BadRequest(Json(ex.Message));
            }

        }

        [HttpDelete("Get/{id}")]
        public async Task<IActionResult> DeleteUserMemoById(int id)
        {
            try
            {
                var res = await _userMemoService.DeleteUserMemoById(id);
                return Ok(res);
            }
            catch (System.Exception ex)
            {

                return BadRequest(Json(ex.Message));
            }
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetUserMemoByid(int id)
        {
            try
            {
                var res = await _userMemoService.GetUserMemoByid(id);

                return Ok(res);
            }
            catch (System.Exception ex)
            {
                return BadRequest(Json(ex.Message));
            }

        }

        [HttpPut("AvailableForPublic")]
        public async Task<IActionResult> AvailableForPublic(int id)
        {
            try
            {
                var u = MiscMethods.getLoginDetailByToken(HttpContext);
                if (u != null)
                {
                    var res = await _userMemoService.AvailableForPublic(id);
                    return Ok(res);
                }
                else
                {
                    return BadRequest(new ApiResponseModel<bool>() { succeed = false, data = false, message = "User is not valid." });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseModel<bool>() { succeed = false, data = false, message = "An error occurred while processing the request." });
            }
        }

        [HttpGet("GetAllMemosForPublic")]
        public async Task<IActionResult> GetAllMemosForPublic()
        {
            try
            {
              
                var res = await _userMemoService.getAllMemos();

                return Ok(res);
            }
            catch (System.Exception ex)
            {
                return BadRequest(Json(ex.Message));
            }

        }
    }
}

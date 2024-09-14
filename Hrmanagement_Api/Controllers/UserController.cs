using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.Misc;
using Hrmanagement.Data.Entities;
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
    public class UserController : ControllerBase
    {
        public IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        private object? Json(string message)
        {
            throw new NotImplementedException();
        }


        [HttpPost("AddUpdateUser")]
        public async Task<IActionResult> AddUpdateUser([FromForm] UserInput model)
        {
            try
            {
                var res = await _userService.AddUpdateUser(model);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize(Roles = "admin")]
        [HttpGet("getAllUser")]
        public async Task<IActionResult> getAllUser()
        {
            try
            {
                var res = await _userService.getAllUser();

                return Ok(res);
            }
            catch (System.Exception ex)
            {
                return BadRequest(Json(ex.Message));
            }

        }

        [HttpDelete("Get/{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            try
            {
                var res = await _userService.DeleteById(id);
                return Ok(res);
            }
            catch (System.Exception ex)
            {

                return BadRequest(Json(ex.Message));
            }
        }

        [Authorize(Roles = "Public,admin")]
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> getUserById(int id)
        {
            try
            {
                var u = MiscMethods.getLoginDetailByToken(HttpContext);
                if (id == 0)
                    id = u.Id;

                var res = await _userService.getUserById(id);
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
                var res = await _userService.ActiveInActive(id);
                return Ok(res);
            }
            catch (System.Exception ex)
            {

                return BadRequest(Json(ex.Message));
            }
        }


        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordInput model)
        {
            try
            {
                var u = MiscMethods.getLoginDetailByToken(HttpContext);

                var rslt = await _userService.ChangePassword(model, u.Id);
                
                return Ok(rslt);

            }
            catch (System.Exception ex)
            {
                return BadRequest(Json(ex.Message));
            }

        }

      
    }
}

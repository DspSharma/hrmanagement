using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.Misc;
using Hrmanagement.Core.Models;
using Hrmanagement.Data.Entities;
using Hrmanagement.Service;
using Hrmanagement.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrmanagement_Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PartialLeaveController : ControllerBase
    {

        public IPartialLeaveService _partialLeaveService;

        public PartialLeaveController(IPartialLeaveService partialLeaveService)
        {
            _partialLeaveService = partialLeaveService;
        }

        [HttpPost("AddUpdatePartialLeave")]
        public async Task<IActionResult> AddUpdatePartialLeave(PartialLeaveInput value)
        {
            try
            {
                var u = MiscMethods.getLoginDetailByToken(HttpContext);
                value.UserId = u.Id;
                if (value.UserId != null)
                {
                    var result = await _partialLeaveService.AddUpdatePartialLeave(value);
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new ApiResponseModel<bool>() { succeed = false, data = false, message = "User is not valid." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GetAllPartialLeaves")]
        public async Task<IActionResult> GetAllPartialLeaves(int userid)
        {
            try
            {
                var u = MiscMethods.getLoginDetailByToken(HttpContext);
                if (userid == 0)
                    userid = u.Id;
                var res = await _partialLeaveService.GetAllPartialLeaves(userid);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

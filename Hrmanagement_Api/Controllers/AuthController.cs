using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.Helper;
using Hrmanagement.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Hrmanagement_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        private object? Json(string message)
        {
            throw new NotImplementedException();
        }


        [HttpPost("Login")]
        public async Task<IActionResult> login([FromBody] UserLoginInput model)
        {
            try
            {
                var res = await _authService.login(model);

                return Ok(res);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("forgotPassword")]
        public async Task<IActionResult> forgotPassword([FromBody] ForgotPasswordInput model)
        {
            try
            {
                var res = await _authService.forgotPassword(model);

                return Ok(res);
            }
            catch (System.Exception ex)
            {
                return BadRequest(Json(ex.Message));
            }

        }

        [HttpPost("resetPasswordUpdate")]
        public async Task<IActionResult> resetPasswordUpdate(ResetPasswordInput value)
        {
            try
            {
                var res = await _authService.resetPasswordUpdate(value);

                return Ok(res);
            }
            catch (System.Exception ex)
            {
                return BadRequest(Json(ex.Message));
            }

        }

        [HttpGet("resetPassword")]
        public async Task<IActionResult> resetPassword(string Token, string toEmail)
        {
            try
            {
                var res = await _authService.resetPassword(Token, toEmail);

                return Ok(res);
            }
            catch (System.Exception ex)
            {
                return BadRequest(Json(ex.Message));
            }

        }
    }
}

using Hrmanagement.Core.DTO.DtoInput;
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
    public class ApiCredentialsController : ControllerBase
    {

        public IApiCredentialsService _apiCredentialsService;

        public ApiCredentialsController(IApiCredentialsService apiCredentialsService)
        {
            _apiCredentialsService = apiCredentialsService;
        }


        [HttpPost("AddUpdateApiCredential")]

        public async Task <IActionResult> AddUpdateApiCredential(ApiCredentialsInput value)
        {
            try
            {
                var res = await _apiCredentialsService.AddUpdateApiCredential(value);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetAllApiCredential")]

        public async Task<IActionResult> GetAllApiCredential()
        {
            try
            {
                var res = await _apiCredentialsService.GetAllApiCredential();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Get/{id}")]

        public async Task<IActionResult> DeleteById(int id)
        {
            try
            {
                var res = await _apiCredentialsService.DeleteById(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetApiCredentialsByid(int id)
        {
            try
            {
                var res = await _apiCredentialsService.getApiCredentiaById(id);

                return Ok(res);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}

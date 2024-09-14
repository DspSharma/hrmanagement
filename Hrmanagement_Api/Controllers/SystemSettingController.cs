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
    public class SystemSettingController : ControllerBase
    {
        public ISystemSettingService _systemSettingService;

        public SystemSettingController(ISystemSettingService systemSettingService)
        {
            _systemSettingService = systemSettingService;
        }

        private object? Json(string message)
        {
            throw new NotImplementedException();
        }

        [HttpPost("AddUpdateSystemSetting")]
        public async Task<IActionResult> AddUpdateSystemSetting([FromBody] SystemSettingInput model)
        {
            try
            {
                var result = await _systemSettingService.AddUpdateSystemSetting(model);

                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(Json(ex.Message));
            }

        }

        [HttpGet("GetSystemSetting")]
        public async Task<IActionResult> GetAllSystemSetting()
        {
            try
            {
                var result = await _systemSettingService.GetAllSystemSetting();

                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(Json(ex.Message));
            }

        }

        [HttpDelete("Get/{id}")]
        public async Task<IActionResult> DeleteSystemSettingById(int id)
        {
            try
            {
                var result = await _systemSettingService.DeleteSystemSettingById(id);

                return Ok(result);
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
                var result = await _systemSettingService.ActiveInActive(id);

                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(Json(ex.Message));
            }

        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetSystemSettingById(int id)
        {
            try
            {
                var res = await _systemSettingService.GetSystemSettingById(id);

                return Ok(res);
            }
            catch (System.Exception ex)
            {
                return BadRequest(Json(ex.Message));
            }

        }
    }
}

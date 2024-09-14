using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Hrmanagement_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
       
        [HttpGet("getAllUser")]
        public async Task<IActionResult> getAllUser()
        {
            try
            {

                var result = "hello ";

                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest((ex.Message));
            }

        }
    }
}

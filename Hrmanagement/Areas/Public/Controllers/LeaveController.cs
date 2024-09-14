using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hrmanagement.Areas.Public.Controllers
{
    [Area("Public")]
    [CheckSessionIsAvailableController]
    public class LeaveController : Controller
    {
        public IMvcLeaveServices _leaveService;
        public LeaveController(IMvcLeaveServices leaveService)
        {
            _leaveService = leaveService;
        }
        public async Task<IActionResult> AddUpdateLeaveRequest([FromBody] LeaveInput value)
        {
            var rslt = await _leaveService.addUpdateLeave(value);
            if (rslt.succeed)
            {
                return Ok(rslt);
            }
            else
            {
                return Ok(rslt);
            }
        }

        public async Task<IActionResult> getUserLeave([FromQuery] int year = 0, [FromQuery] int month = 0)
        {
            int actualUserId = 0;
            var rslt = await _leaveService.getLeaveUserById(actualUserId, year, month);
            ViewData["userLeaveData"] = rslt.data;
            return View("~/Areas/Public/Views/Leave/getUserLeave.cshtml");
        }
        public async Task<IActionResult> leaveById(int id)
        {
            var rslt = await _leaveService.leaveById(id);
            ViewData["leaveData"] = rslt.data;
            return View("~/Areas/Public/Views/Leave/leaveById.cshtml");
        }
    }
}

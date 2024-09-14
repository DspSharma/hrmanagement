using Hrmanagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hrmanagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    [BaseController]
    public class LeaveController : Controller
    {
        public IMvcLeaveServices _leaveService;
        public LeaveController(IMvcLeaveServices leaveService)
        {
            _leaveService = leaveService;
        }

        public async Task<IActionResult>userLeaveList([FromQuery]string filter= "pending")
        {
            var rslt = await _leaveService.LeaveList(filter);
            //var leaveData = rslt.data;
            ViewData["leaveData"] = rslt.data;
            return View("~/Areas/Admin/Views/Leave/userLeaveList.cshtml");
        }
        public async Task<IActionResult>LeaveUserById(int id, [FromQuery] int year = 0, [FromQuery] int month = 0)
        {
            var rslt = await _leaveService.getLeaveUserById(id, year, month);
            ViewData["leaveUserData"] = rslt.data;
            return View("~/Areas/Admin/Views/Leave/LeaveUserById.cshtml");
        }
        public async Task<IActionResult> leaveById(int id)
        {
            var rslt = await _leaveService.leaveById(id);
            ViewData["leaveData"] = rslt.data;
            return View("~/Areas/Admin/Views/Leave/leaveById.cshtml");
        }

        public async Task<IActionResult> updateLeavestatus(int id, [FromQuery] bool IsApproved, [FromQuery] bool IsRejected, [FromQuery] string Remark = "Null")
        {

            var rslt = await _leaveService.updateLeavestatus(id, IsApproved, IsRejected, Remark);
            return Redirect(HttpContext.Request.Headers["Referer"]);
        }
    }
}

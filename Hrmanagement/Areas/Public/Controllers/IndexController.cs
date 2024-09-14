//using AspNetCore;
using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hrmanagement.Areas.Public.Controllers
{
    [Area("Public")]
    [CheckSessionIsAvailableController]
    public class IndexController : Controller
    {
        public IMvcAttendanceServices _attendanceServices;
        public IUserServices _userServices;
        public IndexController(IMvcAttendanceServices attendanceServices, IUserServices userServices)
        {
            _attendanceServices = attendanceServices;
            _userServices = userServices;
        }
        public async Task<IActionResult> Index()
        {
            var rslt = await _attendanceServices.attendanceGetMonthSummary();
            ViewData["attendancedata"] = rslt.data;
            return View("~/Areas/Public/Views/Index/Index.cshtml");
        }
        public async Task<IActionResult> userLayout()
        {
            var rslt = await _userServices.getUserById();
            return Ok(rslt);
        }
        public async Task<IActionResult> userProfile()
        {
            if (!ModelState.IsValid)
            {
                return View("~/Areas/Public/Views/Index/userProfile.cshtml");
            }
            else
            {
                var rslt = await _userServices.getUserById();
                ViewData["userData"] = rslt.data;
                return View("~/Areas/Public/Views/Index/userProfile.cshtml");
            }
        }
        public async Task<IActionResult> AddUpdateUserProfile([FromForm] UserInput value)
        {
            var rslt = await _userServices.addUpdateUser(value);
            if(rslt.succeed)
            {
                TempData["Success"] = $"{rslt.message}";
                return RedirectToAction("Index", "Index", new { area = "Public" });
            }
            else
            {
                TempData["error"] = $"{rslt.message}";
                return Redirect(HttpContext.Request.Headers["Referer"]);
            }
        }
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth", new { area = "Admin" });
        }

        //    gd changepassword work 
        public async Task<IActionResult> UserChangePassword(ChangePasswordInput value)
        {
            var rslt = await _userServices.changepassword(value);
            if (rslt.succeed)
            {
                TempData["Success"] = $"{rslt.message}";
                return RedirectToAction("Index", "Index", new { area = "Public" });
            }
            else
            {
                TempData["error"] = $"{rslt.message}";
                return Redirect(HttpContext.Request.Headers["Referer"]);
            }
        }

        public async Task<IActionResult> latLongWork()
        {
            return View("~/Areas/Public/Views/Index/latLongWork.cshtml");
        }
    }
}

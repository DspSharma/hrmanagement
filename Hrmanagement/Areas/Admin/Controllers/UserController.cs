using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hrmanagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    [BaseController]
    public class UserController : Controller
    {
        public IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        public async Task<IActionResult> AddUpdateUserForm(int id)
        {
            if(!ModelState.IsValid)
            {
                return View("~/Areas/Admin/Views/User/AddUpdateUserForm.cshtml");
            }
            else
            {
                if(id !=0)
                {
                    var rslt = await _userServices.editUser(id);
                    ViewData["user"] = rslt.data;
                    return View("~/Areas/Admin/Views/User/AddUpdateUserForm.cshtml");
                }
                else
                {
                    return View("~/Areas/Admin/Views/User/AddUpdateUserForm.cshtml");
                }
            }
        }
        public async Task<IActionResult> AddUpdateUser([FromForm] UserInput model)
        {
            var rslt = await _userServices.addUpdateUser(model);
            if(rslt.succeed)
            {
                TempData["Success"] = $"{rslt.message}";
                return RedirectToAction("userList", "User", new { area = "Admin" });
                // return RedirectToAction("userList", "User");
            }
            else
            {
                TempData["error"] = $"{rslt.message}";
                return Redirect(HttpContext.Request.Headers["Referer"]);
            }
        }

        public async Task<IActionResult>userList([FromQuery] bool isActive = true)
        {
             var rslt = await _userServices.userList();
            
            ViewData["userData"] = rslt.data;
            ViewData["isActive"] = isActive;
            return View("~/Areas/Admin/Views/User/userList.cshtml");
        }

        public async Task<IActionResult> deleteByIdUser(int id)
        {
            var rslt = await _userServices.deleteByIdUser(id);

            return Redirect(HttpContext.Request.Headers["Referer"]);
        }
        public async Task<IActionResult> ActiveInActive(int id)
        {
            var rslt = await _userServices.activeInActiveUser(id);
            return Redirect(HttpContext.Request.Headers["Referer"]);
        }

        public async Task<IActionResult> userAttendanceDetail()
        {
            var rslt = await _userServices.userList();
            ViewData["userData"] = rslt.data;
            return View("~/Areas/Admin/Views/User/userAttendanceDetail.cshtml");
        }
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth", new { area = "Admin" });
            // return Redirect("../Auth/Login");
        }

    }
}

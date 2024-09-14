using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Hrmanagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        public IAuthServices _authServices;
        private readonly IHttpContextAccessor _contextAccessor;
        public AuthController(IAuthServices authServices, IHttpContextAccessor contextAccessor)
        {
            _authServices = authServices;
            _contextAccessor = contextAccessor;
        }
        public async Task<IActionResult> Login([FromForm] UserLoginInput value)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Areas/Admin/Views/Auth/Login.cshtml");
            }
            else
            {
                var rslt = await _authServices.Login(value);
                if (rslt.succeed)
                {
                    _contextAccessor.HttpContext.Session.SetInt32("id", rslt.data.Id);
                    _contextAccessor.HttpContext.Session.SetString("role", rslt.data.Role);
                    _contextAccessor.HttpContext.Session.SetString("token",rslt.data.Token);
                    ViewData["UserData"] = rslt.data.Role;
                    if (rslt.data.Role == "admin")
                    {
                        //return RedirectToAction("userList", "User", new { area = "Admin" });
                        return RedirectToAction("userAttendanceDetail", "User", new { area = "Admin" });
                       
                    }
                    else if (rslt.data.Role == "Public")
                    {
                        return RedirectToAction("Index", "Index", new { area = "Public" });
                    }
                    else
                    {
                        return View("~/Areas/Admin/Views/Auth/Login.cshtml");
                    }
                }
                else
                {
                    TempData["error"] = "Your Email ID or Password Is Incorrect";
                    return View("~/Areas/Admin/Views/Auth/Login.cshtml");
                }
            }
            // return View("~/Areas/Admin/Views/Auth/Login.cshtml");
        }

        public async Task<IActionResult> forgotPassword([FromForm] ForgotPasswordInput value)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Areas/Admin/Views/Auth/forgotPassword.cshtml");
            }
            else
            {
                var rslt = await _authServices.forgotPassword(value);
                if (rslt.succeed)
                {
                    TempData["success"] = "You have Successfully Email";
                }
                else
                {
                    TempData["error"] = "Email Address Not Registered.";
                    return View("~/Areas/Admin/Views/Auth/forgotPassword.cshtml");
                }
                return View("~/Areas/Admin/Views/Auth/forgotPassword.cshtml");
            }
        }

        public async Task<IActionResult> resetPassword([FromQuery] string Token, [FromQuery] string ToEmail)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Areas/Admin/Views/Auth/resetPassword.cshtml");
            }
            else
            {
                var rslt = await _authServices.resetPassword(Token, ToEmail);
                if (rslt.succeed == false)
                {
                    // return Redirect("~/Login/Auth/error");
                    return RedirectToAction("error", "Auth", new { area = "Admin" });
                }
                else
                {
                    ViewData["resetPassword"] = rslt.data;
                    return View("~/Areas/Admin/Views/Auth/resetPassword.cshtml");
                }
            }
        }
        public async Task<IActionResult> resetPassWordUpdate([FromForm] ResetPasswordInput value)
        {
            var rslt = await _authServices.resetPasswordUpdate(value);
            if (rslt.succeed == false)
            {
                return RedirectToAction("error", "Auth", new { area = "Admin" });

            }
            else
            {
                TempData["Success"] = "Your password changed successfully";
                return RedirectToAction("Login", "Auth", new { area = "Admin" });
                //return Redirect("Login");
            }
        }
        public async Task<IActionResult> error()
        {
            return View("~/Areas/Admin/Views/Auth/error.cshtml");
        }
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth", new { area = "Admin" });
            // return Redirect("../Auth/Login");
        }

    }
}

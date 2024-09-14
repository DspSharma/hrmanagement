using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hrmanagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    [BaseController]
    public class SystemSettingController : Controller
    {
        public IMvcSystemSettingServices _systemSettingService;
        public SystemSettingController(IMvcSystemSettingServices systemSettingService) 
        { 
            _systemSettingService = systemSettingService;
        }
        public async Task<IActionResult> Index()
        {
            var rslt = await _systemSettingService.getSystemSetting();
            ViewData["response"] = rslt.data;
            return View("~/Areas/Admin/Views/SystemSetting/Index.cshtml");
        }
        public async Task<IActionResult> AddUpdate([FromBody] SystemSettingInput model)
        {
            var rslt = await _systemSettingService.AddUpdateSystemSetting(model);
            if (rslt.succeed)
            {
                TempData["Success"] = "Add Successfully";
            }
            else
            {
                TempData["error"] = "Your email  already exits ";
                return Redirect("Request.UrlReferrer.ToString()");
            }
            return Redirect("Request.UrlReferrer.ToString()");
        }

        public async Task<IActionResult> getEditById(int id)
        {
            var rslt = await _systemSettingService.editSystemSetting(id);
            // ViewData["SystemSetting"] = rslt.data;
            return Ok(rslt);
        }

        public async Task<IActionResult> systemSettingDeleteById(int id)
        {
            var rslt = await _systemSettingService.deleteById(id);
            if (rslt.succeed)
            {
                return Redirect(HttpContext.Request.Headers["Referer"]);
            }
            else
            {
                return Redirect(HttpContext.Request.Headers["Referer"]);
            }
        }
        public async Task<IActionResult> ActiveInActive(int id)
        {
            var rslt = await _systemSettingService.activeInActive(id);
            if (rslt.succeed)
            {
                return Redirect(HttpContext.Request.Headers["Referer"]);
            }
            else
            {
                return Redirect(HttpContext.Request.Headers["Referer"]);
            }

        }


    }
}

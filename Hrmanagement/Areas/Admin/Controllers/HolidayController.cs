using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Data.Entities;
using Hrmanagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hrmanagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    [BaseController]
    public class HolidayController : Controller
    {
        public IMvcHolidayService _holidayService;
        public HolidayController(IMvcHolidayService holidayService)
        {
            _holidayService = holidayService;
            
        }

        public async Task<IActionResult> AddUpdateHolidayForm(int id)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Areas/Admin/Views/Holiday/AddUpdateHolidayForm.cshtml");
            }
            else
            {
                if (id != 0)
                {
                    var rslt = await _holidayService.editHoliday(id);
                    ViewData["holiday"] = rslt.data;
                    return View("~/Areas/Admin/Views/Holiday/AddUpdateHolidayForm.cshtml");
                }
                else
                {
                    return View("~/Areas/Admin/Views/Holiday/AddUpdateHolidayForm.cshtml");
                }
            }
        }

        public async Task<IActionResult> AddUpdateHoliday([FromForm] HolidayInput model)
        {
            var rslt = await _holidayService.addUpdateHoliday(model);
            if (rslt.succeed)
            {
                TempData["Success"] = $"{rslt.message}";
                return RedirectToAction("holidayList", "Holiday", new { area = "Admin" });
                // return RedirectToAction("holidayList", "Holiday");
            }
            else
            {
                TempData["error"] = $"{rslt.message}";
                return Redirect(HttpContext.Request.Headers["Referer"]);
            }
        }

        public async Task<IActionResult> holidayList()
        {
            var rslt = await _holidayService.holidayList();
            ViewData["holidayData"] = rslt.data;
            return View("~/Areas/Admin/Views/Holiday/holidayList.cshtml");
        }

        public async Task<IActionResult> deleteByIdHoliday(int id)
        {
            var rslt = await _holidayService.deleteByIdHoliday(id);

            return Redirect(HttpContext.Request.Headers["Referer"]);
        }
        public async Task<IActionResult> ActiveInActive(int id)
        {
            var rslt = await _holidayService.activeInActiveHoliday(id);
            return Redirect(HttpContext.Request.Headers["Referer"]);
        }

    }
}

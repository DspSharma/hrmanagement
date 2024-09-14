using Hrmanagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hrmanagement.Areas.Public.Controllers
{
    [Area("Public")]
    [CheckSessionIsAvailableController]
    public class HolidayController : Controller
    {
        public IMvcHolidayService _holidayService;
        public HolidayController(IMvcHolidayService holidayService)
        {
            _holidayService = holidayService;
        }
        public async Task<IActionResult>holidayList()
        {
            var rslt = await _holidayService.holidayList();
            ViewData["holidayData"] = rslt.data;
            return View("~/Areas/Public/Views/Holiday/holidayList.cshtml");
        }
    }
}

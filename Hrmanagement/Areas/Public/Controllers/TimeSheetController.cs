using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hrmanagement.Areas.Public.Controllers
{
    [Area("Public")]
    [CheckSessionIsAvailableController]
    public class TimeSheetController : Controller
    {
        public IMvcTimeSheetService _timeSheetService;
        public IMvcProjectServices _projectServices;
        public TimeSheetController(IMvcTimeSheetService timeSheetService, IMvcProjectServices projectServices)
        {
            _timeSheetService = timeSheetService;
            _projectServices = projectServices;
        }

        public async Task<IActionResult>AddUpdateTimeSheetForm(int id)
        {
            if(!ModelState.IsValid)
            {
                var reslt = await _projectServices.projectList();
                ViewData["project"] = reslt.data;
                return View("~/Areas/Public/Views/TimeSheet/AddUpdateTimeSheetForm.cshtml");
            }
            else
            {
                if(id !=0)
                {
                    var timesheet = await _timeSheetService.editTimeSheet(id);
                    ViewData["timesheet"] = timesheet.data;
                    var reslt = await _projectServices.projectList();
                    ViewData["project"] = reslt.data;
                    return View("~/Areas/Public/Views/TimeSheet/AddUpdateTimeSheetForm.cshtml");
                }
                else
                {
                    var reslt = await _projectServices.projectList();
                    ViewData["project"] = reslt.data;
                    return View("~/Areas/Public/Views/TimeSheet/AddUpdateTimeSheetForm.cshtml");
                }
            }
        }

        public async Task<IActionResult> AddUpdateTimeSheet([FromForm] TimeSheetInput model)
        {
            var rslt = await _timeSheetService.addUpdateTimeSheet(model);
            if (rslt.succeed)
            {
                TempData["Success"] = $"{rslt.message}";
                return RedirectToAction("TimeSheetList", "TimeSheet", new { area = "Public" });
                // return RedirectToAction("holidayList", "Holiday");
            }
            else
            {
                TempData["error"] = $"{rslt.message}";
                return Redirect(HttpContext.Request.Headers["Referer"]);
            }
        }
        
        public async Task<IActionResult>TimeSheetList([FromQuery] int year = 0, [FromQuery] int month = 0)
        {
            int userId = 0;
            var timeSheet = await _timeSheetService.timeSheetList(userId);
            ViewData["timesheet"] = timeSheet.data;
            var project = await _projectServices.projectList();
            ViewData["project"] = project.data;
            return View("~/Areas/Public/Views/TimeSheet/TimeSheetList.cshtml");
        }

        public async Task<IActionResult> deleteByIdTimeSheet(int id)
        {
            var rslt = await _timeSheetService.timeSheetdeleteById(id);
            TempData["Success"] = $"{rslt.message}";
            return Redirect(HttpContext.Request.Headers["Referer"]);
        }

    }
}

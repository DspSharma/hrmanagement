using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hrmanagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    [BaseController]
    public class AttendanceController : Controller
    {
        public IMvcAttendanceServices _attendanceServicees;
        public IMvcTimeSheetService _timeSheetService;
        public AttendanceController(IMvcAttendanceServices attendanceServicees, IMvcTimeSheetService timeSheetService)
        {
            _attendanceServicees = attendanceServicees;
            _timeSheetService = timeSheetService;
        }

        public async Task<IActionResult> AttendanceUserById(int id, [FromQuery] int year = 0, [FromQuery] int month = 0)
        {
            var rslt = await _attendanceServicees.attendanceGetUserBy(id, year, month);
            ViewData["userId"] = id;
            ViewData["AttendanceData"] = rslt.data;
            return View("~/Areas/Admin/Views/Attendance/AttendanceUserById.cshtml");
        }

        public async Task<IActionResult> AddUpdateAttendance([FromBody] AttendanceInput model)
        {
            var rslt = await _attendanceServicees.addUpdateAttendanceAdmin(model);
            return Ok(rslt);
        }

        public async Task<IActionResult>attendanceById(int id)
        {
            var rslt = await _attendanceServicees.attendanceById(id);
            return Ok(rslt);
        }

        public async Task<IActionResult>partialLeave(int id, [FromQuery] int year = 0, [FromQuery] int month = 0)
        {
            var rslt = await _attendanceServicees.partialLeaveList(id);
            ViewData["partialLeaveData"] = rslt.data;
            ViewData["id"] = id;
            return View("~/Areas/Admin/Views/Attendance/partialLeave.cshtml");
            //return View();
        }
        public async Task<IActionResult>TimeSheetList(int id, [FromQuery] int year = 0, [FromQuery] int month = 0)
        {
            var rslt = await _timeSheetService.timeSheetList(id);
            ViewData["timesheet"] = rslt.data;
            ViewData["id"] = id;
            return View("~/Areas/Admin/Views/Attendance/TimeSheetList.cshtml");
        }
    }
}

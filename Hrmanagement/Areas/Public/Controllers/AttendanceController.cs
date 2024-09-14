using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hrmanagement.Areas.Public.Controllers
{
    [Area("Public")]
    [CheckSessionIsAvailableController]
    public class AttendanceController : Controller
    {
        public IMvcAttendanceServices _attendanceServices;
        public AttendanceController(IMvcAttendanceServices attendanceServices)
        {
            _attendanceServices = attendanceServices;
        }

        //public async Task<IActionResult> addUpdateAttendance()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View();
        //    }
        //    else
        //    {
        //        int actualUserId = 0;
        //        var rslt = await _attendanceServices.addUpdateAttendance(actualUserId);
        //        if (rslt.succeed)
        //        {
        //            TempData["success"] = rslt.message;

        //        }
        //        else if(rslt.message == "before checkout resume the pausetimer")
        //        {
        //            TempData["error"] = rslt.message;
        //            return RedirectToAction("getAttendanceUserBy", "Attendance", new { area = "Public" });
        //        }
        //        else
        //        {

        //            return RedirectToAction("AddUpdateTimeSheetForm", "TimeSheet", new { area = "Public" });
        //        }
        //        return RedirectToAction("getAttendanceUserBy", "Attendance", new { area = "Public" });
        //    }
        //}


        public async Task<IActionResult> addUpdateAttendance([FromForm] AttendanceInput value)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
               // int actualUserId = 0;
                var rslt = await _attendanceServices.addUpdateAttendance(value);
                if (rslt.succeed)
                {
                    TempData["success"] = rslt.message;

                }
                else if (rslt.message == "before checkout resume the pausetimer")
                {
                    TempData["error"] = rslt.message;
                    return RedirectToAction("getAttendanceUserBy", "Attendance", new { area = "Public" });
                }
                else
                {

                    return RedirectToAction("AddUpdateTimeSheetForm", "TimeSheet", new { area = "Public" });
                }
                return RedirectToAction("getAttendanceUserBy", "Attendance", new { area = "Public" });
            }
        }

        public async Task<IActionResult> getAttendanceUserBy([FromQuery] int year = 0, [FromQuery] int month = 0)
        {
            int actualUserId = 0;
            int partialLeaveId = 0;
            var rslt = await _attendanceServices.attendanceGetUserBy(actualUserId, year, month);
            var rse = await _attendanceServices.partialLeaveList(partialLeaveId);
            ViewData["partialLeaveData"] = rse.data;
            ViewData["AttendanceData"] = rslt.data;
            return View("~/Areas/Public/Views/Attendance/getAttendanceUserBy.cshtml");
        }

        public async Task<IActionResult> AddUpdatePartialLeave([FromBody] PartialLeaveInput value)
        {
            var rslt = await _attendanceServices.AddUpdatePartialLeave(value);
            if(rslt.succeed)
            {
                return Ok(rslt);
            }
            else
            {
                return Ok(rslt);
            }
        }

        public async Task<IActionResult>partialLeaveList([FromQuery] int year = 0, [FromQuery] int month = 0)
        {
            int id = 0;
            var rslt = await _attendanceServices.partialLeaveList(id);
            ViewData["partialLeaveData"] = rslt.data;
            return View("~/Areas/Public/Views/Attendance/partialLeaveList.cshtml");
            // return View();
        }

    }
}

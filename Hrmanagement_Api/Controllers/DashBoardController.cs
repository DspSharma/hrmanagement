using Hrmanagement.Core.Misc;
using Hrmanagement.Core.Models;
using Hrmanagement.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrmanagement_Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        public IAttendanceService _attendanceService;
        public ILeaveService _leaveService;
        public DashBoardController(IAttendanceService attendanceService, ILeaveService leaveService)
        {
            _attendanceService = attendanceService;
            _leaveService = leaveService;
        }

        [HttpGet("GetDashBoardMonthSummary")]
        public async Task<IActionResult> GetDashBoardMonthSummary(int userId)
        {
            try
            {
                var u = MiscMethods.getLoginDetailByToken(HttpContext);

                if (userId == 0)
                    userId = u.Id;

                var attendanceResponse = await _attendanceService.getAttendances(userId, 0, 0);
                var monthlyAttendnce = await _attendanceService.GetMonthlyAttendanceSummary(userId);

                if (attendanceResponse.succeed && monthlyAttendnce.succeed)
                {
                    var attendanceData = attendanceResponse.data;
                    var monthlyData = monthlyAttendnce.data;

                    double totalWorkingHours = attendanceData.currentMonthHours - attendanceData.shortHours;
                    int remainingDaysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) - DateTime.Now.Day;

                    var summaryOutput = new DashBoardModel()
                    {
                        TotalAttendance = monthlyData.totalAttendances,
                        TotalLeave = monthlyData.totalLeave,
                        TotalRemainingDays = remainingDaysInMonth,
                        //TotalRemainingWorkingHours = totalWorkingHours * remainingDaysInMonth
                        TotalMonthlyHours = attendanceData.currentMonthHours,
                        TotalWorkingHours = totalWorkingHours,
                        TotalRemainingWorkingHours = attendanceData.currentMonthHours - totalWorkingHours

                    };
                    return Ok(new ApiResponseModel<DashBoardModel> { succeed = true, message = "Success", data = summaryOutput });
                }
                else
                {
                    return BadRequest(new ApiResponseModel<DashBoardModel> { succeed = false, message = "Failed to retrieve data" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseModel<DashBoardModel> { succeed = false, message = ex.Message });
            }
        }

    }
}

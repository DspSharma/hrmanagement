using AutoMapper;
using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.DTO.DtoOutput;
using Hrmanagement.Core.Models;
using Hrmanagement.Data.DBContext;
using Hrmanagement.Data.Entities;
using Hrmanagement.Data.UnitOfWork;
using Hrmanagement.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Service
{
    public class AttendanceService : IAttendanceService
    {
        public IUnitOfWork _unitOfWork;
        private readonly HrManagementContext _context;
        public IMapper _mapper;
        public ILeaveService _leaveService;
        public IEmailService _emailService;
        public IPartialLeaveService _partialLeaveService;

        private readonly IHttpContextAccessor _httpContextAccessor;
        public IConfiguration _Configuration { get; }

        public AttendanceService(HrManagementContext context, IUnitOfWork unitOfWork, IMapper mapper, ILeaveService leaveService, IEmailService emailService, IConfiguration configuration, IPartialLeaveService partialLeaveService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _leaveService = leaveService;
            _emailService = emailService;
            _Configuration = configuration;
            _partialLeaveService = partialLeaveService;
            _httpContextAccessor = httpContextAccessor;
        }

        //public string GetIpAddress(HttpContext context)
        //{

        //    var ipAddress = context.Connection.RemoteIpAddress?.ToString();


        //    return ipAddress;
        //}


        public async Task<ApiResponseModel<bool>> IsCheckedIn(int id)
        {
            try
            {
                Attendance attendance = _unitOfWork.Attendance.GetWhere(x => x.UserId == id && x.CheckIn.Date == DateTime.Now.Date).FirstOrDefault();

                if (attendance == null)
                {
                    return new ApiResponseModel<bool> { succeed = true, message = "today not checked-in", data = false };
                }
                else
                {
                    return new ApiResponseModel<bool> { succeed = true, message = "today checked-in", data = true };
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}");
            }
        }

        public async Task<ApiResponseModel<bool>> IsPause(int id)
        {
            try
            {
                Attendance attendance = _unitOfWork.Attendance.GetWhere(x => x.UserId == id && x.CheckIn.Date == DateTime.Now.Date).FirstOrDefault();
                if (attendance == null)
                {
                    return new ApiResponseModel<bool> { succeed = false, message = "data null", data = false };
                }

                PartialLeave partialLeaves = _unitOfWork.PartialLeave.GetWhere(x => x.UserId == id && x.PauseTime.Date == DateTime.UtcNow.Date && x.ResumeTime == null).OrderByDescending(x => x.PauseTime).FirstOrDefault();

                if (partialLeaves != null)
                {
                    attendance.IsPause = true;
                    await _unitOfWork.SaveAsync();
                    return new ApiResponseModel<bool> { succeed = true, message = "Pause time is on", data = true };
                }
                else
                {
                    attendance.IsPause = false;
                    await _unitOfWork.SaveAsync();
                    return new ApiResponseModel<bool> { succeed = true, message = "Pause time is off", data = false };
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}");
            }
        }

        public async Task<ApiResponseModel<AttendanceOutput>> AddUpdateAttendance(AttendanceInput value)
        {
            try
            {
                // Get the IP address from the HttpContext

                string ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString();

                //DateTime dutyOffTimeToday = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 19, 30, 0);
                //DateTime dutyOffTimeYesterday = dutyOffTimeToday.AddDays(-1);

              
                Attendance previousAttendance = _unitOfWork.Attendance.GetWhere(x => x.UserId == value.UserId && x.CheckIn.Date < DateTime.Now.Date && !x.IsCheckOut).OrderByDescending(x => x.CheckIn).FirstOrDefault();
               
                
                if (previousAttendance != null)
                {
                    PartialLeave? previouspartialLeaves = _unitOfWork.PartialLeave.GetWhere(x => x.UserId == value.UserId && x.PauseTime.Date < DateTime.Now.Date && x.ResumeTime == null).FirstOrDefault();
                    if (previouspartialLeaves == null)
                    {
                       // previousAttendance.CheckOut = DateTime.UtcNow;
                        previousAttendance.IsCheckOut = true;
                        previousAttendance.UpdatedAt = DateTime.UtcNow;
                        return new ApiResponseModel<AttendanceOutput> { succeed = false, message = "No Previous Partial Leave Found" };
                    }

                    previousAttendance.CheckOut = previouspartialLeaves.PauseTime;
                    previousAttendance.IsPause = false;
                    //previouspartialLeaves.ResumeTime = dutyOffTimeYesterday;
                    previouspartialLeaves.ResumeTime = new DateTime(previouspartialLeaves.PauseTime.Year, previouspartialLeaves.PauseTime.Month, previouspartialLeaves.PauseTime.Day, 19, 30, 0);
                    previousAttendance.IsCheckOut = true;
                    previousAttendance.UpdatedAt = DateTime.UtcNow;

                    await _unitOfWork.SaveAsync();
                    //return new ApiResponseModel<AttendanceOutput>
                    //{
                    //    succeed = true,
                    //    message = "Attendance updated successfully.",
                    //    data = new AttendanceOutput { CheckOut = previousAttendance.CheckOut }
                    //};
                }
               

                Attendance attendance = _unitOfWork.Attendance.GetWhere(x => x.UserId == value.UserId && x.CheckIn.Date == DateTime.Now.Date).FirstOrDefault();

                User user = _unitOfWork.User.GetWhere(x => x.Id == value.UserId).FirstOrDefault();
                User adminUser = _unitOfWork.User.GetWhere(x => x.Role == "admin" && x.IsActive).FirstOrDefault();

                AttendanceOutput rslt = new AttendanceOutput();
                if (attendance == null)
                {
                    attendance = new Attendance();
                    attendance.UserId = value.UserId;
                    attendance.CheckIn = DateTime.Now;
                    attendance.CheckOut = DateTime.Now;
                    attendance.CheckInLatitude = value.CheckInLatitude;
                    attendance.CheckInLongitude = value.CheckInLongitude;
                    attendance.IpAddress = ipAddress;
                    attendance.IsCheckOut = false;
                    attendance.CreatedAt = DateTime.UtcNow;
                    attendance.UpdatedAt = DateTime.UtcNow;
                    await _unitOfWork.Attendance.AddAsync(attendance);
                    await _unitOfWork.SaveAsync();

                    rslt = _mapper.Map<AttendanceOutput>(attendance);
                    return new ApiResponseModel<AttendanceOutput>() { succeed = true, message = "CheckIn Successfully.", data = rslt };
                }

                else
                {
                    if (attendance.IsCheckOut)
                    {
                        return new ApiResponseModel<AttendanceOutput> { succeed = false, message = "Cannot check-in again. User has already checked out for the day." };
                    }

                    else
                    {
                        PartialLeave partialLeaves = _unitOfWork.PartialLeave.GetWhere(x => x.UserId == value.UserId && x.ResumeTime == null && x.UpdatedAt.Date == DateTime.Now.Date).FirstOrDefault();
                        if (partialLeaves != null)
                        {

                            return new ApiResponseModel<AttendanceOutput> { succeed = false, message = "before checkout resume the pausetimer" };
                        }

                        TimeSheet timeSheetsd = _unitOfWork.TimeSheet.GetWhere(x => x.UserId == value.UserId && x.CreatedAt.Date == DateTime.Now.Date).FirstOrDefault();

                        if (timeSheetsd == null)
                        {
                            return new ApiResponseModel<AttendanceOutput> { succeed = false, message = "Cannot check-out without filling the timesheet for the day." };
                        }

                        else
                        {
                            attendance.CheckOut = DateTime.Now;
                            attendance.CheckOutLatitude = value.CheckOutLatitude;
                            attendance.CheckOutLongitude = value.CheckOutLongitude;
                            //attendance.IpAddress = value.IpAddress;
                            attendance.IsCheckOut = true;
                            attendance.UpdatedAt = DateTime.UtcNow;
                        }
                    }

                    await _unitOfWork.SaveAsync();

                    // send Email Work Start
                    string userName = user.FirstName + " " + user.LastName;
                    string userEmail = user.Email;
                    DateTime userCheckIn = attendance.CheckIn;
                    DateTime userCheckOut = attendance.CheckOut;
                    TimeSpan totalHours = userCheckOut - userCheckIn;

                    string subject = "This is to inform you that we have received a request for Attendance from one of our users";
                    string Message = $"<html><body>" +
                        $"<h1>Dear {userName}</h1>" +
                        $"<h5>Email :-  {userEmail}</h5>" +
                        $"<p>CheckIn :-  {userCheckIn}</p>" +
                        $" <p>CheckOut :- {userCheckOut}</p> " +
                        $" <p>TotalHours :- {totalHours}</p>" +
                        $" <a href='http://hrm.tagosys.com/Admin/Auth/Login/'>Click here to view the page </a>" +
                        $"</body></html>";
                    string toEmail = adminUser.Email;

                    var emailrslt = _emailService.SendEmail(subject, Message, toEmail);
                    // send Email Work End

                    rslt = _mapper.Map<AttendanceOutput>(attendance);

                    return new ApiResponseModel<AttendanceOutput>() { succeed = true, message = "Attendance record updated successfully.", data = rslt };
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        //

        public async Task<ApiResponseModel<AttendanceOutput>> AddUpdateAttendanceByAdmin(AttendanceInput value)
        {
            try
            {

                Attendance existingAttendance = _unitOfWork.Attendance.GetWhere(x => x.id != value.id && x.UserId == value.UserId && x.CheckIn.Date < DateTime.Now.Date).FirstOrDefault();

                if (existingAttendance != null && existingAttendance.CheckIn != default(DateTime) && existingAttendance.CheckOut != default(DateTime))
                {
                    return new ApiResponseModel<AttendanceOutput>
                    {
                        succeed = false,
                        message = "Attendance record already exists for the given UserId and CheckIn."
                    };
                }

                Attendance attendance;

                if (value.id != 0)
                {
                    attendance = await _unitOfWork.Attendance.GetByIdAsync(value.id);
                    if (attendance == null)
                    {
                        return new ApiResponseModel<AttendanceOutput>
                        {
                            succeed = false,
                            message = "Attendance record not found for the provided id."
                        };
                    }

                    // Update existing attendance record
                    attendance.CheckIn = value.CheckIn;
                    attendance.CheckOut = value.CheckOut;
                    attendance.IsCheckOut = true;
                    attendance.UpdatedAt = DateTime.UtcNow;
                }
                else
                {
                    // Create a new attendance record
                    attendance = new Attendance
                    {
                        UserId = value.UserId,
                        CheckIn = value.CheckIn,
                        CheckOut = value.CheckOut,
                        IsCheckOut = true,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };

                    await _unitOfWork.Attendance.AddAsync(attendance);
                }

                await _unitOfWork.SaveAsync();

                AttendanceOutput result = _mapper.Map<AttendanceOutput>(attendance);

                return new ApiResponseModel<AttendanceOutput>
                {
                    succeed = true,
                    message = "Success",
                    data = result
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponseModel<AttendanceModels>> getAttendances(int id, int year, int month)
        {
            try
            {
                var today = DateTime.Now;
                var selectedYear = year != 0 ? year : today.Year;
                var selectedMonth = month != 0 ? month : today.Month;

                var selectedMonthCountDays = DateTime.DaysInMonth(selectedYear, selectedMonth);
                var countMonthSunday = Enumerable.Range(1, selectedMonthCountDays)
                                                  .Select(day => new DateTime(selectedYear, selectedMonth, day))
                                                  .Count(date => date.DayOfWeek == DayOfWeek.Sunday);

                var holidays = _unitOfWork.Holiday.GetWhere(x => x.HolidayFromDate.Month == selectedMonth && x.HolidayFromDate.Year == selectedYear && x.IsActive).ToList();
                var countTotalHolidays = holidays.Sum(h => (h.HolidayToDate - h.HolidayFromDate).TotalDays + 1);

                var attendanceData = _unitOfWork.Attendance.GetWhere(x => x.UserId == id && x.CheckIn.Month == selectedMonth && x.CheckIn.Year == selectedYear).ToList();
                var todayAttendance = attendanceData.FirstOrDefault(a => a.CheckIn.Date == today.Date);

                var partialLeaves = _unitOfWork.PartialLeave.GetWhere(x => x.UserId == id && x.PauseTime.Month == selectedMonth && x.PauseTime.Year == selectedYear).ToList();
                var totalPartialLeaveMinutes = partialLeaves.Sum(pl => pl.ResumeTime.HasValue ? (pl.ResumeTime.Value - pl.PauseTime).TotalMinutes : 0);

                int totalHours = (int)totalPartialLeaveMinutes / 60; // Total hours for partial leaves
                int totalMinutes = (int)totalPartialLeaveMinutes % 60; // Remaining minutes for partial leaves
                TimeSpan formattedDuration = new TimeSpan(totalHours, totalMinutes, 0); // Total duration of partial leaves

                double workableDays = selectedMonthCountDays - countMonthSunday - countTotalHolidays - 2;
                double totalMonthHours = workableDays * 9.5; // Assuming 9.5 hours per workable day

                double totalAttendanceHours = 0;
                var listAttendanceOutput = _mapper.Map<List<AttendanceOutput>>(attendanceData);
                foreach (var attendance in listAttendanceOutput)
                {
                    var user = _unitOfWork.User.GetWhere(x => x.Id == id).FirstOrDefault();
                    TimeSpan duration = (attendance.CheckOut - attendance.CheckIn);
                    attendance.TotalAttendancesHours = duration;
                    totalAttendanceHours += duration.TotalHours;
                    attendance.DayName = attendance.CheckIn.ToString("dddd");
                    attendance.UserName = user.FirstName + " " + user.LastName;


                }

                double hoursDifference = totalAttendanceHours - totalMonthHours;
                double extraHours = Math.Max(0, hoursDifference);
                double shortHours = Math.Max(0, -hoursDifference);
                AttendanceModels result = new AttendanceModels
                {
                    SelectedYear = selectedYear,
                    SelectedMonth = selectedMonth,
                    userid = id,
                    currentMonthHours = totalMonthHours,
                    MonthlyPartialLeaveHours = formattedDuration,
                    shortHours = Convert.ToInt32(shortHours),
                    extraHours = Convert.ToInt32(extraHours),
                    Attendance = listAttendanceOutput,
                    TotalMonthHours = Convert.ToInt32(totalAttendanceHours),
                    isTodayCheckedIn = todayAttendance != null,
                    isTodayCheckedOut = todayAttendance?.IsCheckOut ?? false
                };


                return new ApiResponseModel<AttendanceModels>
                {
                    succeed = true,
                    message = "success",
                    data = result
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public async Task<ApiResponseModel<AttendanceOutput>> GetMonthlyAttendanceSummary(int userId)
        {
            try
            {

                var currentMonthYear = DateTime.Now;

                // Fetch attendance data
                List<Attendance> getAttendancesData = _unitOfWork.Attendance.GetWhere
                    (x => x.UserId == userId && x.CheckIn.Month == currentMonthYear.Month && x.CheckIn.Year == currentMonthYear.Year).ToList();

                // Fetch leave data
                List<Leave> getLeavesData = _unitOfWork.Leave.GetWhere(
                    x => x.UserId == userId && x.LeaveFromDate.Month == currentMonthYear.Month && x.LeaveFromDate.Year == currentMonthYear.Year && x.IsLeaveApproved
                ).ToList();

                var leaveOutputs = _mapper.Map<List<LeaveOutput>>(getLeavesData);
                double totalLeaveDays = 0;
                foreach (var leave in leaveOutputs)
                {
                    double totalDay = (leave.LeaveToDate - leave.LeaveFromDate).TotalDays + 1;
                    leave.TotalLeaveDays = totalDay;

                    totalLeaveDays += totalDay;
                }

                // Fetch user data
                User userData = await _unitOfWork.User.GetByIdAsync(userId);

                int remainingDaysInMonth = DateTime.DaysInMonth(currentMonthYear.Year, currentMonthYear.Month) - currentMonthYear.Day;
                int totalLeaveCount = Convert.ToInt32(totalLeaveDays);
                int TotalAttendanceCount = Convert.ToInt32(getAttendancesData.Count());

                AttendanceOutput attendanceOutput = new AttendanceOutput();
                attendanceOutput.totalAttendances = TotalAttendanceCount;
                attendanceOutput.totalLeave = totalLeaveCount;
                attendanceOutput.totalShortdays = remainingDaysInMonth;
                attendanceOutput.UserId = userId;
                attendanceOutput.UserName = userData?.FirstName;
                return new ApiResponseModel<AttendanceOutput>()
                {
                    succeed = true,
                    message = "success",
                    data = attendanceOutput
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ApiResponseModel<AttendanceOutput>> GetAttendanceByid(int id)
        {
            try
            {
                Attendance attendance = await _unitOfWork.Attendance.GetByIdAsync(id);
                if (attendance == null)
                {
                    return new ApiResponseModel<AttendanceOutput>
                    {
                        succeed = false,
                        message = "attendance was not found.",
                        data = null
                    };
                    //throw new Exception($"attendance was not found.");
                }
                var attendanceOutput = _mapper.Map<AttendanceOutput>(attendance);
                return new ApiResponseModel<AttendanceOutput>
                {
                    succeed = true,
                    message = "success",
                    data = attendanceOutput
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //public async Task<ApiResponseModel<AttendanceModels>> getAttendances(int id, int year, int month)
        //{
        //    try
        //    {
        //        double totalhours = 0;
        //        int selectedYear;
        //        int selectedMonth;
        //        int selectedMonthCountDays;
        //        double todayhours = 9.5;
        //        int countMonthSunday = 0;
        //        double countTotalHolidays = 0;
        //        var today = DateTime.Now;

        //        if (year == 0 && month == 0)
        //        {
        //            selectedYear = Convert.ToInt32(DateTime.Now.ToString("yyyy"));
        //            selectedMonth = Convert.ToInt32(DateTime.Now.ToString("MM"));
        //            selectedMonthCountDays = DateTime.DaysInMonth(selectedYear, selectedMonth);
        //            for (int day = 1; day <= selectedMonthCountDays; day++)
        //            {
        //                DateTime date = new DateTime(selectedYear, selectedMonth, day);
        //                if (date.DayOfWeek == DayOfWeek.Sunday)
        //                {
        //                    countMonthSunday++;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            selectedYear = year;
        //            selectedMonth = month;
        //            selectedMonthCountDays = DateTime.DaysInMonth(year, month);
        //            for (int day = 1; day <= selectedMonthCountDays; day++)
        //            {
        //                DateTime date = new DateTime(year, month, day);
        //                if (date.DayOfWeek == DayOfWeek.Sunday)
        //                {
        //                    countMonthSunday++;
        //                }
        //            }
        //        }

        //        List<Holiday> holidays = new List<Holiday>();
        //        holidays = _unitOfWork.Holiday.GetWhere(x => x.HolidayFromDate.Month == selectedMonth && x.HolidayFromDate.Year == selectedYear && x.IsActive).ToList();
        //        var holidaysOutput = _mapper.Map<List<Holiday>>(holidays);
        //        foreach (var holiday in holidays)
        //        {
        //            countTotalHolidays += (holiday.HolidayToDate - holiday.HolidayFromDate).TotalDays + 1;
        //        }


        //        List<Attendance> attendancesData = new List<Attendance>();
        //        attendancesData = _unitOfWork.Attendance.GetWhere(x => x.UserId == id && x.CheckIn.Month == selectedMonth && x.CheckIn.Year == selectedYear).ToList();
        //        Attendance todayAttendances = _unitOfWork.Attendance.GetWhere(x => x.UserId == id && x.CheckIn.Date == today.Date).FirstOrDefault();
        //        var listAttendanceOutput = _mapper.Map<List<AttendanceOutput>>(attendancesData);

        //        /****  partial Leaves
        //         ***/
        //        var partialLeaves = _unitOfWork.PartialLeave.GetWhere(x => x.UserId == id && x.PauseTime.Month == selectedMonth && x.PauseTime.Year == selectedYear).ToList();
        //        var totalPartialLeaveMinutes = partialLeaves.Sum(pl => pl.ResumeTime.HasValue ? (pl.ResumeTime.Value - pl.PauseTime).TotalMinutes : 0);
        //        int totalHours = (int)totalPartialLeaveMinutes / 60; // Total hours
        //        int totalMinutes = (int)totalPartialLeaveMinutes % 60;
        //        TimeSpan formattedDuration = new TimeSpan(totalHours, totalMinutes, 0);
        //        ///

        //        double countSunday = selectedMonthCountDays - countMonthSunday;
        //        double countholidays = countSunday - countTotalHolidays;
        //        double totalMonthHours = countholidays - 2;
        //        double totalCurrentMonthHours = totalMonthHours * todayhours;

        //        AttendanceModels rslt = new AttendanceModels();
        //        rslt.SelectedYear = selectedYear;
        //        rslt.SelectedMonth = selectedMonth;
        //        rslt.userid = id;
        //        rslt.currentMonthHours = totalCurrentMonthHours;
        //        rslt.MonthlyPartialLeaveHours = formattedDuration;

        //        // daily Hours count
        //        foreach (var a in listAttendanceOutput)
        //        {
        //            TimeSpan startTime = (a.CheckOut - a.CheckIn);
        //            a.TotalAttendancesHours = startTime;

        //            totalhours += startTime.TotalHours;
        //            a.DayName = a.CheckIn.ToString("dddd");
        //        }

        //        double shortHours = (totalCurrentMonthHours - totalhours);
        //        double extraHours = (totalhours - totalCurrentMonthHours);
        //        int getshortHours = Convert.ToInt32(shortHours);
        //        int getextraHours = Convert.ToInt32(extraHours);
        //        rslt.shortHours = getshortHours;
        //        rslt.extraHours = getextraHours;

        //        rslt.Attendance = listAttendanceOutput;
        //        int getTotalHours = Convert.ToInt32(totalhours);
        //        rslt.TotalMonthHours = getTotalHours;

        //        if (todayAttendances == null)
        //        {

        //            rslt.isTodayCheckedIn = false;
        //        }
        //        else
        //        {
        //            if (todayAttendances.IsCheckOut)
        //                rslt.isTodayCheckedOut = true;
        //            rslt.isTodayCheckedIn = true;
        //        }

        //        return new ApiResponseModel<AttendanceModels> { succeed = true, message = "success", data = rslt };
        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception(ex.Message);
        //    }

        //}


    }
}

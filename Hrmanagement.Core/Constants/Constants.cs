using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Core.Constants
{
    public class Constants
    {
    }

    //public class baseUrlEndPoint
    //{
    //    public const string baseUrl = "http://192.168.0.32:5001/";
    //}

    public class UserEndPoint
    {
        public const string userAddUpdate = "api/User/AddUpdateUser";
        public const string userGetAll = "api/User/getAllUser";
        public const string userGetById = "api/User/Get";
        public const string userDeleteById = "api/User/Get";
        public const string userActiveInActive = "api/User/ActiveInActive";
        // gd changepassword work
        public const string userchangepassword = "api/User/ChangePassword";
    }

    public class UserMemoEndpoint
    {
        public const string userMemoAddUpdate = "api/UserMemo/AddUpdateUserMemo";
        public const string UserMemoGetAll = "api/UserMemo/getAllUserMemo";
        public const string userMemoGetById = "api/UserMemo/Get";
        public const string userMemoDeletebyId = "api/UserMemo/Get";
        public const string userMemoGetAllForPublic = "api/UserMemo/GetAllMemosForPublic";
    }
    public class AuthEndPoint
    {
        public const string TokenLogin = "api/Auth/Login";
        public const string Login = "api/User/login";
        public const string forgotPassword = "api/Auth/forgotPassword";
        public const string getForgotPassWord = "api/Auth/resetPassword";
        public const string addUpdateResetPassword = "api/Auth/resetPasswordUpdate";
    }
    public class HolidayEndPoint
    {
        public const string holidayAddUpdate = "api/Holiday/AddUpdateHoliday";
        public const string holidayGetAll = "api/Holiday/GetAllHolidays";
        public const string holidayGetById = "api/Holiday/Get";
        public const string holidayDeleteById = "api/Holiday/Get";
        public const string holidayActiveInActive = "api/Holiday/ActiveInActive";
    }
    public class LeaveEndPoint
    {
        public const string leaveAddUpdate = "api/Leave/AddLeave";
        public const string leaveGetAll = "api/Leave/GetAllLeaves";
        public const string leaveGetUserById = "api/Leave/GetLeavesByUserId";
        public const string leaveGetById = "api/Leave/Get";
        public const string leaveUpdateStatus = "api/Leave/updateLeavestatus";
    }

    public class AttendanceEndPoint
    {
         public const string attendanceAddUpdate = "api/Attendance/AddUpdateAttendance";  
       // public const string attendanceAddUpdate = "api/Attendance/AddUpdateAttendancesssss";
        public const string attendanceGetUserBy = "api/Attendance/GetAttendance";
        public const string attendanceGetMonthSummary = "api/Attendance/GetMonthlyAttendanceSummary";
        public const string attendanceAddUpdateAdmin = "api/Attendance/AddUpdateAttendanceByAdmin";
        public const string attendanceAddUpdatePartialLeave = "api/PartialLeave/AddUpdatePartialLeave";
        public const string attendanceGetPartialLeave = "api/PartialLeave/GetAllPartialLeaves";
        public const string attendanceGetById = "api/Attendance/Get";
    }

    public class ApiCredentialsEndPoint
    {
        public const string apiCredentialAddUpdate = "api/ApiCredentials/AddUpdateApiCredential";
        public const string apiCredentialGetAll = "api/ApiCredentials/GetAllApiCredential";
        public const string apiCredentialDeleteById = "api/ApiCredentials/Get";
        public const string apiCredentialGetById = "api/ApiCredentials/Get";
    }

    public class SystemSettingEndPoint
    {
        public const string systemSettingAddUpdate = "api/SystemSetting/AddUpdateSystemSetting";
        public const string systemSettingGetAll = "api/SystemSetting/GetSystemSetting";
        public const string systemSettingDeleteById = "api/SystemSetting/Get";
        public const string systemSettingActiveInActive = "api/SystemSetting/ActiveInActive";
        public const string systemSettingGetById = "api/SystemSetting/Get";
    }

    public class ProjectEndPoint
    {
        public const string projectAddUpdate = "api/Project/AddUpdateProject";
        public const string projectGetAll = "api/Project/GetAllProject";
        public const string projectGetById = "api/Project/Get";
        public const string projectDeleteById = "api/Project/Get";
        public const string projectActiveInActive = "api/Project/ActiveInActive";
    }

    public class TimeSheetEndPoint
    {
        public const string timeSheetAddUpdate = "api/TimeSheet/AddUpdateTimeSheet";
        public const string timeSheetGetAll = "api/TimeSheet/GetAllTimeSheet";
        public const string timeSheetGetById = "api/TimeSheet/Get";
        public const string timeSheetDeleteById = "api/TimeSheet/Delete";
    }
         

}

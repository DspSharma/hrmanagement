using Hrmanagement.Core.DTO.DtoOutput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Core.Models
{
    public class AttendanceModels
    {
        public List<AttendanceOutput> Attendance { get; set; }
        public int userid { get; set; }
        public bool isTodayCheckedIn { get; set; }
        public bool isTodayCheckedOut { get; set; }
        public int SelectedYear { get; set; }
        public int SelectedMonth { get; set; }
        public int TotalMonthHours { get; set; }
        public double currentMonthHours { get; set; }
        public int extraHours { get; set; }
        public int shortHours { get; set; }
        public TimeSpan MonthlyPartialLeaveHours { get; set; }

       // public TimeSpan MonthlyPartial { get; set; }


    }
}

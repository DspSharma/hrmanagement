using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Core.Models
{
    public class DashBoardModel
    {

        public int TotalAttendance { get; set; }
        public int TotalLeave { get; set; }
        public int TotalRemainingDays { get; set; }
        public double TotalMonthlyHours { get; set; }
        public double TotalWorkingHours { get; set; }
        public double TotalRemainingWorkingHours { get; set; }



    }
}

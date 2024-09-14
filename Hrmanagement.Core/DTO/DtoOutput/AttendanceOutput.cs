using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Core.DTO.DtoOutput
{
    public class AttendanceOutput
    {
        public int id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public bool IsCheckOut { get; set; }
        public bool IsPause { get; set; }
        public double CheckInLatitude { get; set; }
        public double CheckInLongitude { get; set; }
        public double? CheckOutLatitude { get; set; }
        public double? CheckOutLongitude { get; set; }
        public string? IpAddress { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int totalAttendances { get; set; }
        public int totalLeave { get; set; }
        public int totalShortdays { get; set; }
        public TimeSpan? TotalAttendancesHours { get; set; }

        public string DayName { get; set; }
    }

   

}

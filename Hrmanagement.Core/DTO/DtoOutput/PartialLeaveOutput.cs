using Hrmanagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Core.DTO.DtoOutput
{
    public class PartialLeaveOutput
    {

        public int id { get; set; }

        public int UserId { get; set; }
        public string Detail { get; set; }
        public DateTime PauseTime { get; set; }
        public DateTime? ResumeTime { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string Username { get; set; }

        //public double DailyHours { get; set; }
        //public double MonthlyHours { get; set; }
        //public double TotalHours { get; set; }


        //public bool LeavesExceeded { get; set; }
    }
}

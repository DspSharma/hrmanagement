using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Core.DTO.DtoOutput
{
    public class HolidayOutput :BaseOutput
    {
        public string Title { get; set; }
        public DateTime HolidayFromDate { get; set; }
        public DateTime HolidayToDate { get; set; }

        public double TotalDays { get; set; }
    }
}

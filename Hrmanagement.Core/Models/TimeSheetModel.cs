using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.DTO.DtoOutput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Core.Models
{
    public class TimeSheetModel
    {
        public bool checkoutStatus { get; set; }
        public  List <TimeSheetOutput> timesheets {get; set;}
    }
}

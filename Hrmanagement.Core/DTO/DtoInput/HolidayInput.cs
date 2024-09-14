using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Core.DTO.DtoInput
{
    public class HolidayInput:BaseInput
    {

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "HolidayFromDate is required")]
       
        public DateTime HolidayFromDate { get; set; }

        [Required(ErrorMessage = "HolidayToDate is required")]
       
        public DateTime HolidayToDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Data.Entities
{
    public class Holiday:BaseEntitiy
    {
       
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
       
        [Required]
        [DataType(DataType.Date)]
        public DateTime HolidayFromDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime HolidayToDate { get; set; }
        
    }
}

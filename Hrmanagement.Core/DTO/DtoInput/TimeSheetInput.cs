using Hrmanagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Core.DTO.DtoInput
{
    public class TimeSheetInput
    {
        public int id { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }

        [Required]
        public string TaskTitle { get; set; }
        [Required]
        public string TaskDescription { get; set; }
        
        [Required]
        public decimal Hours { get; set; }
        [Required]
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

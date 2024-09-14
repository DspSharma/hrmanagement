using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Data.Entities
{
    public class PartialLeave
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        [StringLength(500, ErrorMessage = "Detail must be at most 500 characters")]
        public string Detail { get; set; }
        public DateTime PauseTime { get; set; }
        public DateTime? ResumeTime { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

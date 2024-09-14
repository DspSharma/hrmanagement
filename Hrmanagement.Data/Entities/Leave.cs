using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Data.Entities
{
    public class Leave 
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [StringLength(500)]
        public string Message { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime LeaveFromDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime LeaveToDate { get; set;}
        public bool IsLeaveApproved { get; set; }
        public bool IsRejected { get; set; }
        public string? Remark { get; set; } = null;

        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}

using Hrmanagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Core.DTO.DtoInput
{
    public class LeaveInput
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(50, ErrorMessage = "Title cannot exceed 100 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Message is required")]
        [MaxLength(500, ErrorMessage = "Message cannot exceed 500 characters")]
        public string Message { get; set; }

        [Required(ErrorMessage = "LeaveFromDate is required")]
        [DataType(DataType.Date)]
        public DateTime LeaveFromDate { get; set; }

        [Required(ErrorMessage = "LeaveToDate is required")]
        [DataType(DataType.Date)]
        public DateTime LeaveToDate { get; set; }
        public bool IsLeaveApproved { get; set; } 
        public bool IsRejected { get; set; }

        //[MaxLength(200, ErrorMessage = "Remark cannot exceed 200 characters")]
        public string? Remark { get; set; } = null;
        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

using Hrmanagement.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Core.DTO.DtoInput
{
    public class SMSlogsInput
    {
        public int id { get; set; }
        public int? Pid { get; set; } = null!;
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Message is required")]
        public string Message { get; set; }
        public int ProjectId { get; set; }
        public int userId { get; set; }
        [Required(ErrorMessage = "Mobile number  is required")]
        public int From { get; set; }
        [Required(ErrorMessage = "Mobile number  is required")]
        public int To { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

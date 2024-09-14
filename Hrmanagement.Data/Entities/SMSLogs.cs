using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Data.Entities
{
    public class SMSLogs
    {
        [Key]
        public int id { get; set; }
        public int? Pid { get; set; } = null!;
        [StringLength(50)]
        public string Title { get; set; }
        [StringLength(500)]
        public string Message { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int userId { get; set; }
        public User User { get; set; }
        [Column(TypeName = "int(10)")]
        public int From { get; set; }
        [Column(TypeName = "int(10)")]
        public int To { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

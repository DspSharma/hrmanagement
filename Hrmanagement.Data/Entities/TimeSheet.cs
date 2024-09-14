using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Data.Entities
{
    public class TimeSheet
    {
        [Key]
        public int id { get; set; }
        public int UserId { get; set; } 
        public User User { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        [StringLength(50)]
        public string TaskTitle { get; set; }
        [StringLength(500)]
        public string TaskDescription { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal Hours { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

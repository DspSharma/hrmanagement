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
    public class UserMemo
    {
        [Key, Column(TypeName = "int(11)")]
        public int id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        [StringLength(50, ErrorMessage = "Title must be at most 50 characters")]
        public string? Title { get; set; } = null!;

        [StringLength(500, ErrorMessage = "Description must be at most 500 characters")]
        public string? Description { get; set; } = null!;

        [Url(ErrorMessage = "Invalid URL format")]
        public string Url { get; set; }


        public bool AvailableForPublic { get; set; } = false;

        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime UpdatedAt { get; set; }

    }
}

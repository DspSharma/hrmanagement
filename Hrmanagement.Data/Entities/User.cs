using Microsoft.AspNetCore.Http;
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
    public class User:BaseEntitiy
    {
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email  is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile is required.")]
        public string Mobile { get; set; }


        [Column(TypeName = "varchar(200)")]
        public string? ProfileImage { get; set; }
        public string? Password { get; set; } = null!;
        public string Role { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string? PasswordResetToken { get; set; } = null;

        [Column(TypeName = "datetime")]
        public DateTime? TokenExpiredOn { get; set; } = null;

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}

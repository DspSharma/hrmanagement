using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Core.DTO.DtoInput
{
    public class UserInput:BaseInput
    {
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email  is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile is required.")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Mobile is required.")]
        public string? Password { get; set; }
        public string Role { get; set; }

        public IFormFile? ImageFile { get; set; }
        public string? ProfileImage { get; set; }
    }
}

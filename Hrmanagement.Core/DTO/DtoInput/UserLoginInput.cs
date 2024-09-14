using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Core.DTO.DtoInput
{
    public class UserLoginInput
    {
        [Required(ErrorMessage = "Email filed is required")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password filed is required")]
        public string? Password { get; set; }
    }
}

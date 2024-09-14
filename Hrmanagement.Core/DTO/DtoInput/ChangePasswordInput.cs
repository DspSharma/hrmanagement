using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Core.DTO.DtoInput
{
    public class ChangePasswordInput
    {
        //public string UserId { get; set; }
        //public string OldPassword { get; set; }
        //public string NewPassword { get; set; }

        [Required(ErrorMessage = "Old password is required field.")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New password is required field.")]
        public string NewPassword { get; set; }
    }
}

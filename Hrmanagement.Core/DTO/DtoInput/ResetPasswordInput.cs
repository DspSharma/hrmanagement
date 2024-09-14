using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Core.DTO.DtoInput
{
    public class ResetPasswordInput
    {
        public string? Token { get; set; }
        public string? ToEmail { get; set; }
        public string? Newpassword { get; set; }
        public string? Confirmpassword { get; set; }
        public DateTime? PasswordResetTokenExpire { get; set; }
    }
}

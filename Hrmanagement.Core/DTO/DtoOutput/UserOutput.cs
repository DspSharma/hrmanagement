using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Core.DTO.DtoOutput
{
    public class UserOutput:BaseOutput
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string? Password { get; set; }
        public string Role { get; set; }

        public string? ProfileImage { get; set; }
        public string LocalOrgImage { get; set; }
    }
}

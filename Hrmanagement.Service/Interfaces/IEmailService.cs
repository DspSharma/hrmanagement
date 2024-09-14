using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Service.Interfaces
{
    public interface IEmailService
    {
        public bool SendEmail(string subject, string message, string toEmail);
    }
}

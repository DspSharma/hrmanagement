using Hrmanagement.Core.Helper;
using Hrmanagement.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Service
{
    public class EmailService : IEmailService
    {
        public IConfiguration _configuration { get; }

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool SendEmail(string subject, string message, string toEmail)
        {
            var mail = new Email()
            {
                Host = _configuration.GetSection("EmailConfig:Host").Value, //"email-smtp.ap-northeast-1.amazonaws.com",
                UserName = _configuration.GetSection("EmailConfig:UserName").Value, //"AKIAXT4OVLR63DSUEMZC",
                Password = _configuration.GetSection("EmailConfig:Password").Value, //"BF/jEsTmzPfcF1urnJIMnThruUyxVmz1t7J7nAAgKB+8",
                Port = Convert.ToInt32(_configuration.GetSection("EmailConfig:Port").Value), //587,
                From = _configuration.GetSection("EmailConfig:From").Value, //"deepaktanksgm@gmail.com",
                FromName = _configuration.GetSection("EmailConfig:FromName").Value,
                To = toEmail,
                Subject = subject,
                MailBody = message,
                MailBodyManualSupply = true,
                CC = "",
                BCC = "",
                EmailTemplateFileName = "",
                AttachFile = ""
            };

            mail.SendEmail();
            //await mail.SendEmailAsync();
            return true;
        }
    }
}

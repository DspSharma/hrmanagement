using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Core.DTO.DtoOutput
{
    public class SMSLogsOutput
    {
        public int id { get; set; }
        public int? Pid { get; set; } = null!;
        public string Title { get; set; }
        public string Message { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int userId { get; set; }
        public string UserName { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

using Hrmanagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Core.DTO.DtoOutput
{
    public class TimeSheetOutput
    {
        public int id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public decimal Hours { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

       public string dayname { get; set; }
    }
}

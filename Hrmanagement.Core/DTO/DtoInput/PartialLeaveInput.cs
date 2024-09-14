using Hrmanagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Core.DTO.DtoInput
{
    public class PartialLeaveInput
    {
        public int id { get; set; }
        public int UserId { get; set; }
        public string Detail { get; set; }
        public DateTime PauseTime { get; set; }
        public DateTime? ResumeTime { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

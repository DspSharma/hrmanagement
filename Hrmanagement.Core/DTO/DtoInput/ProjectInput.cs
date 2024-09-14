using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Core.DTO.DtoInput
{
    public class ProjectInput : BaseInput
    {
        public string Title { get; set; }
        public string Description { get; set; } 
        public string Url { get; set; } 
        public string Status { get; set; } 
    }
}

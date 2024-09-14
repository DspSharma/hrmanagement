using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Core.DTO.DtoOutput
{
    public class ProjectOutput : BaseOutput
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Status { get; set; }
    }
}

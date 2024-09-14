using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Core.Models
{
    public class ApiResponseModel<T>
    {
        public bool succeed { get; set; }
        public string error { get; set; }
        public string message { get; set; }
        public T data { get; set; }

        public int TotalCount { get; set; }
    }
}

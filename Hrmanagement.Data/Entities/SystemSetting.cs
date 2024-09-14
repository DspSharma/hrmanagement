using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Data.Entities
{
    public class SystemSetting:BaseEntitiy
    {
        [StringLength(100)]
        public string Key { get; set; }

        [StringLength(120)]
        public string Value { get; set; }
    }
}

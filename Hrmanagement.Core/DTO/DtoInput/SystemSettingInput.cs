using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Core.DTO.DtoInput
{
    public class SystemSettingInput:BaseInput
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}

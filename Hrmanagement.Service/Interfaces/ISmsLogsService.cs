using Hrmanagement.Core.DTO.DtoInput;
using Hrmanagement.Core.DTO.DtoOutput;
using Hrmanagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Service.Interfaces
{
    public interface ISmsLogsService
    {
        Task<ApiResponseModel<SMSLogsOutput>> AddSmsLogs(SMSlogsInput value);
        Task<ApiResponseModel<List<SMSLogsOutput>>> GetAllSmsLogs();
    }
}

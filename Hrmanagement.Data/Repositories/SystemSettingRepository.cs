using Hrmanagement.Data.DBContext;
using Hrmanagement.Data.Entities;
using Hrmanagement.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Data.Repositories
{
    public class SystemSettingRepository:GenericRepository<SystemSetting> ,ISystemSettingRepository
    {
        private readonly HrManagementContext _dbcontext;
        public SystemSettingRepository(HrManagementContext Dbcontext) : base(Dbcontext)
        {
            _dbcontext = Dbcontext;
        }
    }
}

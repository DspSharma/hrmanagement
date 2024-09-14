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
    public class PartialLeaveRepository : GenericRepository<PartialLeave>, IPartialLeaveRepository
    {
        private readonly HrManagementContext _dbcontext;
        public PartialLeaveRepository(HrManagementContext Dbcontext) : base(Dbcontext)
        {
            _dbcontext = Dbcontext;
        }
    }
}

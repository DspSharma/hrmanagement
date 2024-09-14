using Hrmanagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Data.DBContext
{
    public class HrManagementContext : DbContext
    {

        public HrManagementContext()
        {
        }

        public HrManagementContext(DbContextOptions<HrManagementContext> options)
        : base(options)
        {
        }

        //public DbSet<User> Users { get; set; }
        public virtual DbSet<User> users { get; set; } = null!;
        public virtual DbSet<Holiday> holidays { get; set; } = null!;
        public virtual DbSet<Leave> leaves { get; set; } = null!;
        public virtual DbSet<Attendance> attendances { get; set; } = null!;

        public virtual DbSet<ApiCredentials> apicredential { get; set; } = null!;
        public virtual DbSet<SystemSetting> systemsettings { get; set; } = null!;

        public virtual DbSet<UserMemo> usermemos { get; set; } = null!;
        public virtual DbSet<Project> projects { get; set; } = null!;
        public virtual DbSet<SMSLogs> smslogs { get; set; } = null!;
        public virtual DbSet<TimeSheet> timesheets { get; set; } = null!;

        public virtual DbSet<PartialLeave> partialleaves { get; set; } = null!;

    }
}

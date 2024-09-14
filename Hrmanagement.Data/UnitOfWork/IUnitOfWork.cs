using Hrmanagement.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }
        IHolidayRepository Holiday { get; }
        ILeaveRepository Leave { get; }
        IAttendanceRepository Attendance { get; }
        IApiCredentialsRepositoryp ApiCredentials { get; }
        ISystemSettingRepository SystemSetting { get; }
        IUserMemoRepository UserMemo { get; }
        IProjectRepository Project { get; }
        ISmsLogsRepository SmsLogs { get; }
        ITimeSheetRepository TimeSheet { get; }
        IPartialLeaveRepository PartialLeave { get; }


        int Save();
        Task<int> SaveAsync();
        Task DisposeAsync();
        bool HasChanges();
        Task CreateTransactionAsync();
        Task RollbackAsync();
        Task CommitTransactionAsync();
        IExecutionStrategy GetExecutionStrategy();
        Task RollbackTransactionAsync();
    }
}

using Hrmanagement.Data.DBContext;
using Hrmanagement.Data.Repositories;
using Hrmanagement.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrmanagement.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HrManagementContext _dbContext;
        private IDbContextTransaction _objTran;
        private bool disposed = false;
        public IUserRepository User { get; }
        public IHolidayRepository Holiday { get; }
        public ILeaveRepository Leave { get; }
        public IAttendanceRepository Attendance { get; }
        public IApiCredentialsRepositoryp ApiCredentials { get; }
        public ISystemSettingRepository SystemSetting { get; }
        public IUserMemoRepository UserMemo { get; }
        public IProjectRepository Project { get; }
        public ISmsLogsRepository SmsLogs { get; }

        public ITimeSheetRepository TimeSheet { get; }

        public IPartialLeaveRepository PartialLeave { get; }

        public UnitOfWork(HrManagementContext DbContext)
        {
            _dbContext = DbContext;
            User = new UserRepository(_dbContext);
            Holiday = new HolidayRepository(_dbContext);
            Leave = new LeaveRepository(_dbContext);
            Attendance = new AttendanceRepository(_dbContext);
            ApiCredentials = new ApiCredentialsRepository(_dbContext);
            SystemSetting = new SystemSettingRepository(_dbContext);
            UserMemo = new UserMemoRepository(_dbContext);
            Project = new ProjectRepository(_dbContext);
            SmsLogs = new SmsLogsRepository(_dbContext);
            TimeSheet = new TimeSheetRepository(_dbContext);
            PartialLeave = new PartialLeaveRepository (_dbContext);
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }
        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        public bool HasChanges()
        {
            return _dbContext.ChangeTracker.HasChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            disposed = true;
        }

        public async Task DisposeAsync()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }

        protected virtual async Task DisposeAsync(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    await _dbContext.DisposeAsync();
                }
            }
            disposed = true;
        }

        public async Task CreateTransactionAsync()
        {
            _objTran = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _objTran.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            await _objTran.RollbackAsync();
            await _objTran.DisposeAsync();
        }

        public IExecutionStrategy GetExecutionStrategy()
        {
            return _dbContext.Database.CreateExecutionStrategy();
        }

        public Task RollbackTransactionAsync()
        {
            throw new NotImplementedException();
        }
    }
}

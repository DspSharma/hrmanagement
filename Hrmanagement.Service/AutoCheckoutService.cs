using Hrmanagement.Core.Helper;
using Hrmanagement.Data.DBContext;
using Hrmanagement.Data.Entities;
using Hrmanagement.Data.UnitOfWork;
using Hrmanagement.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hrmanagement.Service
{
    public class AutoCheckoutService : BackgroundService
    {
        private readonly ILogger<AutoCheckoutService> _logger;
        private readonly IServiceProvider _serviceProvider;
        public IUnitOfWork _unitOfWork;
        public IEmailService _emailService;
        private readonly TimeSpan _checkoutTime = new TimeSpan(19, 30, 0); // Set checkout time to 7:30 PM

        public AutoCheckoutService(IServiceProvider serviceProvider, ILogger<AutoCheckoutService> logger,IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await SendReminders();
                await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
            }
        }

        private async Task SendReminders()
        {
            var usersToRemind = _unitOfWork.Attendance.GetWhere(a =>
                !a.IsCheckOut && a.IsPause && a.CheckIn.Date == DateTime.UtcNow.Date).ToList();
            
            //User userattendance = _unitOfWork.User.GetWhere(x => x.Id ==usersToRemind. ).FirstOrDefault();
            foreach (var user in usersToRemind)
            {
             
                //string subject = "This is to inform you that we have received a request for Attendance from one of our users";
                //string Message = $"<html><body>" +
                //    $"<h1>Dear {userName}</h1>" +
                //    $"<h5>Email :-  {userEmail}</h5>" +
                //    $"<p>CheckIn :-  {userCheckIn}</p>" +
                //    $" <p>CheckOut :- {userCheckOut}</p> " +
                //    $" <p>TotalHours :- {totalHours}</p>" +
                //    $" <a href='http://hrm.tagosys.com/Admin/Auth/Login/'>Click here to view the page </a>" +
                //    $"</body></html>";
                //string toEmail = user.email;

                //var emailrslt = _emailService.SendEmail(subject, Message, toEmail);

               // var emailrslt = _emailService.SendEmail(subject, Message, toEmail);
            }
        }
        //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        //{
        //    while (!stoppingToken.IsCancellationRequested)
        //    {
        //        var now = DateTime.UtcNow;
        //        var timeToNextRun = _checkoutTime.Subtract(now.TimeOfDay);
        //        if (timeToNextRun <= TimeSpan.Zero)
        //        {
        //            // Time to run the checkout process
        //            await ProcessAutoCheckout();
        //            // Wait till the next day to run again
        //            timeToNextRun = _checkoutTime.Add(new TimeSpan(24, 0, 0)).Subtract(now.TimeOfDay);
        //        }
        //        await Task.Delay(timeToNextRun, stoppingToken);
        //    }
        //}

        //private async Task ProcessAutoCheckout()
        //{
        //    using (var scope = _serviceProvider.CreateScope())
        //    {
        //        var dbContext = scope.ServiceProvider.GetRequiredService<HrManagementContext>();
        //        var usersToCheckout = await dbContext.attendances
        //            .Where(a => a.IsPause && !a.IsCheckOut && a.CheckIn.Date == DateTime.UtcNow.Date).ToList();

        //        if (usersToCheckout.Any())
        //        {
        //            foreach (var attendance in usersToCheckout)
        //            {
        //                attendance.IsPause = false;
        //                attendance.CheckOut = DateTime.UtcNow;
        //                attendance.IsCheckOut = true;
        //            }

        //            await dbContext.SaveChangesAsync();
        //            _logger.LogInformation("Processed auto-checkout for {Count} users.", usersToCheckout.Count);
        //        }
        //    }
        //}
    }
}

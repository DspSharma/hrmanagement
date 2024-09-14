using Hrmanagement.Core.Mapping;
using Hrmanagement.Data.DBContext;
using Hrmanagement.Data.UnitOfWork;
using Hrmanagement.Service;
using Hrmanagement.Service.Interfaces;
using Hrmanagement.Services;
using Hrmanagement.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Mvc Services Start
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IUserServices, UserServices>();
builder.Services.AddTransient<IApiServices, ApiServices>();
builder.Services.AddTransient<IAuthServices, AuthServices>();
builder.Services.AddTransient<IMvcHolidayService,MvcHolidayService>();
builder.Services.AddTransient<IMvcLeaveServices,MvcLeaveServices>();
builder.Services.AddTransient<IMvcAttendanceServices,MvcAttendanceServices>();
builder.Services.AddTransient<IMvcApiCredentialsServices,MvcApiCredentialsServices>();
builder.Services.AddTransient<IMvcUserMemoServices,MvcUserMemoServices>();
builder.Services.AddTransient<IMvcSystemSettingServices,MvcSystemSettingServices>();
builder.Services.AddTransient<IMvcProjectServices,MvcProjectServices>();
builder.Services.AddTransient<IMvcTimeSheetService,MvcTimeSheetService>();

// MVc Services End

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

// Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(Options =>
{
    Options.IdleTimeout = TimeSpan.FromHours(24);
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//app.UseAuthentication();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
       name: "Public",
       pattern: "Public/{controller=Index}/{action=Index}/{id?}",
        //pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
       defaults: new { area = "Public" }
    );
    endpoints.MapAreaControllerRoute(
                      name: "default",
                      areaName: "Admin",
                      defaults: new { area = "Admin" },
                      pattern: "{area}/{controller=Auth}/{action=Login}/{id?}"
    );
});

app.Run();

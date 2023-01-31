using FutureFridges.Business.Notifications;
using FutureFridges.Business.UserManagement;
using FutureFridges.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var _Builder = WebApplication.CreateBuilder(args);

// Add services to the container.
_Builder.Services.AddRazorPages();

_Builder.Services.AddDbContext<FridgeDBContext>(options => options.UseSqlServer(_Builder.Configuration.GetConnectionString("Default")));

_Builder.Services
    .AddIdentity<FridgeUser, IdentityRole>()
    .AddEntityFrameworkStores<FridgeDBContext>()
    .AddDefaultTokenProviders();

_Builder.Services.AddTransient<NotificationController>();

var _App = _Builder.Build();

// Configure the HTTP request pipeline.
if (!_App.Environment.IsDevelopment())
{
    _App.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    _App.UseHsts();
}

_App.UseHttpsRedirection();
_App.UseStaticFiles();

_App.UseRouting();

_App.UseAuthentication();
_App.UseAuthorization();

_App.MapRazorPages();

_App.Run();

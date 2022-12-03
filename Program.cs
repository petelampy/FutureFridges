using FutureFridges.Data;
using Microsoft.EntityFrameworkCore;

var _Builder = WebApplication.CreateBuilder(args);

// Add services to the container.
_Builder.Services.AddRazorPages();

_Builder.Services.AddDbContext<FridgeDBContext>(options => options.UseSqlServer(_Builder.Configuration.GetConnectionString("Default")));

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

_App.UseAuthorization();

_App.MapRazorPages();

_App.Run();
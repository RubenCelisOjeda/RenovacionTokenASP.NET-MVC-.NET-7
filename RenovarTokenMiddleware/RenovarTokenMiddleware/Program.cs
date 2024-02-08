using Microsoft.AspNetCore.Cors.Infrastructure;
using RenovarTokenMiddleware.Util;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

#region [Services]

#region [IoC]
//IoCGeneralService.AddRegistration(builder.Services);
#endregion

#region [Log]
//LogService.AddLogging(builder.Services);
#endregion

#region [Cors]
//CorsService.AddCors(builder.Services);
#endregion

#region [Authenticacion]
//JwtService.AddAuthentication(builder.Services);
#endregion

#region [Session]
//SessionService.AddSession(builder.Services);
#endregion

#region [Configuration areas]
//AreasViewLocationFilter.AddAreas(builder.Services);
#endregion

#endregion

#region [Initialize]
AppSettings.Initialize(builder.Configuration);
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

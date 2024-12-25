using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Application.Extensions;
using ProjectManager.Infrastructure.Extensions;
using ProjectManager.Infrastructure.Persistence;
using ProjectManager.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000);
    options.ListenAnyIP(5001, listenOptions =>
    {
        listenOptions.UseHttps("/app/certificates/aspnetapp.pfx", "Pass@ord1");
    });
});

// Add services to the container.
builder.Services.AddControllersWithViews(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

app.UseMiddleware<ExceptionHandler>();


using (var migrateScope = app.Services.CreateScope())
{
    var dbContext = migrateScope.ServiceProvider.GetRequiredService<ProjectManagerDbContext>();
    dbContext.Database.Migrate();
}

var scope = app.Services.CreateScope();

var userRolesSeeder = scope.ServiceProvider.GetRequiredService<UserRolesSeeder>();
var projectRolesSeeder = scope.ServiceProvider.GetRequiredService<ProjectRolesSeeder>();
await userRolesSeeder.Seed(scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>());
await projectRolesSeeder.Seed();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();

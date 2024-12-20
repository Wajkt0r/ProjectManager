using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using ProjectManager.Application.Extensions;
using ProjectManager.Infrastructure.Extensions;
using ProjectManager.Infrastructure.Persistence;
using ProjectManager.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();

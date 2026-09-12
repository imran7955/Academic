using LmsProject.Application.Services;
using LmsProject.Domain.Repositories;
using LmsProject.Infrastructure.Persistence;
using LmsProject.Infrastructure.Repositories;
using LmsProject.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// FIXED: Converted to AddIdentity and added AddRoles<IdentityRole>() to explicitly support RBAC
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // Set to false for easier development/testing
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configure Application Cookie redirection parameters securely
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";       // Where users go if not logged in
    options.AccessDeniedPath = "/Account/AccessDenied"; // Where users go if they don't have the right Role
});

builder.Services.AddControllersWithViews();

// ============================================================================
//  Clean Architecture Dependency Injections
// ============================================================================
// 1. Register Concrete Infrastructure Repositories
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();
builder.Services.AddScoped<ISyllabusRepository, SyllabusRepository>();

// 2. Register Application Core Processing Use Case Services
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IInstructorService, InstructorService>();
// ============================================================================

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// CRITICAL PIPELINE ORDER: Authentication MUST run right before Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

//app.MapRazorPages().WithStaticAssets();







// ... your existing setup up to app.MapRazorPages()

// FIXED: Seed database security roles before running the application
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await DataSeeder.SeedRolesAndAdminAsync(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding security roles.");
    }
}

app.Run();
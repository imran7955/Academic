using Microsoft.AspNetCore.Identity;

namespace LmsProject.Web
{
    public static class DataSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string[] roleNames = { "Admin", "Instructor", "Student" };

            // 1. Create Roles if they do not exist
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // 2. Create a default Admin User for testing
            var adminEmail = "admin@lms.com";
            var defaultAdmin = await userManager.FindByEmailAsync(adminEmail);

            if (defaultAdmin == null)
            {
                var adminUser = new IdentityUser
                {
                    UserName = "admin@lms.com",
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                // Password matches your simple restrictions setup in Program.cs
                var createPowerUser = await userManager.CreateAsync(adminUser, "Admin123");
                if (createPowerUser.Succeeded)
                {
                    // Assign the Admin user to the Admin Role
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
using LmsProject.Domain.Entities;
using LmsProject.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LmsProject.Application.Services;
using LmsProject.Infrastructure.Persistence; // Ensure access to DbContext for profile lookups

namespace LmsProject.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IInstructorService _instructorService;
        private readonly ApplicationDbContext _context; // Added for direct profile lookup

        public UserController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IInstructorService instructorService,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _instructorService = instructorService;
            _context = context;
        }

        public async Task<IActionResult> SystemUsers()
        {
            var allUsers = await _userManager.Users.ToListAsync();
            var userListWithRoles = new List<UserManagementViewModel>();

            foreach (var user in allUsers)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userListWithRoles.Add(new UserManagementViewModel
                {
                    UserId = user.Id,
                    Email = user.Email ?? string.Empty,
                    Username = user.UserName ?? string.Empty,
                    Role = roles.FirstOrDefault() ?? "General User"
                });
            }

            return View(userListWithRoles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUserCredentials(string userId, string email, string newPassword, string targetRole)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("Target user record context not found.");
            }

            // 1. Update Identity User basic details
            user.Email = email;
            user.UserName = email;
            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
            {
                AddErrorsToModelState(updateResult);
                return await RedirectToSystemUsersWithErrors();
            }

            // 2. Handle Password resets
            if (!string.IsNullOrEmpty(newPassword))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var passwordResult = await _userManager.ResetPasswordAsync(user, token, newPassword);

                if (!passwordResult.Succeeded)
                {
                    AddErrorsToModelState(passwordResult);
                    return await RedirectToSystemUsersWithErrors();
                }
            }

            // 3. Update RBAC Roles
            var currentRoles = await _userManager.GetRolesAsync(user);
            if (!currentRoles.Contains(targetRole))
            {
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
                await _userManager.AddToRoleAsync(user, targetRole);
            }

            // 4. SYNC LOGIC: Domain Table Maintenance
            var existingProfile = await _context.UserProfiles
                .FirstOrDefaultAsync(p => p.IdentityUserId == userId);

            if (targetRole == "Instructor")
            {
                // Ensure they exist in the Instructors table
                var allInstructors = await _instructorService.GetAllInstructorsAsync();
                if (!allInstructors.Any(i => i.IdentityUserId == userId))
                {
                    // Use their actual name from UserProfiles, fallback to Email if profile missing
                    string displayName = existingProfile?.FullName ?? email.Split('@')[0];
                    await _instructorService.RegisterInstructorAsync(displayName, userId);
                }
            }
            else
            {
                // If they are no longer an Instructor, remove them from the Instructor domain table
                var instructorEntry = await _context.Instructors
                    .FirstOrDefaultAsync(i => i.IdentityUserId == userId);

                if (instructorEntry != null)
                {
                    _context.Instructors.Remove(instructorEntry);
                    await _context.SaveChangesAsync();
                }
            }

            // Update the general UserProfile AccountType to match the new role
            if (existingProfile != null)
            {
                existingProfile.AccountType = targetRole;
                existingProfile.EmailAddress = email; // Keep email in sync
                _context.UserProfiles.Update(existingProfile);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(SystemUsers));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNewUser(string name, string email, string password, string targetRole)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(targetRole))
            {
                ModelState.AddModelError(string.Empty, "Email, Password, and Target Security Role are required fields.");
                return await RedirectToSystemUsersWithErrors();
            }

            var identityUser = new IdentityUser { UserName = email, Email = email, EmailConfirmed = true };
            var identityResult = await _userManager.CreateAsync(identityUser, password);

            if (identityResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(identityUser, targetRole);

                string profileName = !string.IsNullOrEmpty(name) ? name : email.Split('@')[0];

                // Create the master UserProfile
                await _instructorService.CreateUserProfileAsync(identityUser.Id, profileName, email, targetRole);

                // If Instructor, create the specialized table entry
                if (targetRole == "Instructor")
                {
                    await _instructorService.RegisterInstructorAsync(profileName, identityUser.Id);
                }

                return RedirectToAction(nameof(SystemUsers));
            }

            foreach (var error in identityResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return await RedirectToSystemUsersWithErrors();
        }

        private void AddErrorsToModelState(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private async Task<IActionResult> RedirectToSystemUsersWithErrors()
        {
            var allUsers = await _userManager.Users.ToListAsync();
            var userListWithRoles = new List<UserManagementViewModel>();

            foreach (var user in allUsers)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userListWithRoles.Add(new UserManagementViewModel
                {
                    UserId = user.Id,
                    Email = user.Email ?? string.Empty,
                    Username = user.UserName ?? string.Empty,
                    Role = roles.FirstOrDefault() ?? "General User"
                });
            }
            return View(nameof(SystemUsers), userListWithRoles);
        }
    }
}
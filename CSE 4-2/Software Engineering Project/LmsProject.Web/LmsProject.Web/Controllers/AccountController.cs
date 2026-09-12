using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LmsProject.Web.Models;
using LmsProject.Application.Services; // Refers to your core IInstructorService implementation layer

namespace LmsProject.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IInstructorService _instructorService; // Centralized Service layer access field

        // Injecting the service layer along with identity management utilities
        public AccountController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IInstructorService instructorService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _instructorService = instructorService;
        }

        // GET: Account/Login
        [HttpGet]
        public IActionResult Login() => View();

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid) return View(model);

            // Attempt to authenticating user session via cookies identity schemes
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("ViewCourses", "Course");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt. Please check your credentials.");
            return View(model);
        }

        // GET: Account/Register
        [HttpGet]
        public IActionResult Register() => View();

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            // 1. Instantiate security access record mapping parameters
            var user = new IdentityUser { UserName = model.Email, Email = model.Email, EmailConfirmed = true };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // 2. Identify target operational security privilege grouping role dynamically
                // If your RegisterViewModel doesn't use SelectedRole, default it to "Student"
                string assignedRole = !string.IsNullOrEmpty(model.SelectedRole) ? model.SelectedRole : "Student";
                await _userManager.AddToRoleAsync(user, assignedRole);

                // Determine display names dynamically based on ViewModel inputs
                string fallbackName = !string.IsNullOrEmpty(model.Name) ? model.Name : model.Email.Split('@')[0];

                // 3. PIPELINE SERVICE HOOK: Propagate custom profile indexes down to UserProfiles domain table
                await _instructorService.CreateUserProfileAsync(user.Id, fallbackName, model.Email, assignedRole);

                // 4. CROSS-DOMAIN ADAPTER: If they registered as an Instructor, automatically seed the Instructor table
                if (assignedRole == "Instructor")
                {
                    await _instructorService.RegisterInstructorAsync(fallbackName, user.Id);
                }

                // 5. Establish immediate user session and redirect safely
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("ViewCourses", "Course");
            }

            // Populate infrastructure transaction identity errors back into validation collection
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        // POST: Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("ViewCourses", "Course");
        }

        // GET: Account/AccessDenied
        public IActionResult AccessDenied() => View();
    }
}
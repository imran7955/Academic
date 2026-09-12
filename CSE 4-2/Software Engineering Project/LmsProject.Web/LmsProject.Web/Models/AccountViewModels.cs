using System.ComponentModel.DataAnnotations;

namespace LmsProject.Web.Models
{
    public class LoginViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter your full name.")]
        [Display(Name = "Full Name")]
        public string Name { get; set; } = string.Empty; // FIXED: Added to capture Instructor/Student profile name

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "Password must be at least 4 characters long.")]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select an account type.")]
        public string SelectedRole { get; set; } = string.Empty; // "Student" or "Instructor"
    }
}
using Student.Shared.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Student.Shared.Models.Authentication
{
    public class RegisterRequestDTO
    {
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(250, MinimumLength = 2, ErrorMessage = "First Name must be between 2 and 250 characters")]
        [RegularExpression(@"^[a-zA-Z'-]+$", ErrorMessage = "First Name can only contain letters, apostrophes, and hyphens")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(250, MinimumLength = 2, ErrorMessage = "Last Name must be between 2 and 250 characters")]
        [RegularExpression(@"^[a-zA-Z'-]+$", ErrorMessage = "Last Name can only contain letters, apostrophes, and hyphens")]
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";

        [Required(ErrorMessage = "PhoneNumber is required")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(256, ErrorMessage = "Email cannot exceed 256 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(256, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 256 characters")]
        [RegularExpression(@"^[a-zA-Z0-9_-]+$", ErrorMessage = "Username can only contain letters, numbers, underscores, and hyphens")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 12,ErrorMessage = "Password must be between 12 and 100 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).*$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character")]
        [UniqueCharacters(4, ErrorMessage = "Password must contain at least 4 unique characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Student.Shared.DomainModels.Authentication.SystemUsers
{
    public class ApplicationUser : IdentityUser
    {
        // Personal Information
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(250, MinimumLength = 2, ErrorMessage = "First Name must be between 2 and 250 characters")]
        [RegularExpression(@"^[a-zA-Z'-]+$", ErrorMessage = "First Name can only contain letters, apostrophes, and hyphens")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(250, MinimumLength = 2, ErrorMessage = "Last Name must be between 2 and 250 characters")]
        [RegularExpression(@"^[a-zA-Z'-]+$", ErrorMessage = "Last Name can only contain letters, apostrophes, and hyphens")]
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(256, ErrorMessage = "Email cannot exceed 256 characters")]
        public override string Email { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(256, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 256 characters")]
        [RegularExpression(@"^[a-zA-Z0-9_-]+$", ErrorMessage = "Username can only contain letters, numbers, underscores, and hyphens")]
        public override string UserName { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required")]
        public override string PhoneNumber { get; set; }


        // Security & Authentication
        public bool IsActive { get; set; } = true;
        public bool IsTwoFactorEnabled { get; set; } = false;
        public int AccessFailedCount { get; set; }
        public bool RequirePasswordReset { get; set; } = false;

        // Last Login & Tracking
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string ModifiedBy { get; set; } = string.Empty;
        public DateTime? LastModifiedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }


        // Soft Delete Properties
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        public void SoftDelete()
        {
            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
        }
    }
}

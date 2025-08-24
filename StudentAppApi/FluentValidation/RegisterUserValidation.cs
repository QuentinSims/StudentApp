using FluentValidation;
using Student.Shared.Models.Authentication;

namespace StudentAppApi.FluentValidation
{
    public class RegisterUserValidation : AbstractValidator<RegisterRequestDTO>
    {
        public RegisterUserValidation()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First Name is required")
                .Length(2, 250).WithMessage("First Name must be between 2 and 250 characters");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last Name is required")
                .Length(2, 250).WithMessage("Last Name must be between 2 and 250 characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .MaximumLength(256).WithMessage("Email cannot exceed 256 characters")
                .EmailAddress().WithMessage("Invalid email address");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required")
                .Length(3, 256).WithMessage("Username must be between 3 and 256 characters");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(12).WithMessage("Password must be at least 12 characters")
                .MaximumLength(100).WithMessage("Password cannot exceed 100 characters")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches(@"[0-9]").WithMessage("Password must contain at least one number")
                .Matches(@"[^\da-zA-Z]").WithMessage("Password must contain at least one special character")
                .Must(password => password == null || password.Distinct().Count() >= 4)
                .WithMessage("Password must contain at least 4 unique characters");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Passwords do not match");
        }
    }
}

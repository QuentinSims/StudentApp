using FluentValidation;
using Student.Shared.Models.Authentication;

namespace StudentAppApi.FluentValidation
{
    public class LoginUserValidation : AbstractValidator<LoginRequestDTO>
    {
        public LoginUserValidation()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email address");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Email is required");
        }
    }
}

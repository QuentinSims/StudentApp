using System.ComponentModel.DataAnnotations;

namespace Student.Shared.Models.Authentication
{
    public class LoginRequestDTO
    {
        [Required(ErrorMessage = "The Email field is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "The Password field is required.")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public bool IsEmail => new EmailAddressAttribute().IsValid(Email);
    }
}

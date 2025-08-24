using Student.Shared.Models.Authentication;
using Student.Shared.Models.UserManagement;

namespace StudentAppApi.Interfaces.Authentication
{
    public interface IUserService
    {
        // User Creation
        Task<UserModelDTO> CreateUserAsync(RegisterRequestDTO model, UserClaims cliams, bool requirePasswordReset = false);
        // Sign In
        Task<LoginResponseDTO> SignInUserAsync(LoginRequestDTO model, UserClaims cliams);
        //sign out
        Task SignOutAsync();
    }
}

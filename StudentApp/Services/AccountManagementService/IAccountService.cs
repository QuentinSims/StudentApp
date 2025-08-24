using Student.Shared.Models.Authentication;
using Student.Shared.Models.UserManagement;

namespace StudentApp.Services.AccountManagementService
{
    public interface IAccountService
    {
        Task<UserModelDTO> RegisterAsync(RegisterRequestDTO model);
        Task<LoginResponseDTO> LoginAsync(LoginRequestDTO model);
        Task<bool> LogoutAsync();
    }
}

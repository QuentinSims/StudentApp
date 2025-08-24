using Microsoft.AspNetCore.Components.Authorization;
using Student.Shared.Models.Authentication;

namespace StudentApp.Services.AccountManagementService
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(LoginRequestDTO model);
        Task LogoutAsync();
        Task<AuthenticationState> GetAuthenticationStateAsync();
    }
}

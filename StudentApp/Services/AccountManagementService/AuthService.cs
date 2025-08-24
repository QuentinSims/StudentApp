using Microsoft.AspNetCore.Components.Authorization;
using Student.Shared.Models.Authentication;
using StudentApp.Services.AuthenicationManager;

namespace StudentApp.Services.AccountManagementService
{
    public class AuthService : IAuthService
    {
        private readonly IAccountService _accountService;
        private readonly CustomAuthenticationStateProvider _authStateProvider;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            IAccountService accountService,
            CustomAuthenticationStateProvider authStateProvider,
            ILogger<AuthService> logger)
        {
            _accountService = accountService;
            _authStateProvider = authStateProvider;
            _logger = logger;
        }

        public async Task<bool> LoginAsync(LoginRequestDTO model)
        {
            try
            {
                var loginResponse = await _accountService.LoginAsync(model);

                if (loginResponse != null && !string.IsNullOrEmpty(loginResponse.token))
                {
                    await _authStateProvider.MarkUserAsAuthenticated(loginResponse);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login failed");
                return false;
            }
        }

        public async Task LogoutAsync()
        {
            try
            {
                await _accountService.LogoutAsync();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Backend logout failed, continuing with local logout");
            }
            finally
            {
                await _authStateProvider.MarkUserAsLoggedOut();
            }
        }

        public async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return await _authStateProvider.GetAuthenticationStateAsync();
        }
    }
}

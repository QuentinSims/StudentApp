using Microsoft.AspNetCore.Components.Authorization;
using Student.Shared.Models.Authentication;
using StudentApp.Services.JwtService;
using System.Security.Claims;

namespace StudentApp.Services.AuthenicationManager
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ILogger<CustomAuthenticationStateProvider> _logger;

        public CustomAuthenticationStateProvider(IJwtTokenService jwtTokenService, ILogger<CustomAuthenticationStateProvider> logger)
        {
            _jwtTokenService = jwtTokenService;
            _logger = logger;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var token = await _jwtTokenService.GetTokenAsync();

                if (string.IsNullOrEmpty(token))
                {
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                }

                var claims = await _jwtTokenService.GetClaimsFromTokenAsync(token);

                if (!claims.Any())
                {
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                }

                var claimsIdentity = new ClaimsIdentity(claims, "jwt");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                return new AuthenticationState(claimsPrincipal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting authentication state");
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }

        public async Task MarkUserAsAuthenticated(LoginResponseDTO loginResponse)
        {
            await _jwtTokenService.SetTokenAsync(loginResponse.token);

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, $"{loginResponse.FirstName} {loginResponse.LastName}"),
            new Claim(ClaimTypes.NameIdentifier, loginResponse.Username),
            new Claim(ClaimTypes.Email, loginResponse.Email),
            new Claim("FirstName", loginResponse.FirstName),
            new Claim("LastName", loginResponse.LastName)
        };

            var claimsIdentity = new ClaimsIdentity(claims, "jwt");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _jwtTokenService.RemoveTokenAsync();
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymousUser)));
        }
    }
}

using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace StudentApp.Services.JwtService
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IJSRuntime _jsRuntime;
        private const string TokenKey = "authToken";

        public JwtTokenService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task SetTokenAsync(string token)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", TokenKey, token);
        }

        public async Task<string> GetTokenAsync()
        {
            return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", TokenKey);
        }

        public async Task RemoveTokenAsync()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", TokenKey);
        }

        public async Task<IEnumerable<Claim>> GetClaimsFromTokenAsync(string token)
        {
            if (string.IsNullOrEmpty(token))
                return new List<Claim>();

            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtHandler.ReadJwtToken(token);

            if (jwtToken.ValidTo < DateTime.UtcNow)
            {
                await RemoveTokenAsync();
                return new List<Claim>();
            }

            return jwtToken.Claims;
        }
    }
}

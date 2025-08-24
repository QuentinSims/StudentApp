using System.Security.Claims;

namespace StudentApp.Services.JwtService
{
    public interface IJwtTokenService
    {
        Task SetTokenAsync(string token);
        Task<string> GetTokenAsync();
        Task RemoveTokenAsync();
        Task<IEnumerable<Claim>> GetClaimsFromTokenAsync(string token);
    }
}

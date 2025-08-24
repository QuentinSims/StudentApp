using Student.Shared.Models.Authentication;
using StudentAppApi.Interfaces.Authentication;
using System.Security.Claims;

namespace StudentAppApi.Services.Authenication
{
    public class UserClaimsService : IUserClaimsService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserClaimsService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public UserClaims? GetUserClaims()
        {
            ClaimsPrincipal claimsPrincipal = _httpContextAccessor.HttpContext?.User;
            if (claimsPrincipal == null)
            {
                return null;
            }

            IEnumerable<Claim> claims = claimsPrincipal.Claims;
            return new UserClaims
            {
                UserIdString = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
                Email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                FirstName = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value?.Split(' ').FirstOrDefault(), // if FullName used
                LastName = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value?.Split(' ').Skip(1).FirstOrDefault(), // naive split
                Username = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value // fallback if no username
            };
        }

    }
}

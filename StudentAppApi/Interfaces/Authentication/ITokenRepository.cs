using Student.Shared.DomainModels.Authentication.SystemUsers;

namespace StudentAppApi.Interfaces.Authentication
{
    public interface ITokenRepository
    {
        string CreateToken(ApplicationUser user, bool RememberMe = false);
    }
}

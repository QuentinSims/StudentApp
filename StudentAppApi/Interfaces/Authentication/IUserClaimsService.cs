using Student.Shared.Models.Authentication;

namespace StudentAppApi.Interfaces.Authentication
{
    public interface IUserClaimsService
    {
        UserClaims? GetUserClaims();
    }
}

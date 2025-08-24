using Riok.Mapperly.Abstractions;
using Student.Shared.DomainModels.Authentication.SystemUsers;
using Student.Shared.Models.Authentication;

namespace StudentAppApi.Mapper.AuthenicationMapper
{
    [Mapper]
    public static partial class UserMapper
    {
        public static partial ApplicationUser ConvertResponseDTOToUser(RegisterRequestDTO model);
    }
}

using Riok.Mapperly.Abstractions;
using Student.Shared.DomainModels.Authentication.SystemUsers;
using Student.Shared.Models.UserManagement;

namespace StudentAppApi.Mapper.UserManagementMapper
{
    [Mapper]
    public static partial class UserManagementMapper
    {
        public static partial UserModelDTO ConvertResponseDTOToUser(ApplicationUser model);
    }
}

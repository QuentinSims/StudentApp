namespace Student.Shared.Models.Authentication
{
    public class UserClaims
    {
        public string? Username { get; set; } = string.Empty;
        public string? UserIdString { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
    }
}

namespace Student.Shared.Models.Authentication
{
    public class LoginResponseDTO
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? token { get; set; } = string.Empty;

    }
}

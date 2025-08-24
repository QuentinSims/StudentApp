namespace Student.Shared.Models.UserManagement
{
    public class UserModelDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName ?? ""} {LastName ?? ""}".Trim();
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsDeleted { get; set; }

    }
}

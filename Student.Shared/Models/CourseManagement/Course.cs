namespace Student.Shared.Models.CourseManagement
{
    public class Course
    {
        public Guid Id { get; set; }
        public string CourseCode { get; set; } = "";
        public string CourseName { get; set; } = "";
        public string Instructor { get; set; } = "";
        public int Credits { get; set; }
        public string Schedule { get; set; } = "";
        public string Department { get; set; } = "";
        public int AvailableSeats { get; set; }
        public int MaxSeats { get; set; }
    }
}

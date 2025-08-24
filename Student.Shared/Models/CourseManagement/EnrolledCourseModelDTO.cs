namespace Student.Shared.Models.CourseManagement
{
    public class EnrolledCourseModelDTO
    {
        public Guid Id { get; set; }
        public string CourseCode { get; set; } = "";
        public string CourseName { get; set; } = "";
        public string Instructor { get; set; } = "";
        public int Credits { get; set; }
        public string Schedule { get; set; } = "";
        public DateTime EnrollmentDate { get; set; }
        public string StudentId { get; set; } = "";
    }
}

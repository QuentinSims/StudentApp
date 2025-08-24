namespace Student.Shared.Models.CourseManagement
{
    public class LinkBetweenStudentAndCourse
    {
        public string StudentId { get; set; }
        public Guid CourseId { get; set; }
    }
}

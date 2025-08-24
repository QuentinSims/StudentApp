using Student.Shared.DomainModels.Authentication.SystemUsers;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student.Shared.DomainModels.CourseManagement
{
    public class EnrolledCourse : BaseEntity
    {
        public string CourseCode { get; set; } = "";
        public string CourseName { get; set; } = "";
        public string Instructor { get; set; } = "";
        public int Credits { get; set; }
        public string Schedule { get; set; } = "";
        public DateTime EnrollmentDate { get; set; }

        public string StudentId { get; set; }

        [ForeignKey(nameof(StudentId))]
        public virtual ApplicationUser Student { get; set; }
    }
}

using Riok.Mapperly.Abstractions;
using Student.Shared.Models.CourseManagement;

namespace StudentAppApi.Mapper.CourseManagementMapper
{
    [Mapper]
    public static partial class CourseManagementMapper
    {
        public static partial CourseModelDTO ConvertCourseToResponseDTO(Student.Shared.DomainModels.CourseManagement.Course model);
        public static partial EnrolledCourseModelDTO ConvertEnrolledToResponseDTO(Student.Shared.DomainModels.CourseManagement.EnrolledCourse model);
    }
}

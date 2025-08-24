
using Student.Shared.Models.Authentication;
using Student.Shared.Models.CourseManagement;

namespace StudentAppApi.Interfaces.CourseManagement
{
    public interface ICourseManagementService
    {
        Task<List<CourseModelDTO>> GetCoursesAsync();
        Task<List<EnrolledCourseModelDTO>> GetEnrolledCoursesAsync();
        Task<List<EnrolledCourseModelDTO>> GetCoursesLinkedToStudentAsync(string id);
        Task<bool> DeleteLinkBetweenStudentAndCourseAsync(LinkBetweenStudentAndCourse model, UserClaims? userClaims);
        Task<EnrolledCourseModelDTO> CreateLinkBetweenStudentAndCourseAsync(LinkBetweenStudentAndCourse model, UserClaims? userClaims);

    }
}

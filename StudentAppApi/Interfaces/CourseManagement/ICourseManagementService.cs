
using Student.Shared.Models.Authentication;
using Student.Shared.Models.CourseManagement;

namespace StudentAppApi.Interfaces.CourseManagement
{
    public interface ICourseManagementService
    {
        Task<List<CourseModelDTO>> GetCoursesAsync();
        Task<List<EnrolledCourseModelDTO>> GetCoursesLinkedToStudentAsync(string id);
        Task<bool> AddCourseAsync(AddEditCourseModelDTO model, UserClaims? userClaims);
        Task<bool> UpdateCourseAsync(Guid id, AddEditCourseModelDTO model, UserClaims? userClaims);
        Task<bool> DeleteLinkBetweenStudentAndCourseAsync(LinkBetweenStudentAndCourse model, UserClaims? userClaims);
    }
}

using Student.Shared.Models.CourseManagement;

namespace StudentApp.Services.CourseService
{
    public interface ICourseManagementService
    {

        Task<List<CourseModelDTO>> GetAllCoursesAsync();
        Task<List<EnrolledCourseModelDTO>> GetAllEnrolledCoursesAsync();
        Task<List<EnrolledCourseModelDTO>> GetCoursesLinkedToStudentAsync(string studentId);
        Task<bool> AddCourseAsync(AddEditCourseModelDTO model);
        Task<bool> UpdateCourseAsync(Guid courseId, AddEditCourseModelDTO model);
        Task<bool> DeleteLinkBetweenStudentAndCourseAsync(LinkBetweenStudentAndCourse model);
        Task<EnrolledCourseModelDTO> RegisterForCourseAsync(LinkBetweenStudentAndCourse model);
    }
}

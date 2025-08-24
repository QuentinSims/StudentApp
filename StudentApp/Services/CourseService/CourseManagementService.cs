using Student.Shared.Models.Authentication;
using Student.Shared.Models.CourseManagement;
using StudentApp.Services.AccountManagementService;
using System.Net.Http.Json;

namespace StudentApp.Services.CourseService
{
    public class CourseManagementService : ICourseManagementService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CourseManagementService> _logger;

        public CourseManagementService(HttpClient httpClient, ILogger<CourseManagementService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<bool> AddCourseAsync(AddEditCourseModelDTO model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/course", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteLinkBetweenStudentAndCourseAsync(LinkBetweenStudentAndCourse model)
        {
            var response = await _httpClient.DeleteAsync($"api/course/{model.CourseId}/deregisterstudentfromcourse/{model.StudentId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<EnrolledCourseModelDTO> RegisterForCourseAsync(LinkBetweenStudentAndCourse model)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/course/registerstudentforcourse", model);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<EnrolledCourseModelDTO>();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login");
                throw;
            }
        }

        public async Task<List<CourseModelDTO>> GetAllCoursesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<CourseModelDTO>>("api/course/getallcourses");
        }

        public async Task<List<EnrolledCourseModelDTO>> GetAllEnrolledCoursesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<EnrolledCourseModelDTO>>("api/course/getallenrolledcourses");
        }

        public async Task<List<EnrolledCourseModelDTO>> GetCoursesLinkedToStudentAsync(string studentId)
        {
            return await _httpClient.GetFromJsonAsync<List<EnrolledCourseModelDTO>>($"api/course/getbyid/courseslinkedtostudents/{studentId}");
        }

        public async Task<bool> UpdateCourseAsync(Guid courseId, AddEditCourseModelDTO model)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/course/{courseId}", model);
            return response.IsSuccessStatusCode;
        }
    }
}

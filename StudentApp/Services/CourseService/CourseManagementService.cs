using Student.Shared.Models.CourseManagement;
using System.Net.Http.Json;

namespace StudentApp.Services.CourseService
{
    public class CourseManagementService : ICourseManagementService
    {
        private readonly HttpClient _httpClient;

        public CourseManagementService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> AddCourseAsync(AddEditCourseModelDTO model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/course", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteLinkBetweenStudentAndCourseAsync(LinkBetweenStudentAndCourse model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/course/delete-link", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RegisterForCourseAsync(LinkBetweenStudentAndCourse model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/courses/delete-link", model);
            return response.IsSuccessStatusCode;
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
            return await _httpClient.GetFromJsonAsync<List<EnrolledCourseModelDTO>>($"api/course/courseslinkedtostudents/{studentId}");
        }

        public async Task<bool> UpdateCourseAsync(Guid courseId, AddEditCourseModelDTO model)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/course/{courseId}", model);
            return response.IsSuccessStatusCode;
        }
    }
}

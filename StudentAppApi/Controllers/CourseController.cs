using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student.Shared.Consts;
using Student.Shared.Models.CourseManagement;
using StudentAppApi.Interfaces.Authentication;
using StudentAppApi.Interfaces.CourseManagement;

namespace StudentAppApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class CourseController : ControllerBase
    {
        private readonly ICourseManagementService _courseManagementService;
        private readonly IUserClaimsService _claims;
        public CourseController(ICourseManagementService courseManagementService, IUserClaimsService claims)
        {
            _courseManagementService = courseManagementService;
            _claims = claims;
        }

        /// <summary>
        /// Get courses
        /// </summary>
        /// <returns>List of all enrolled courses</returns>
        [HttpGet]
        [Route(ApiRoutes.GetAllEnrolledCourses)]
        [ProducesResponseType(typeof(List<EnrolledCourseModelDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<List<EnrolledCourseModelDTO>>> GetAllEnrolledCourses()
        {
            var result = await _courseManagementService.GetEnrolledCoursesAsync();
            return Ok(result);
        }


        /// <summary>
        /// Get courses
        /// </summary>
        /// <returns>List of all  courses</returns>
        [HttpGet]
        [Route(ApiRoutes.GetAllCourses)]
        [ProducesResponseType(typeof(List<CourseModelDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<List<CourseModelDTO>>> GetAllCourses()
        {
            var result = await _courseManagementService.GetCoursesAsync();
            return Ok(result);
        }


        /// <summary>
        /// Retrieves a student courses by their ID
        /// </summary>
        /// <param name="id">The unique identifier of the student</param>
        /// <returns>The user details</returns>
        [HttpGet]
        [Route($"{ApiRoutes.GetCoursesByStudentId}/{{id}}")]
        [ProducesResponseType(typeof(List<EnrolledCourseModelDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<List<EnrolledCourseModelDTO>>> GetCoursesLinkedToStudent(string id)
        {
            var courses = await _courseManagementService.GetCoursesLinkedToStudentAsync(id);
            return Ok(courses);
        }

        /// <summary>
        /// Add m0re courses
        /// </summary>
        /// <param name="model">The courses model</param>
        /// <returns>Status result</returns>
        [HttpPost]
        [Route(ApiRoutes.AddLinkBetweenStudentAndCourse)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<bool>> RegisterStudentForCourse([FromBody] LinkBetweenStudentAndCourse model)
        {
            var result = await _courseManagementService.CreateLinkBetweenStudentAndCourseAsync(model, _claims.GetUserClaims());
            return Ok(result);
        }

        /// <summary>
        /// Deregister a student from course by their ID
        /// </summary>
        /// <param name="id">The unique identifier of the student</param>
        /// <returns>Status result</returns>
        [HttpDelete]
        [Route($"{{courseId}}/{ApiRoutes.DeleteLinkBetweenStudentAndCourse}/{{studentId}}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<bool>> DeleteLinkBetweenStudentAndCourse(Guid courseId, string studentId)
        {
            LinkBetweenStudentAndCourse model = new LinkBetweenStudentAndCourse
            {
                CourseId = courseId,
                StudentId = studentId
            };
            var result = await _courseManagementService.DeleteLinkBetweenStudentAndCourseAsync(model, _claims.GetUserClaims());
            return Ok(result);
        }

    }
}

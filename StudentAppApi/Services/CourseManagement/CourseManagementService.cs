using Student.Shared.DomainModels.CourseManagement;
using Student.Shared.Interfaces.Repositories;
using Student.Shared.Models.Authentication;
using Student.Shared.Models.CourseManagement;
using StudentAppApi.Interfaces.CourseManagement;
using StudentAppApi.Mapper.CourseManagementMapper;

namespace StudentAppApi.Services.CourseManagement
{
    public class CourseManagementService : ICourseManagementService
    {
        #region fields
        private readonly ICourseRepository _courseRepository;
        private readonly IEnrolledCourseRepository _enrolledCourseRepository;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger<CourseManagementService> _logger;
        #endregion

        #region const
        public CourseManagementService(ILoggerFactory loggerFactory, ICourseRepository courseRepository, IEnrolledCourseRepository enrolledCourseRepository)
        {
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger<CourseManagementService>();
            _courseRepository = courseRepository;
            _enrolledCourseRepository = enrolledCourseRepository;
        }
        #endregion

        #region methods
        public async Task<List<CourseModelDTO>> GetCoursesAsync()
        {
            var data = await _courseRepository.GetAllAsync();
            return data.Select(CourseManagementMapper.ConvertCourseToResponseDTO).ToList();
        }

        public async Task<List<EnrolledCourseModelDTO>> GetEnrolledCoursesAsync()
        {
            var data = await _enrolledCourseRepository.GetAllAsync();
            return data.Select(CourseManagementMapper.ConvertEnrolledToResponseDTO).ToList();
        }

        public async Task<List<EnrolledCourseModelDTO>> GetCoursesLinkedToStudentAsync(string id)
        {
            var data = _enrolledCourseRepository.GetBaseQueryable().Where(x=>x.StudentId == id).ToList();
            return data.Select(CourseManagementMapper.ConvertEnrolledToResponseDTO).ToList();
        }

        public async Task<bool> CreateLinkBetweenStudentAndCourseAsync(LinkBetweenStudentAndCourse model, UserClaims? userClaims)
        {
            var courseEntity = await _courseRepository.FindEntityAsync(model.CourseId);
            var enrolledCourse = BuildEnrolledEntity(model, courseEntity, userClaims);
            await _enrolledCourseRepository.Create(enrolledCourse);
            return true;
        }

        public async Task<bool> DeleteLinkBetweenStudentAndCourseAsync(LinkBetweenStudentAndCourse model, UserClaims? userClaims)
        {
            var courseEntity = await _courseRepository.FindEntityAsync(model.CourseId);
            var entity = _enrolledCourseRepository.GetBaseQueryable()
                .FirstOrDefault(x => x.StudentId == model.StudentId && x.CourseCode == courseEntity.CourseCode);
            if (entity != null)
            {
                await _enrolledCourseRepository.Delete(entity);
            }
            return true;
        }

        #region private methods
        public Student.Shared.DomainModels.CourseManagement.Course BuildEntity(AddEditCourseModelDTO model, UserClaims? userClaims)
        {
            var entity = new Student.Shared.DomainModels.CourseManagement.Course();
            entity.CourseCode = model.CourseCode;
            entity.CourseName = model.CourseName;
            entity.Instructor = model.Instructor;
            entity.Credits = model.Credits;
            entity.Schedule = model.Schedule;
            entity.Department = model.Department;
            entity.AvailableSeats = model.AvailableSeats;
            entity.MaxSeats = model.MaxSeats;
            entity.CreatedBy = userClaims?.Username ?? "System";
            entity.CreatedOn = DateTime.UtcNow;
            return entity;
        }
        public void BuildUpdateEntity(AddEditCourseModelDTO model, Student.Shared.DomainModels.CourseManagement.Course entity, UserClaims? userClaims)
        {
            entity.CourseCode = model.CourseCode;
            entity.CourseName = model.CourseName;
            entity.Instructor = model.Instructor;
            entity.Credits = model.Credits;
            entity.Schedule = model.Schedule;
            entity.Department = model.Department;
            entity.AvailableSeats = model.AvailableSeats;
            entity.MaxSeats = model.MaxSeats;
            entity.UpdateModified(userClaims?.Username);
        }
        public Student.Shared.DomainModels.CourseManagement.EnrolledCourse BuildEnrolledEntity(LinkBetweenStudentAndCourse model, Student.Shared.DomainModels.CourseManagement.Course courseEntity, UserClaims? userClaims)
        {
            var enrolledCourse = new Student.Shared.DomainModels.CourseManagement.EnrolledCourse
            {
                CourseCode = courseEntity.CourseCode,
                CourseName = courseEntity.CourseName,
                Credits = courseEntity.Credits,
                Instructor = courseEntity.Instructor,
                Schedule = courseEntity.Schedule,
                StudentId = model.StudentId,
                CourseId = courseEntity.Id,
                CreatedBy = userClaims?.Username ?? "System",
                CreatedOn = DateTime.UtcNow
            };
            return enrolledCourse;
        }
        #endregion
        #endregion



    }
}

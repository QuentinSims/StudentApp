using Microsoft.AspNetCore.Mvc.Formatters;
using Student.Shared.DomainModels.CourseManagement;
using Student.Shared.Interfaces.Repositories;
using Student.Shared.Models.Authentication;
using Student.Shared.Models.CourseManagement;
using Student.Shared.Utilities.Exceptions;
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
            var data = _enrolledCourseRepository.GetBaseQueryable().Where(x => x.StudentId == id).ToList();
            return data.Select(CourseManagementMapper.ConvertEnrolledToResponseDTO).ToList();
        }

        public async Task<EnrolledCourseModelDTO> CreateLinkBetweenStudentAndCourseAsync(LinkBetweenStudentAndCourse model, UserClaims? userClaims)
        {
            var courseEntity = await _courseRepository.FindEntityAsync(model.CourseId);
            CourseValidation(courseEntity);
            await EnrolledValidation(courseEntity, model.StudentId);
            var enrolledCourse = BuildEnrolledEntity(model, courseEntity, userClaims);
            await _enrolledCourseRepository.Create(enrolledCourse);
            courseEntity.AvailableSeats--;
            await _courseRepository.Update(courseEntity);
            return CourseManagementMapper.ConvertEnrolledToResponseDTO(enrolledCourse);
        }

        public async Task<bool> DeleteLinkBetweenStudentAndCourseAsync(LinkBetweenStudentAndCourse model, UserClaims? userClaims)
        {
            var courseEntity = await _courseRepository.FindEntityAsync(model.CourseId);
            var enrolledEntity = await DeEnrollValidation(courseEntity, model.StudentId);
            if (enrolledEntity != null)
            {
                await _enrolledCourseRepository.Delete(enrolledEntity);
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
        public void CourseValidation(Student.Shared.DomainModels.CourseManagement.Course courseEntity)
        {
            if (courseEntity.AvailableSeats <= 0)
            {
                throw new BadRequestModelException("No more seats available in this course.");
            }
        }
        public async Task EnrolledValidation(Student.Shared.DomainModels.CourseManagement.Course courseEntity, string studentId)
        {
            var entityEnrolled = _enrolledCourseRepository.GetBaseQueryable().Where(x => x.CourseId == courseEntity.Id && x.StudentId == studentId).FirstOrDefault();
            if (entityEnrolled is not null)
            {
                throw new BadRequestModelException("Student already enrolled in course");
            }
        }
        public async Task<Student.Shared.DomainModels.CourseManagement.EnrolledCourse> DeEnrollValidation(Student.Shared.DomainModels.CourseManagement.Course courseEntity, string studentId)
        {
            var entityEnrolled = _enrolledCourseRepository.GetBaseQueryable().Where(x => x.CourseId == courseEntity.Id && x.StudentId == studentId).FirstOrDefault();
            if (entityEnrolled is null)
            {
                throw new BadRequestModelException("Student not currently enrolled for course, cannot deregister student from course.");
            }
            return entityEnrolled;
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
                EnrollmentDate = DateTime.UtcNow,
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

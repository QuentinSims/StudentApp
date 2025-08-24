using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Radzen;
using Student.Shared.Models.CourseManagement;
using StudentApp.Services.CourseService;

namespace StudentApp.Pages
{
    public partial class AvailableCoursesComponent
    {
        [Inject] protected DialogService DialogService { get; set; }
        [Inject] protected NotificationService NotificationService { get; set; }
        [Inject] protected ICourseManagementService CourseManagementService { get; set; }
        [Inject] protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        List<CourseModelDTO> availableCourses = new();
        List<EnrolledCourseModelDTO> enrolledCourses = new();
        bool isLoading = true;
        private string? loggedInUserId;

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity != null && user.Identity.IsAuthenticated)
            {
                loggedInUserId = user.FindFirst("nameid")?.Value;
            }
            await LoadAvailableCourses();
            await LoadEnrolledCourseCodes();
            isLoading = false;
        }

        async Task LoadAvailableCourses()
        {
            try
            {
                availableCourses = await CourseManagementService.GetAllCoursesAsync();
            }
            catch (Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = "Error Loading Courses",
                    Detail = "Failed to load available courses. Please try again later."
                });
                availableCourses = new List<CourseModelDTO>();
            }
        }

        async Task LoadEnrolledCourseCodes()
        {
            try
            {
                enrolledCourses = await CourseManagementService.GetCoursesLinkedToStudentAsync(loggedInUserId);
            }
            catch (Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = "Error Loading enrolled Courses",
                    Detail = "Failed to load available enrolled courses. Please try again later."
                });
            }
        }

        bool IsAlreadyEnrolled(CourseModelDTO course)
        {
            return enrolledCourses.Any(x => x.StudentId == loggedInUserId && x.CourseId == course.Id);
        }

        async Task RegisterForCourse(CourseModelDTO course)
        {
            if (course.AvailableSeats == 0)
            {
                NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = "Registration Failed", Detail = "This course is full." });
                return;
            }

            if (IsAlreadyEnrolled(course))
            {
                NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Warning, Summary = "Already Enrolled", Detail = $"You are already enrolled in {course.CourseName}." });
                return;
            }

            var confirmed = await DialogService.Confirm($"Register for {course.CourseName} ({course.CourseCode})?", "Confirm Registration", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });

            if (confirmed == true)
            {
                var result = await CourseManagementService.RegisterForCourseAsync(new LinkBetweenStudentAndCourse() { CourseId = course.Id, StudentId = loggedInUserId });
                course.AvailableSeats--;
                enrolledCourses.Add(result);
                NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Success, Summary = "Registration Successful", Detail = $"Successfully registered for {course.CourseName}" });
                StateHasChanged();
            }
        }

        async Task RefreshCourses()
        {
            isLoading = true;
            await LoadAvailableCourses();
            isLoading = false;
            StateHasChanged();
        }
    }
}

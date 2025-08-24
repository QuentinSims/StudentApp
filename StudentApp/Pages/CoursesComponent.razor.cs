using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Radzen;
using Student.Shared.Models.CourseManagement;
using StudentApp.Services.CourseService;

namespace StudentApp.Pages
{
    public partial class CoursesComponent
    {
        private List<EnrolledCourseModelDTO> enrolledCourses = new();
        [Inject] protected DialogService DialogService { get; set; }
        [Inject] protected NotificationService NotificationService { get; set; }
        [Inject] protected ICourseManagementService CourseManagementService { get; set; }
        [Inject] protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }

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
            await LoadEnrolledCourses();
            bool isLoading = false;
        }

        async Task LoadEnrolledCourses()
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

        private async Task DeregisterFromCourse(EnrolledCourseModelDTO course)
        {
            var confirmed = await DialogService.Confirm(
                $"Are you sure you want to deregister from {course.CourseName}?",
                "Confirm Deregistration",
                new ConfirmOptions
                {
                    OkButtonText = "Yes",
                    CancelButtonText = "No"
                });

            if (confirmed == true)
            {
                var success = await CourseManagementService.DeleteLinkBetweenStudentAndCourseAsync(new LinkBetweenStudentAndCourse() { CourseId = course.CourseId, StudentId = loggedInUserId });

                if (success)
                {
                    enrolledCourses.Remove(course);
                    List<EnrolledCourseModelDTO> remainingCourses = [.. enrolledCourses];
                    enrolledCourses = new();
                    NotificationService.Notify(new NotificationMessage
                    {
                        Severity = NotificationSeverity.Success,
                        Summary = "Success",
                        Detail = $"Successfully deregistered from {course.CourseName}"
                    });
                    enrolledCourses = remainingCourses;
                    StateHasChanged();
                }
                else
                {
                    NotificationService.Notify(new NotificationMessage
                    {
                        Severity = NotificationSeverity.Error,
                        Summary = "Error",
                        Detail = $"Failed to deregister from {course.CourseName}. Please try again later."
                    });
                }
            }
        }
    }
}

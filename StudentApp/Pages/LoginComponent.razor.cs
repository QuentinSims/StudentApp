using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Radzen;
using Student.Shared.Models.Authentication;
using StudentApp.Services.AccountManagementService;

namespace StudentApp.Pages
{
    public partial class LoginComponent
    {
        [Inject] IAuthService AuthService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] NotificationService NotificationService { get; set; }
        [Inject] AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        private LoginRequestDTO _loginModel = new LoginRequestDTO();
        private string errorMessage = "";
        private bool isLoading = false;

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            if (authState.User.Identity?.IsAuthenticated == true)
            {
                var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
                var queryParams = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(uri.Query);
                var returnUrl = queryParams.TryGetValue("returnUrl", out var url) ? url.FirstOrDefault() : "/";

                NavigationManager.NavigateTo(returnUrl ?? "/", replace: true);
            }
        }

        private async Task HandleLogin()
        {
            if (!IsFormValid()) return;

            isLoading = true;
            errorMessage = "";
            StateHasChanged();

            try
            {
                var success = await AuthService.LoginAsync(_loginModel);

                if (success)
                {
                    NotificationService.Notify(new NotificationMessage
                    {
                        Severity = NotificationSeverity.Success,
                        Summary = "Login Successful",
                        Detail = "Welcome back!",
                        Duration = 4000
                    });

                    // Handle return URL after successful login
                    var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
                    var queryParams = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(uri.Query);
                    var returnUrl = queryParams.TryGetValue("returnUrl", out var url) ? url.FirstOrDefault() : "/";

                    NavigationManager.NavigateTo(returnUrl ?? "/", replace: true);
                    _loginModel = new LoginRequestDTO();
                }
                else
                {
                    errorMessage = "Invalid email or password. Please try again.";
                    NotificationService.Notify(new NotificationMessage
                    {
                        Severity = NotificationSeverity.Error,
                        Summary = "Login Failed",
                        Detail = "Invalid email or password. Please try again.",
                        Duration = 4000
                    });
                }
            }
            catch (Exception ex)
            {
                errorMessage = "An error occurred during login. Please try again.";
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = "Login Failed",
                    Detail = "An error occurred during login. Please try again.",
                    Duration = 4000
                });
            }
            finally
            {
                isLoading = false;
                StateHasChanged();
            }
        }

        private bool IsFormValid()
        {
            return !string.IsNullOrWhiteSpace(_loginModel.Email) &&
                   !string.IsNullOrWhiteSpace(_loginModel.Password) &&
                   _loginModel.Email.Contains("@");
        }
    }
}

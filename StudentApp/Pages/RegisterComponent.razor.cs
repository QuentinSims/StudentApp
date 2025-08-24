using Microsoft.AspNetCore.Components;
using Radzen;
using Student.Shared.Models.Authentication;
using StudentApp.Services.AccountManagementService;

namespace StudentApp.Pages
{
    public partial class RegisterComponent
    {
        [Inject] protected NavigationManager NavigationManager { get; set; }
        [Inject] protected NotificationService NotificationService { get; set; }
        [Inject] protected IAccountService AccountService { get; set; }

        private RegisterRequestDTO registerModel = new();
        private string errorMessage = "";
        private string successMessage = "";
        private bool isLoading = false;

        private async Task HandleRegistration()
        {
            isLoading = true;
            errorMessage = "";
            successMessage = "";
            StateHasChanged();

            try
            {
                var result = await AccountService.RegisterAsync(registerModel);

                if (result != null)
                {
                    successMessage = "Registration successful! Please check your email to verify your account.";

                    NotificationService.Notify(new NotificationMessage
                    {
                        Severity = NotificationSeverity.Success,
                        Summary = "Registration Successful",
                        Detail = $"Welcome {result.FullName}! Please verify your email."
                    });

                    NavigationManager.NavigateTo("/login");
                }
            }
            catch (HttpRequestException httpEx)
            {
                if (httpEx.Message.Contains("400"))
                {
                    errorMessage = "Invalid registration data. Please check your information and try again.";
                }
                else if (httpEx.Message.Contains("409"))
                {
                    errorMessage = "An account with this email or username already exists.";
                }
                else
                {
                    errorMessage = "A network error occurred. Please check your connection and try again.";
                }
            }
            catch (Exception ex)
            {
                errorMessage = "An unexpected error occurred during registration. Please try again.";
                Console.WriteLine($"Registration error: {ex.Message}");
            }
            finally
            {
                isLoading = false;
                StateHasChanged();
            }
        }
    }
}

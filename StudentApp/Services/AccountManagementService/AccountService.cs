using Student.Shared.Models.Authentication;
using Student.Shared.Models.UserManagement;
using System.Net.Http.Json;

namespace StudentApp.Services.AccountManagementService
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<AccountService> _logger;

        public AccountService(HttpClient httpClient, ILogger<AccountService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO model)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/account/login", model);
                response.EnsureSuccessStatusCode();

                var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();
                return loginResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login");
                throw;
            }
        }

        public async Task<bool> LogoutAsync()
        {
            try
            {
                var response = await _httpClient.PostAsync("api/auth/logout", null);
                response.EnsureSuccessStatusCode();
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during logout");
                throw;
            }
        }

        public async Task<UserModelDTO> RegisterAsync(RegisterRequestDTO model)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/account/register", model);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<UserModelDTO>();
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during registration");
                throw;
            }
        }
    }
}

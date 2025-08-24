using Microsoft.AspNetCore.Identity;
using Student.Shared.DomainModels.Authentication.SystemUsers;
using Student.Shared.Models.Authentication;
using Student.Shared.Models.UserManagement;
using StudentAppApi.FluentValidation;
using StudentAppApi.Interfaces.Authentication;
using StudentAppApi.Mapper.AuthenicationMapper;
using StudentAppApi.Mapper.UserManagementMapper;

namespace StudentAppApi.Services.Authenication
{
    public class UserService : IUserService
    {
        #region fields
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger<UserService> _logger;
        private readonly IConfiguration _configuration;
        private readonly ITokenRepository _tokenRepository;
        #endregion

        #region const

        public UserService(
           UserManager<ApplicationUser> userManager,
           SignInManager<ApplicationUser> signInManager,
           ILoggerFactory loggerFactory, IConfiguration configuration, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _loggerFactory = loggerFactory;
            _configuration = configuration;
            _logger = _loggerFactory.CreateLogger<UserService>();
            _tokenRepository = tokenRepository;
        }
        #endregion

        #region methods
        #region CRUD operations for user
        public async Task<UserModelDTO> CreateUserAsync(RegisterRequestDTO model, UserClaims cliams, bool requirePasswordReset = false)
        {
            ValidateFieldsEnteredCreatedUser(model);
            var user = UserMapper.ConvertResponseDTOToUser(model);
            user.RequirePasswordReset = requirePasswordReset;
            user.IsActive = true;

            var result = await _userManager.CreateAsync(user, model.Password);

            var createdUser = await _userManager.FindByEmailAsync(model.Email);
            return UserManagementMapper.ConvertResponseDTOToUser(user);
        }
        #endregion

        #region login & logout operations
        public async Task<LoginResponseDTO> SignInUserAsync(LoginRequestDTO model, UserClaims claims)
        {
            ValidateFieldsEnteredLoggedInUser(model);

            ApplicationUser? user = model.IsEmail
                ? await _userManager.FindByEmailAsync(model.Email)
                : await _userManager.FindByNameAsync(model.Email);

            if (user == null || !user.IsActive)
            {
                _logger.LogWarning($"Failed login attempt for {model.Email}: User not found or inactive");
                throw new UnauthorizedAccessException("Invalid credentials or account is inactive");
            }

            var isPasswordLinkedToUser = await _userManager.CheckPasswordAsync(user, model.Password);

            if (isPasswordLinkedToUser)
            {
                try
                {
                    var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, isPersistent: model.RememberMe, lockoutOnFailure: true);

                    if (result.Succeeded)
                    {
                        _logger.LogInformation($"User logged in successfully: {user.Email}");

                        var token = _tokenRepository.CreateToken(user, model.RememberMe);

                        return new LoginResponseDTO()
                        {
                            Username = user.UserName,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Email = user.Email,
                            token = token
                        };
                    }

                    if (result.IsLockedOut)
                    {
                        _logger.LogWarning($"Account locked out: {user.Email}");
                        throw new UnauthorizedAccessException("Account locked. Please try again later.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"An error occurred while signing in user: {user.Email}");
                    throw new InvalidOperationException("An unexpected error occurred during sign-in. Please try again.");
                }
            }

            _logger.LogWarning($"Invalid password for user: {user.Email}");
            throw new UnauthorizedAccessException("Invalid login attempt");
        }
        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        #endregion


        #region private methods
        private void ValidateFieldsEnteredCreatedUser(RegisterRequestDTO model)
        {
            var validator = new RegisterUserValidation();
            var results = validator.Validate(model);

            if (!results.IsValid)
            {
                var errors = results.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ArgumentException(string.Join(", ", errors));
            }
        }
        private void ValidateFieldsEnteredLoggedInUser(LoginRequestDTO model)
        {
            var validator = new LoginUserValidation();
            var results = validator.Validate(model);

            if (!results.IsValid)
            {
                var errors = results.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ArgumentException(string.Join(", ", errors));
            }
        }
        #endregion
        #endregion
    }
}

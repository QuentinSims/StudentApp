using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student.Shared.Consts;
using Student.Shared.Models.Authentication;
using Student.Shared.Models.UserManagement;
using StudentAppApi.Interfaces.Authentication;

namespace StudentAppApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserClaimsService _claims;

        public AccountController(IUserService userService, IUserClaimsService claims)
        {
            _userService = userService;
            _claims = claims;
        }

        #region endpoints
        /// <summary>
        /// User login
        /// </summary>
        /// <param name="model">Login credentials</param>
        /// <returns>Authentication result</returns>
        [HttpPost(ApiRoutes.Login)]
        [AllowAnonymous]
        [ProducesResponseType(typeof(LoginResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LoginResponseDTO>> Login([FromBody] LoginRequestDTO model)
        {
            var result = await _userService.SignInUserAsync(model, _claims.GetUserClaims());
            return Ok(result);
        }



        /// <summary>
        /// User logout
        /// </summary>
        /// <returns>Logout result</returns>
        [HttpPost(ApiRoutes.Logout)]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Logout()
        {

            await _userService.SignOutAsync();
            return Ok();
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="model">Registration details</param>
        /// <returns>Created user details or error</returns>
        [HttpPost(ApiRoutes.Register)]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UserModelDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> Register([FromBody] RegisterRequestDTO model)
        {
            var result = await _userService.CreateUserAsync(model, _claims.GetUserClaims());
            return Ok(result);
        }
    }
    #endregion
}

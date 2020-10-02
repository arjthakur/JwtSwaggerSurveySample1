using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Survey.DTOs.Request;
using Survey.DTOs.Response;
using Survey.Services;
using System.Threading.Tasks;

namespace Survey.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("Authorize")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly IUserService userService;
        /// <summary>
        /// Authentication end points
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="userService"></param>
        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            this.userService = userService;
        }
        /// <summary>
        /// User can login and get access token via this end point
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Post(Login login)
        {
            var user = await userService.Login(login).ConfigureAwait(false);
            if (user == null)
            {
                _logger.LogInformation($"User [{login.Username}] failed to logged in the system.");
                return Unauthorized();
            }
            _logger.LogInformation($"User [{login.Username}] logged in the system.");
            return Ok(user);
        }
    }
}
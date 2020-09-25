using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Survey.DTOs.Response;
using Survey.Services;

namespace Survey.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IUserService userService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IUserService userService)
        {
            _logger = logger;
            this.userService = userService;
        }

        /// <summary>
        /// Only authorised user can access this end point
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(List<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Get()
        {
            return Ok(await userService.GetUsers());
        }
        /// <summary>
        /// Only authorised user can access this end point with Role "Admin"
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAdmin")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(List<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAdmin()
        {
            return Ok(await userService.GetUsers());
        }
        /// <summary>
        /// Only authorised user can access this end point with Role "SuperAdmin"
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetSuperAdmin")]
        [Authorize(Roles = "SuperAdmin")]
        [ProducesResponseType(typeof(List<UserDto>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetSuperAdmin()
        {
            return Ok(await userService.GetUsers());
        }
        /// <summary>
        /// Anonymous call
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAnonymous")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<WeatherForecast>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Anonymous()
        {
            var rng = new Random();
            return Ok(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray());
        }
        /// <summary>
        /// Anonymous call
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<WeatherForecast>), StatusCodes.Status200OK)]
        [HttpGet("GetAnonymousPages")]
        public async Task<IActionResult> Anonymous2()
        {
            return Ok(userService.GetPages());
        }
    }
}

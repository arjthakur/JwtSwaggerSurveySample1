using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Survey.DTOs.Request;
using Survey.DTOs.Response;
using Survey.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Survey.Api.Controllers
{
    /// <summary>
    /// Survey related end points collection
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    [EnableCors("AllCorsPolicy")]
    public class SurveyController : BaseController
    {
        private readonly ILogger<SurveyController> logger;
        private readonly ISurveyService surveyService;

        /// <summary>
        /// ctor
        /// </summary>
        public SurveyController(ILogger<SurveyController> logger, ISurveyService SurveyService)
        {
            this.logger = logger;
            surveyService = SurveyService;
        }
        /// <summary>
        /// Get All Surveys [Only authorised user can access this end point]
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(List<SurveyDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Get(CancellationToken token)
        {
            return Ok(await surveyService.GetAsync(token));
        }
        /// <summary>
        /// Get survey by id [Only authorised user can access this end point]
        /// </summary>
        /// <returns></returns>
        [HttpGet("{Id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(SurveyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Get(long Id, CancellationToken token)
        {
            return Ok(await surveyService.GetAsync(Id, token));
        }

        /// <summary>
        /// Add survey [Only authorised user can access this end point]
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        //[Authorize]
        [ProducesResponseType(typeof(SurveyDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Add(NewSurvey model, CancellationToken token)
        {
            var UserId = long.Parse(User.Claims.First().Value);
            var surveyDto = await surveyService.AddAsync(model, UserId, token);
            return Created("/Survey/" + surveyDto.Id, surveyDto);
        }
        /// <summary>
        /// Update survey [Only authorised user can access this end point]
        /// </summary>
        /// <returns></returns>
        [HttpPut()]
        [Authorize]
        [ProducesResponseType(typeof(SurveyDto), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update(SurveyDto model, CancellationToken token)
        {
            var UserId = long.Parse(User.Claims.First().Value);
            return Accepted(await surveyService.UpdateAsync(model, UserId, token));
        }
        /// <summary>
        /// Delete survey [Only authorised user can access this end point]
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        [Authorize]
        [ProducesResponseType(typeof(SurveyDto), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Delete(long Id, CancellationToken token)
        {
            var UserId = long.Parse(User.Claims.First().Value);
            await surveyService.DeleteAsync(Id, UserId, token);
            return NoContent();
        }
    }
}
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Survey.DTOs.Request;
using Survey.DTOs.Response;
using Survey.Services;
using System.Collections.Generic;
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
        //[Authorize]
        [ProducesResponseType(typeof(List<SurveyDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Get()
        {
            return Ok(await surveyService.GetAsync());
        }
        /// <summary>
        /// Get survey by id [Only authorised user can access this end point]
        /// </summary>
        /// <returns></returns>
        [HttpGet("{Id}")]
        //[Authorize]
        [ProducesResponseType(typeof(SurveyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Get(long Id)
        {
            return Ok(await surveyService.GetAsync(Id));
        }

        /// <summary>
        /// Add survey [Only authorised user can access this end point]
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        //[Authorize]
        [ProducesResponseType(typeof(SurveyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Add(NewSurvey model)
        {
            var UserId = 10001;//long.Parse(User.Claims.First().Value);
            return Ok(await surveyService.AddAsync(model, UserId));
        }
        /// <summary>
        /// Update survey [Only authorised user can access this end point]
        /// </summary>
        /// <returns></returns>
        [HttpPut()]
        //[Authorize]
        [ProducesResponseType(typeof(SurveyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update(SurveyDto model)
        {
            var UserId = 10001;//long.Parse(User.Claims.First().Value);
            return Ok(await surveyService.UpdateAsync(model, UserId));
        }
        /// <summary>
        /// Delete survey [Only authorised user can access this end point]
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        //[Authorize]
        [ProducesResponseType(typeof(SurveyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Delete(long Id)
        {
            var UserId = 10001;//long.Parse(User.Claims.First().Value);
            return Ok(await surveyService.DeleteAsync(Id, UserId));
        }
    }
}
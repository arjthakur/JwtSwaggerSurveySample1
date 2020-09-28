using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Survey.Api.Controllers;
using Survey.DTOs.Response;
using Survey.Services;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xunit;
using System.Threading;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Survey.DTOs.Request;

namespace Survey.API.Tests
{
    public class SurveyController_Tests
    {
        private readonly Mock<ISurveyService> _surveyService;
        private readonly SurveyController _surveyControler;
        private readonly CancellationTokenSource cts;
        private readonly ILogger<SurveyController> _logger;

        public SurveyController_Tests()
        {
            _surveyService = new Mock<ISurveyService>();
            _logger = new Mock<ILogger<SurveyController>>().Object;
            _surveyControler = new SurveyController(_logger, _surveyService.Object);
            cts = new CancellationTokenSource();
            GlobalSetup();
        }

        private void GlobalSetup()
        {
            _surveyService.Setup(x => x.GetAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(GenerateListOFSurvey(100));
            _surveyService.Setup(x => x.GetAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(GenerateListOFSurvey(100).FirstOrDefault(x => x.Id == 10));

            //Local setup for Admin temporary in Global
            SetUserRole(GetUser());
        }

        private UserDto GetUser()
        {
            return new UserDto
            {
                FirstName = "Arjun",
                Id = 10001,
                LastName = "Thakur",
                Roles = new List<string> { "SuperAdmin" }
            };
        }

        private void SetUserRole(UserDto appUser)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, appUser.Id.ToString()),
                new Claim(ClaimTypes.Name, appUser.FirstName+" "+appUser.LastName),
            };
            appUser.Roles.ForEach(x => claims.Add(new Claim(ClaimTypes.Role, x)));
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "mock"));
            _surveyControler.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
        }
        private List<SurveyDto> GenerateListOFSurvey(int Count)
        {
            var lst = new List<SurveyDto>();
            for (long i = 1; i <= Count; i++)
                lst.Add(GetItem(i));
            return lst;
        }

        private SurveyDto GetItem(long i)
        {
            return new SurveyDto { Id = i, Description = "DESC" + i, Title = "Title" + i };
        }

        [Fact]
        public async void Get_AllSurvey()
        {
            //arrage
            //act
            var result = await _surveyControler.Get(cts.Token).ConfigureAwait(false);
            // assert
            Assert.IsType<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<List<SurveyDto>>(okResult.Value);
            var survey = (List<SurveyDto>)okResult.Value;
            Assert.NotNull(survey);
            //asert
        }

        [Fact()]
        public async void Get_SurveyById()
        {
            //arrage
            long Id = 10;
            var survey = GetItem(Id);
            //act
            var result = await _surveyControler.Get(Id, cts.Token).ConfigureAwait(false);
            //asert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            var surveyById = (SurveyDto)okResult.Value;
            Assert.Equal(survey.Title, surveyById.Title);
            Assert.Equal(survey.Description, surveyById.Description);
            Assert.Equal(survey.Id, surveyById.Id);
        }

        [Fact()]
        public async void Add_Survey()
        {
            //arrage
            var survey = new NewSurvey { Description = "New", Title = "New" };

            _surveyService.Setup(x => x.AddAsync(It.IsAny<NewSurvey>(), It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SurveyDto { Id=10, Title = survey.Title, Description = survey.Description });

            //act
            var result = await _surveyControler.Add(survey, cts.Token).ConfigureAwait(false);
            //asert
            Assert.IsType<CreatedResult>(result);
            var statusResult = result as CreatedResult;
            Assert.NotNull(statusResult);
            Assert.Equal(201, statusResult.StatusCode);
            var surveyById = (SurveyDto)statusResult.Value;
            Assert.Equal(survey.Title, surveyById.Title);
            Assert.Equal(survey.Description, surveyById.Description);
        }

        //[Theory]
        //[InlineData(25,5,15,3,2)]
        //[InlineData(20,20)]
        //[InlineData(250,200,10,20,20)]
        //[InlineData(30,3,-2,-1,5,25)
        //public void Get_Survey(int expected,params int[] v)
        //{
        //    Assert.Equal(expected, v.Sum());
        //}
    }
}

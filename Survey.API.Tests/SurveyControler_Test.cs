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

namespace Survey.API.Tests
{
    public class SurveyControler_Test
    {
        private readonly Mock<ISurveyService> _surveyService;
        private readonly SurveyController _surveyControler;
        private readonly ILogger<SurveyController> _logger;

        public SurveyControler_Test()
        {
            _surveyService = new Mock<ISurveyService>();
            _logger = new Mock<ILogger<SurveyController>>().Object;
            _surveyControler = new SurveyController(_logger, _surveyService.Object);

            Setup();
        }

        private void Setup()
        {
            _surveyService.Setup(x => x.GetAsync())
                .ReturnsAsync(GenerateListOFSurvey(100));
            _surveyService.Setup(x => x.GetAsync(It.IsAny<long>()))
                .ReturnsAsync(GenerateListOFSurvey(100).FirstOrDefault(x=>x.Id == 10));
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
            var result = await _surveyControler.Get().ConfigureAwait(false);

            //asert
            _ = Assert.IsType<OkObjectResult>(result);
        }

        [Fact()]
        public async void Get_SurveyById()
        {
            //arrage
            long Id = 9;
            var surveyById = GetItem(Id);
            //act
            var result = await _surveyControler.Get().ConfigureAwait(false);
            var survey = await _surveyService.Object.GetAsync(Id);
            //asert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(survey.Title, surveyById.Title);
            Assert.Equal(survey.Description, surveyById.Description);
            Assert.Equal(survey.Id, surveyById.Id);
        }
    }
}

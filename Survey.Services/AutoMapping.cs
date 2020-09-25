using AutoMapper;
using Survey.DTOs.Request;
using Survey.DTOs.Response;
using Survey.Entities;
using System.Collections.Generic;

namespace Survey.Services
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<UserDto, ApplicationUser>();

            CreateMap<PageDto, Entities.Tables.Page>();
            CreateMap<Entities.Tables.Page, PageDto>();

            CreateMap<SurveyDto, Entities.Tables.Survey>();
            CreateMap<Entities.Tables.Survey, SurveyDto>();
            CreateMap<NewSurvey, Entities.Tables.Survey>();
        }
    }
}

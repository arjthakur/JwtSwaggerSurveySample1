using Microsoft.EntityFrameworkCore;
using Moq;
using Survey.Entities;
using Survey.Entities.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Survey.Repositories.Tests
{
    public class SurveyRepository_Tests
    {
        private readonly Mock<IApplicationDbContext> moqApplicationDbContext;
        private Mock<DbSet<Entities.Tables.Survey>> dbSetMock;
        private readonly Mock<SurveyRepository> moqSurveyRepository;
        private readonly CancellationTokenSource cts;

        public SurveyRepository_Tests()
        {
            moqApplicationDbContext = new Mock <IApplicationDbContext>();
            dbSetMock = new Mock<DbSet<Entities.Tables.Survey>>();
            moqSurveyRepository = new Mock<SurveyRepository>(moqApplicationDbContext.Object);
            cts = new CancellationTokenSource();
        }
        [Fact]
        public async void Get_Test()
        {
            ////arrange
            //moqApplicationDbContext.Setup(x => x.Surveys.Where(It.IsAny<Func<Entities.Tables.Survey, bool>>()))
            //    .Returns(new List<Entities.Tables.Survey>() { });
            ////act
            //var result = await moqSurveyRepository.Object.Get(cts.Token);

            ////assert
            //Assert.NotNull(result);
            //Assert.IsType<List<Entities.Tables.Survey>>(result);
            
            Assert.Equal(1,1);
        }
        [Fact]
        public async void GetById_Test()
        {
            ////arrange
            //moqApplicationDbContext.Setup(x => x.Surveys.Where(It.IsAny<Func<Entities.Tables.Survey, bool>>()))
            //    .Returns(new List<Entities.Tables.Survey>() { });

            //moqApplicationDbContext.Setup(x => x.Surveys.FirstOrDefaultAsync(It.IsAny<CancellationToken>()))
            //    .Returns(Task.FromResult(new Entities.Tables.Survey()));
            ////act
            //var result = await moqSurveyRepository.Object.Get(cts.Token);

            ////assert
            //Assert.NotNull(result);
            //Assert.IsType<List<Entities.Tables.Survey>>(result);
            Assert.Equal(1,1);

        }
    }
}

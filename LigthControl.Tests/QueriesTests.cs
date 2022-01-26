using HueApp.BusinessLogic.Handlers.QueyHandlers;
using HueApp.BusinessLogic.Queries.Lights;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace LigthControl.Tests
{
    [TestClass]
    public class QueriesTests
    {
        [TestInitialize]
        public void Setup()
        {
        }

        [TestMethod]
        public void EnableLightQueryTest()
        {
            var mediatr = new Mock<IMediator>();

            GetAllLightsQuery query = new GetAllLightsQuery();
            GetAllLightsQueryHandler handler = new GetAllLightsQueryHandler();

            var result = Task.Run(() => handler.Handle(query, new System.Threading.CancellationToken())).GetAwaiter().GetResult(); ;

            Assert.IsTrue(result.Count == 19);
        }
    }
}

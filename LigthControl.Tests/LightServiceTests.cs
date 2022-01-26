using HueCore.Services.Abstract;
using HueCore.Services.Configs;
using LightControl.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace LigthControl.Tests
{
    [TestClass]
    public class LightServiceTests
    {

        private HueLightService _hueLightService { get; set; }

        private IConfiguration configuration { get; set; }

        private string _lightId { get; set; }

        [TestInitialize]
        public void Setup()
        {
            string outputPath = Path.GetDirectoryName(Assembly.GetAssembly(typeof(HueSettingsConfig)).Location);
            var abstractLogger = new Mock<ILogger<HueHttpClient>>();
            var lightLogger = new Mock<ILogger<HueLightService>>();

            configuration = HueSettingsConfig.GetJsonConfiguration(outputPath);
            _lightId = configuration.GetSection("HueSettings")["LightInHome"].ToString();

            _hueLightService = new HueLightService(configuration, abstractLogger.Object, lightLogger.Object);
        }

        [TestMethod]
        public void GetAllLights_ReturnsSomething()
        {
            var lightResponse = Task.Run(() => _hueLightService.GetLights()).GetAwaiter().GetResult();

            Assert.IsTrue(lightResponse.LightDataCollection.Count > 1);
        }

        [TestMethod]
        public void GetLightByID_ReturnsSomething()
        {
            var lightResponse = Task.Run(() => _hueLightService.GetLight(_lightId)).GetAwaiter().GetResult();

            Assert.IsTrue(lightResponse.LightDataCollection.Count == 1);
        }

        [TestMethod]
        public void TurnOnLight()
        {
            _lightId = configuration.GetSection("HueSettings")["WorkLightId"].ToString();

            var lightResponse = Task.Run(() => _hueLightService.TurnLightOn(_lightId)).GetAwaiter().GetResult();

            Assert.IsTrue(lightResponse.LightStateChangeData.Count == 1);
            Assert.IsTrue(lightResponse.LightStateChangeData.First().rid == _lightId);
        }

        [TestMethod]
        public void TurnOffLight()
        {
            _lightId = configuration.GetSection("HueSettings")["WorkLightId"].ToString();

            var lightResponse = Task.Run(() => _hueLightService.TurnLightOff(_lightId)).GetAwaiter().GetResult();

            Assert.IsTrue(lightResponse.LightStateChangeData.Count == 1);
            Assert.IsTrue(lightResponse.LightStateChangeData.First().rid == _lightId);
        }
    }
}
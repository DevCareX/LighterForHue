using HueCore.Services.Abstract;
using HueCore.Services.Configs;
using LightControl.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace LigthControl.Tests
{
    [TestClass]
    public class LightServiceTests
    {

        private HueLightService _hueLightService { get; set; }

        private string _lightId { get; set; }

        [TestInitialize]
        public void Setup()
        {
            string outputPath = Path.GetDirectoryName(Assembly.GetAssembly(typeof(HueSettingsConfig)).Location);
            var abstractLogger = new Mock<ILogger<HueServiceAbstract>>();
            var lightLogger = new Mock<ILogger<HueLightService>>();

            var config = HueSettingsConfig.GetJsonConfiguration(outputPath);
            _lightId = config.GetSection("HueSettings")["LightInHome"].ToString();

            _hueLightService = new HueLightService(config, abstractLogger.Object, lightLogger.Object);
        }

        [TestMethod]
        public void GetAllLights_ReturnsSomething()
        {
            var lightResponse = Task.Run(() => _hueLightService.GetLights()).GetAwaiter().GetResult();

            Assert.IsTrue(lightResponse.Data.Length > 1);
        }

        [TestMethod]
        public void GetLightByID_ReturnsSomething()
        {
            var lightResponse = Task.Run(() => _hueLightService.GetLight(_lightId)).GetAwaiter().GetResult();

            Assert.IsTrue(lightResponse.Data.Length == 1);
        }
    }
}
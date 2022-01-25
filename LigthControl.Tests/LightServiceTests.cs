using LightControl.UI.Services;
using LightControl.UI.Utils;
using Microsoft.Extensions.Configuration;
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
        private HueSettingsConfig _hueSettings { get; set; }

        private HueLightService _hueLightService { get; set; }

        [TestInitialize]
        public void Setup()
        {
            string outputPath = Path.GetDirectoryName(Assembly.GetAssembly(typeof(HueSettingsConfig)).Location);
            _hueSettings = HueSettingsConfig.GetHueConfiguration(outputPath);

            var abstractLogger = new Mock<ILogger<HueServiceAbstract>>();
            var lightLogger = new Mock<ILogger<HueLightService>>();

            var config = HueSettingsConfig.GetJsonConfiguration(outputPath);

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
            var lightResponse = Task.Run(() => _hueLightService.GetLight("8e054a31-0d01-4f43-bdea-cb1fc05fc0ae")).GetAwaiter().GetResult();

            Assert.IsTrue(lightResponse.Data.Length == 1);
        }
    }
}
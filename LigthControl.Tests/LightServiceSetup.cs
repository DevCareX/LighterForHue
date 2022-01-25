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
    public class LightServiceSetup
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
        public void SmokeTestThis()
        {
            var lightResponse = Task.Run(() =>_hueLightService.GetLights()).GetAwaiter().GetResult();

            Assert.IsTrue(lightResponse.Length > 0);
        }
    }
}
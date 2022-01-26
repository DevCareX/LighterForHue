using HueCore.Services.Abstract;
using HueCore.Services.Configs;
using LightControl.Services;
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

        private string _lightId { get; set; }

        private IConfiguration _configuration;

        [TestInitialize]
        public void Setup()
        {            
            string outputPath = Path.GetDirectoryName(Assembly.GetAssembly(typeof(HueSettingsConfig)).Location);
            
            _configuration = new ConfigurationBuilder()
               .SetBasePath(outputPath)
               .AddJsonFile("appsettings.json", optional: false)
               .Build();

            var abstractLogger = new Mock<ILogger<HueHttpClient>>();
            var lightLogger = new Mock<ILogger<HueLightService>>();
            
            _lightId = _configuration.GetSection("HueSettings")["LightInHome"].ToString();

            _hueLightService = new HueLightService();
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
            _lightId = _configuration.GetSection("HueSettings")["WorkLightId"].ToString();

            var lightResponse = Task.Run(() => _hueLightService.TurnLightOn(_lightId)).GetAwaiter().GetResult();

            Assert.IsTrue(lightResponse.LightStateChangeData.Count == 1);
            Assert.IsTrue(lightResponse.LightStateChangeData.First().rid == _lightId);
        }

        [TestMethod]
        public void TurnOffLight()
        {
            _lightId = _configuration.GetSection("HueSettings")["WorkLightId"].ToString();

            var lightResponse = Task.Run(() => _hueLightService.TurnLightOff(_lightId)).GetAwaiter().GetResult();

            Assert.IsTrue(lightResponse.LightStateChangeData.Count == 1);
            Assert.IsTrue(lightResponse.LightStateChangeData.First().rid == _lightId);
        }
    }
}
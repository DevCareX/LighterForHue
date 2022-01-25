using LightControl.UI.Models;
using Newtonsoft.Json;

namespace LightControl.UI.Services
{
    public class HueLightService : HueServiceAbstract, IHueLightService
    {
        private string ApiServicePrefix { get; set; }

        private ILogger<HueLightService> _lightLogger { get; set; }

        public HueLightService(IConfiguration configuration, ILogger<HueServiceAbstract> logger, ILogger<HueLightService> lightLogger) : base(configuration, logger)
        {
            ApiServicePrefix = "/resource/light";
            _lightLogger = lightLogger;
        }

        public async Task<LightResponse> GetLights()
        {
            try
            {
                var response = await DoGetCall(ApiServicePrefix, null);
                _lightLogger.LogInformation(response);

                return JsonConvert.DeserializeObject<LightResponse>(response);
            }
            catch (Exception ex)
            {
                _lightLogger.LogError("ERROR: " + ex.Message);
                throw;
            }
        }

        public async Task<LightResponse> GetLight(string id)
        {
            try
            {
                var response = await DoGetCall(string.Format("{0}/{1}", ApiServicePrefix, id), null);

                _lightLogger.LogInformation(response);

                return JsonConvert.DeserializeObject<LightResponse>(response);
            }
            catch (Exception ex)
            {
                _lightLogger.LogError("ERROR: " + ex.Message);
                throw;
            }
        }
    }
}

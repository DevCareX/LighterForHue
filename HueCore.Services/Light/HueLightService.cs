using HueCore.Domain.Responses;
using HueCore.Services.Abstract;
using HueCoreModels.Requests;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LightControl.UI.Services
{
    public class HueLightService : HueHttpClient, IHueLightService
    {
        private string ApiServicePrefix { get; set; }

        private ILogger<HueLightService> _lightLogger { get; set; }

        public HueLightService(IConfiguration configuration, ILogger<HueHttpClient> logger, ILogger<HueLightService> lightLogger) : base(configuration, logger)
        {
            ApiServicePrefix = "/resource/light";
            _lightLogger = lightLogger;
        }

        public async Task<LightResponse> GetLights()
        {
            try
            {
                var response = await MakeRequest<LightResponse>("get", ApiServicePrefix);
                _lightLogger.LogInformation("OK");

                return response;
            }
            catch (Exception ex)
            {
                _lightLogger.LogError("ERROR: " + ex.Message);
                throw;
            }
        }

        public async Task<LightResponse> GetLight(string lightId)
        {
            try
            {
                var response = await MakeRequest<LightResponse>("get", string.Format("{0}/{1}", ApiServicePrefix, lightId));
                _lightLogger.LogInformation("OK");

                return response;
            }
            catch (Exception ex)
            {
                _lightLogger.LogError("ERROR: " + ex.Message);
                throw;
            }
        }

        public async Task<LightResponse> TurnLightOn(string lightId)
        {
            try
            {
                var response = await MakeRequest<LightResponse>(
                    "put",
                    string.Format("{0}/{1}", ApiServicePrefix, lightId),
                     new LightOnRequest());

                _lightLogger.LogInformation("OK");

                return response;
            }
            catch (Exception ex)
            {
                _lightLogger.LogError("ERROR: " + ex.Message);
                throw;
            }
        }

        public async Task<LightResponse> TurnLightOff(string lightId)
        {
            try
            {
                var response = await MakeRequest<LightResponse>(
                    "put",
                    string.Format("{0}/{1}", ApiServicePrefix, lightId),
                    new LightOffRequest());

                _lightLogger.LogInformation("OK");

                return response;
            }
            catch (Exception ex)
            {
                _lightLogger.LogError("ERROR: " + ex.Message);
                throw;
            }
        }
    }
}

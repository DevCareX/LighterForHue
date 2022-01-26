using HueApp.BusinessLogic.Lights.Requests;
using HueApp.BusinessLogic.Lights.Responses;
using HueCore.Services.Abstract;
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

        public async Task<GetLightResponse> GetLights()
        {
            try
            {
                var response = await MakeRequest<GetLightResponse>("get", ApiServicePrefix);
                _lightLogger.LogInformation("OK");

                return response;
            }
            catch (Exception ex)
            {
                _lightLogger.LogError("ERROR: " + ex.Message);
                throw;
            }
        }

        public async Task<GetLightResponse> GetLight(string lightId)
        {
            try
            {
                var response = await MakeRequest<GetLightResponse>("get", string.Format("{0}/{1}", ApiServicePrefix, lightId));
                _lightLogger.LogInformation("OK");

                return response;
            }
            catch (Exception ex)
            {
                _lightLogger.LogError("ERROR: " + ex.Message);
                throw;
            }
        }

        public async Task<EnableLightResponse> TurnLightOn(string lightId)
        {
            try
            {
                var response = await MakeRequest<EnableLightResponse>(
                    "put",
                    string.Format("{0}/{1}", ApiServicePrefix, lightId),
                     new EnableLightRequest());

                _lightLogger.LogInformation("OK");

                return response;
            }
            catch (Exception ex)
            {
                _lightLogger.LogError("ERROR: " + ex.Message);
                throw;
            }
        }

        public async Task<DisableLightResponse> TurnLightOff(string lightId)
        {
            try
            {
                var response = await MakeRequest<DisableLightResponse>(
                    "put",
                    string.Format("{0}/{1}", ApiServicePrefix, lightId),
                    new DisableLightRequest());

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

using HueCore.Services.Abstract;
using HueCore.Services.Light.API.Requests;
using HueCore.Services.Light.API.Responses;
using Microsoft.Extensions.Logging;

namespace LightControl.Services
{
    public class HueLightService : HueHttpClient, IHueLightService
    {
        private string ApiServicePrefix { get; set; }

        public HueLightService() : base()
        {
            ApiServicePrefix = "/resource/light";
        }

        public async Task<GetLightResponse> GetLights()
        {
            try
            {
                var response = await MakeRequest<GetLightResponse>("get", ApiServicePrefix);

                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<GetLightResponse> GetLight(string lightId)
        {
            try
            {
                var response = await MakeRequest<GetLightResponse>("get", string.Format("{0}/{1}", ApiServicePrefix, lightId));

                return response;
            }
            catch (Exception ex)
            {
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


                return response;
            }
            catch (Exception ex)
            {
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

                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

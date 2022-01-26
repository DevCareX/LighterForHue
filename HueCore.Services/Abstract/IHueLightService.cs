using HueCore.Domain.Responses;

namespace HueCore.Services.Abstract
{
    public interface IHueLightService
    {
        Task<LightResponse> GetLights();

        Task<LightResponse> GetLight(string stringId);

        Task<LightResponse> TurnLightOn(string lightId);

        Task<LightResponse> TurnLightOff(string lightId);
    }
}
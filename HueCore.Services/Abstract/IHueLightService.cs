using HueApp.BusinessLogic.Lights.Responses;

namespace HueCore.Services.Abstract
{
    public interface IHueLightService
    {
        Task<GetLightResponse> GetLights();

        Task<GetLightResponse> GetLight(string stringId);

        Task<EnableLightResponse> TurnLightOn(string lightId);

        Task<DisableLightResponse> TurnLightOff(string lightId);
    }
}
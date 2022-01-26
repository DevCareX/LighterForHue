using HueCoreModels.Responses;

namespace HueCore.Services.Abstract
{
    public interface IHueLightService
    {
        Task<GetLightResponse> GetLights();

        Task<GetLightResponse> GetLight(string stringId);

        Task<LightOnResponse> TurnLightOn(string lightId);

        Task<LightOffResponse> TurnLightOff(string lightId);
    }
}
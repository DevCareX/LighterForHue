using LightControl.UI.Models;

namespace LightControl.UI.Services
{
    public interface IHueLightService
    {
        Task<LightResponse> GetLights();

        Task<LightResponse> GetLight(string id);
    }
}
using LightControl.UI.Utils;

namespace LightControl.UI.Services
{
    public interface IHueLightService
    {
        Task<string> GetLights();
    }
}
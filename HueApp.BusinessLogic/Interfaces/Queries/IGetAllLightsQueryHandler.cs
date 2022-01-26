using HueApp.BusinessLogic.Models.ResponseModels;

namespace HueApp.BusinessLogic.Interfaces.Queries
{
    public interface IGetAllLightsQueryHandler
    {
        Task<GetAllLightsResponseModel> GetAllLights();
    }
}
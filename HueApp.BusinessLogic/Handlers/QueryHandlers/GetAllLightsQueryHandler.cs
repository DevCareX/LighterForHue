using HueApp.BusinessLogic.Models.ResponseModels;
using HueApp.BusinessLogic.Queries.Lights;
using LightControl.Services;
using MediatR;

namespace HueApp.BusinessLogic.Handlers.QueyHandlers
{
    public class GetAllLightsQueryHandler : IRequestHandler<GetAllLightsQuery, GetAllLightsResponseModel>
    {
        public async Task<GetAllLightsResponseModel> Handle(GetAllLightsQuery request, CancellationToken cancellationToken)
        {
            var lightService = new HueLightService();

            var allLightsResponse = await lightService.GetLights();

            return new GetAllLightsResponseModel() { Count = allLightsResponse.LightDataCollection.Count };
        }
    }
}

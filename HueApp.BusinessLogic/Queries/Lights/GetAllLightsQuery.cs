using HueApp.BusinessLogic.Models.ResponseModels;
using MediatR;

namespace HueApp.BusinessLogic.Queries.Lights
{
    public class GetAllLightsQuery : IRequest<GetAllLightsResponseModel>
    {
    }
}

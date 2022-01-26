using HueCore.HueAPIObjects;

namespace HueCore.Services.Light.API.Requests
{
    public class DisableLightRequest
    {
        public On On { get; set; }

        public DisableLightRequest()
        {
            On = new On()
            {
                on = false
            };
        }
    }
}

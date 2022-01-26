using HueCore.HueAPIObjects;

namespace HueCore.Services.Light.API.Requests
{
    public class EnableLightRequest
    {
        public On On { get; set; }

        public EnableLightRequest()
        {
            On = new On()
            {
                on = true
            };
        }
    }
}

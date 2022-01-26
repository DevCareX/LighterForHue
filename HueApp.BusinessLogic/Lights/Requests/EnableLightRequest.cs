using HueCore.HueAPIObjects;

namespace HueApp.BusinessLogic.Lights.Requests
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

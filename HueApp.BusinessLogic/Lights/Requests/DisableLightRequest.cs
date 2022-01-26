using HueCore.HueAPIObjects;

namespace HueApp.BusinessLogic.Lights.Requests
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

using HueCore.Domain;

namespace HueCoreModels.Requests
{
    public class LightOffRequest
    {
        public On On { get; set; }

        public LightOffRequest()
        {
            On = new On()
            {
                on = false
            };
        }
    }
}

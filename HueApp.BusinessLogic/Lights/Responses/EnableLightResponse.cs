using HueCore.HueAPIObjects;
using Newtonsoft.Json;

namespace HueApp.BusinessLogic.Lights.Responses
{
    public class EnableLightResponse
    {
        [JsonProperty("data")]
        public List<LightStateChangeData> LightStateChangeData { get; set; }

        [JsonProperty("errors")]
        public List<object> Errors { get; set; }
    }
}

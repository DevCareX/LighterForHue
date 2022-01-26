using HueCore.HueAPIObjects;
using Newtonsoft.Json;

namespace HueCore.Services.Light.API.Responses
{
    public class DisableLightResponse
    {
        [JsonProperty("data")]
        public List<LightStateChangeData> LightStateChangeData { get; set; }

        [JsonProperty("errors")]
        public List<object> errors { get; set; }
    }
}

using HueCoreModels.DomainModels;
using Newtonsoft.Json;

namespace HueCoreModels.Responses
{
    public class LightOffResponse
    {
        [JsonProperty("data")]
        public List<LightStateChangeData> LightStateChangeData { get; set; }

        [JsonProperty("errors")]
        public List<object> errors { get; set; }
    }
}

using HueCoreModels.DomainModels;
using Newtonsoft.Json;

namespace HueCoreModels.Responses
{
    public class LightOnResponse
    {
        [JsonProperty("data")]
        public List<LightStateChangeData> LightStateChangeData { get; set; }

        [JsonProperty("errors")]
        public List<object> Errors { get; set; }
    }
}

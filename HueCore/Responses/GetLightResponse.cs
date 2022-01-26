using HueCore.Domain;
using Newtonsoft.Json;

namespace HueCoreModels.Responses
{

    public class GetLightResponse
    {
        [JsonProperty("errors")]
        public List<object> Errors { get; set; }

        [JsonProperty("data")]
        public LightData[] LightDataCollection { get; set; }
    }
}

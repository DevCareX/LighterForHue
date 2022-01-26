using Newtonsoft.Json;

namespace HueCore.Domain.Responses
{

    public class LightResponse
    {
        [JsonProperty("errors")]
        public List<object> Errors { get; set; }

        [JsonProperty("data")]
        public Data[] Data { get; set; }
    }   
}

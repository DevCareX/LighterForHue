using HueCore.Domain;
using Newtonsoft.Json;

namespace LightControl.UI.Models
{

    public class LightResponse
    {
        [JsonProperty("errors")]
        public List<object> Errors { get; set; }

        [JsonProperty("data")]
        public Data[] Data { get; set; }
    }   
}

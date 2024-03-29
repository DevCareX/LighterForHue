﻿using HueCore.HueAPIObjects;
using Newtonsoft.Json;

namespace HueCore.Services.Light.API.Responses
{
    public class GetLightResponse
    {
        [JsonProperty("errors")]
        public List<object> Errors { get; set; }

        [JsonProperty("data")]
        public List<LightData> LightDataCollection { get; set; }
    }
}

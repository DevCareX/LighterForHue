﻿using HueCore.Domain;

namespace HueCoreModels.Requests
{
    public class LightOnRequest
    {
        public On On { get; set; }

        public LightOnRequest()
        {
            On = new On()
            {
                on = true
            };
        }
    }
}

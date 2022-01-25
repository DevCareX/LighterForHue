namespace HueCore.Domain
{
    public class Data
    {
        public Alert alert { get; set; }
        public Color color { get; set; }
        public ColorTemperature color_temperature { get; set; }
        public Dimming dimming { get; set; }
        public Dynamics dynamics { get; set; }
        public Effects effects { get; set; }
        public string id { get; set; }
        public string id_v1 { get; set; }
        public Metadata metadata { get; set; }
        public string mode { get; set; }
        public On on { get; set; }
        public Owner owner { get; set; }
        public string type { get; set; }
    }
}


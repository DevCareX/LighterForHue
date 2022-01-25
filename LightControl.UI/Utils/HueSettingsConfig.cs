namespace LightControl.UI.Utils
{
    public class HueSettingsConfig
    {
        private IConfigurationSection hueSettingsSection;

        public HueSettingsConfig(IConfigurationSection hueSettings)
        {
            hueSettingsSection = hueSettings;

            BridgeIP = hueSettingsSection["BridgheIP"];
            DebugToolAddress = hueSettingsSection["DebugToolAddress"];
            HueAPIAddress = hueSettingsSection["HueAPIAddress"];
            HueRegisterKey = hueSettingsSection["HueRegisteredKey"];
        }

        public string BridgeIP { get; set; }
        public string DebugToolAddress { get; set; }
        public string HueAPIAddress { get; set; }
        public string HueRegisterKey { get; set; }
    }
}



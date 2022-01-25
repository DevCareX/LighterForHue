namespace LightControl.UI.Utils
{
    public class HueSettingsConfig
    {
        private IConfigurationSection hueSettingsSection;

        public HueSettingsConfig()
        {

        }

        public HueSettingsConfig(IConfigurationSection hueSettings)
        {
            hueSettingsSection = hueSettings;

            BridgeIP = hueSettingsSection["BridgheIP"];
            DebugToolAddress = hueSettingsSection["DebugToolAddress"];
            HueAPIAddress = hueSettingsSection["HueAPIAddress"];
            HueRegisterKey = hueSettingsSection["HueRegisteredKey"];
        }
        public static HueSettingsConfig GetHueConfiguration(string outputPath)
        {
            var configuration = new HueSettingsConfig();

            var iConfig = GetJsonConfiguration(outputPath);

            iConfig
                .GetSection("HueSettings")
                .Bind(configuration);

            return configuration;
        }

        public static IConfiguration GetJsonConfiguration(string outputPath)
        {
            return new ConfigurationBuilder()
               .SetBasePath(outputPath)
               .AddJsonFile("appsettings.json", optional: false)
               .Build();
        }

        public string BridgeIP { get; set; }
        public string DebugToolAddress { get; set; }
        public string HueAPIAddress { get; set; }
        public string HueRegisterKey { get; set; }
    }
}



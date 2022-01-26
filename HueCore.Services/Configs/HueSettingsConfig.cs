using Microsoft.Extensions.Configuration;

namespace HueCore.Services.Configs
{
    public class HueSettingsConfig
    {
        private IConfigurationSection hueSettingsSection;
        public string BridgeIP { get; set; }
        public string DebugToolAddress { get; set; }
        public string HueAPIAddress { get; set; }
        public string HueRegisterKey { get; set; }

        public HueSettingsConfig()
        {
            BridgeIP = String.Empty;
            DebugToolAddress = String.Empty; 
            HueAPIAddress = String.Empty;
            HueRegisterKey = String.Empty;
        }

        public HueSettingsConfig(IConfiguration configuration)
        {
            var hueSection = configuration.GetSection("HueSettings");
            BridgeIP = hueSection["BridgheIP"];
            DebugToolAddress = hueSection["DebugToolAddress"];
            HueAPIAddress = hueSection["HueAPIAddress"];
            HueRegisterKey = hueSection["HueRegisteredKey"];
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
    }
}



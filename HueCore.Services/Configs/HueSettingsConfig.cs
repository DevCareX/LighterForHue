using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace HueCore.Services.Configs
{
    public class HueSettingsConfig
    {
        public string BridgeIP { get; set; }
        public string DebugToolAddress { get; set; }
        public string HueAPIAddress { get; set; }
        public string HueRegisteredKey { get; set; }

        public HueSettingsConfig()
        {
            var config = GetJsonConfiguration(Path.GetDirectoryName(Assembly.GetAssembly(typeof(HueSettingsConfig)).Location));
            config
                .GetSection("HueSettings")
                .Bind(this);
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



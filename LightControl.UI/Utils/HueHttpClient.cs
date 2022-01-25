using Microsoft.AspNetCore.Components;
using System.Text;

namespace LightControl.UI.Utils
{
    public class HueHttpClient
    {

        private IConfigurationSection _configurationSection;
        private HueSettingsConfig _hueSettings;

        public HueHttpClient(IConfigurationSection configurationSection)
        {
            _configurationSection = configurationSection;
            _hueSettings = new HueSettingsConfig(configurationSection);
        }

        public HttpClient GetHueAPIClient()
        {
            var apiBaseAddress = _hueSettings.HueAPIAddress;
            var key = _hueSettings.HueRegisterKey;

            var baseAddress = string.Format("{0}{1}{2}", apiBaseAddress, key, "/");

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);

            return httpClient;
        }

        public async Task<string> APITest()
        {
            var httpClient = GetHueAPIClient();
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };

            httpClient = new HttpClient(httpClientHandler)
            {
                BaseAddress = new Uri(String.Format("{0}{1}", httpClient.BaseAddress, "lights"))
            };

            var response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, httpClient.BaseAddress));

            Stream receiveStream = response.Content.ReadAsStream();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            return  readStream.ReadToEnd();
        }
    }
}

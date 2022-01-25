using Microsoft.AspNetCore.Components;

namespace Lighter.UI.Utils
{
    public class HueHttpClient
    {

        private HueSettingsConfig _configuration;

        public HueHttpClient(HueSettingsConfig configuration)
        {
            _configuration = configuration;
        }

        public HttpClient GetHueAPIClient()
        {
            var apiBaseAddress = _configuration.HueAPIAddress;
            var key = _configuration.HueRegisterKey;

            var baseAddress = string.Format("{0}{1}{2}", apiBaseAddress, key, "/");

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);

            return httpClient;
        }

        public async Task<HttpResponseMessage> APITest()
        {
            var httpClient = GetHueAPIClient();
            httpClient.BaseAddress = new Uri(String.Format("{0}{1}", httpClient.BaseAddress, "lights"));

            var response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, httpClient.BaseAddress));

            return response;
        }
    }
}

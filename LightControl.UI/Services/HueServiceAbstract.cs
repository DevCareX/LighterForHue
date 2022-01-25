using LightControl.UI.Utils;

namespace LightControl.UI.Services
{
    public abstract class HueServiceAbstract
    {
        private IConfigurationSection ConfigurationSection { get; set; }
        private ILogger<HueServiceAbstract> _logger;
        private HueSettingsConfig HueSettings { get; set; }

        protected HttpClient HueAPIHttpClient { get; set; }

        public HueServiceAbstract(IConfiguration configuration, ILogger<HueServiceAbstract> logger)
        {
            ConfigurationSection = configuration.GetSection("HueSettings");
            HueSettings = new HueSettingsConfig(ConfigurationSection);
            _logger = logger;

            HueAPIHttpClient = ConfigureHttpClient();
        }

        public async Task<string> DoGetCall(string apiPrefix, string requestBody)
        {
            try
            {
                string apiUrl = string.Format("{0}{1}", HueAPIHttpClient.BaseAddress.ToString(), apiPrefix);

                var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
                request.Headers.Add("hue-application-key", HueSettings.HueRegisterKey);

                if (requestBody != null && requestBody.Length > 0)
                {
                    // toDo
                }

                var lightsResponse = await HueAPIHttpClient.SendAsync(request);
                lightsResponse.EnsureSuccessStatusCode();

                string responseBody = await lightsResponse.Content.ReadAsStringAsync();
                _logger.LogInformation(responseBody);

                return lightsResponse.Content.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return "ERROR: " + ex.Message;
            }
        }

        private HttpClient ConfigureHttpClient()
        {
            try
            {
                var httpClientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) =>
                    {
                        return true;
                    }
                };

                return new HttpClient(httpClientHandler)
                {
                    BaseAddress = new Uri(HueSettings.HueAPIAddress)
                };
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}

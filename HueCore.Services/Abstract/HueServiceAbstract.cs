using HueCore.Services.Configs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Json;

namespace HueCore.Services.Abstract
{
    public abstract class HueServiceAbstract
    {
        private static IConfigurationSection ConfigurationSection { get; set; }
        private static IConfiguration Configuration { get; set; }
        private HttpClientHandler _httpClientHandler { get; set; }
        protected HttpClient HueAPIHttpClient { get; set; }

        private ILogger<HueServiceAbstract> _logger;
        private static HueSettingsConfig HueSettings { get; set; }

        public HueServiceAbstract(IConfiguration configuration, ILogger<HueServiceAbstract> logger)
        {
            Configuration = configuration;
            ConfigurationSection = Configuration.GetSection("HueSettings");
            HueSettings = new HueSettingsConfig(ConfigurationSection);
            _logger = logger;

            _httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) =>
                {
                    return true;
                }
            };

            HueAPIHttpClient = ConfigureHttpClient();
        }

        protected async Task<T> MakeRequest<T>(string httpMethod, string route, Dictionary<string, string> postParams = null)
        {
            using (var client = new HttpClient(_httpClientHandler))
            {
                HttpRequestMessage requestMessage = new HttpRequestMessage(new HttpMethod(httpMethod), $"{HueSettings.HueAPIAddress}/{route}");
                requestMessage.Headers.Add("hue-application-key", HueSettings.HueRegisterKey);

                if (postParams != null)
                    requestMessage.Content = new FormUrlEncodedContent(postParams);   // This is where your content gets added to the request body


                HttpResponseMessage response = await client.SendAsync(requestMessage);
                response.EnsureSuccessStatusCode();

                string apiResponse = await response.Content.ReadAsStringAsync();

                try
                {
                    _logger.LogInformation(apiResponse);
                    // Attempt to deserialise the reponse to the desired type, otherwise throw an expetion with the response from the api.
                    if (apiResponse != "")
                        return JsonConvert.DeserializeObject<T>(apiResponse);
                    else
                        throw new Exception();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new Exception($"An error ocurred while calling the API. It responded with the following message: {response.StatusCode} {response.ReasonPhrase}");
                }
            }
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

                return IsValidJson(responseBody) ? responseBody : String.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return "ERROR: " + ex.Message;
            }
        }

        public async Task<string> DoPutCall(string apiPrefix, object requestBody)
        {
            try
            {
                string apiUrl = string.Format("{0}{1}", HueAPIHttpClient.BaseAddress.ToString(), apiPrefix);

                var request = new HttpRequestMessage(HttpMethod.Put, apiUrl);
                request.Headers.Add("hue-application-key", HueSettings.HueRegisterKey);

                if (requestBody != null)
                {
                    // toDo
                    request.Content = JsonContent.Create(requestBody);
                }

                var lightsResponse = await HueAPIHttpClient.SendAsync(request);
                lightsResponse.EnsureSuccessStatusCode();

                string responseBody = await lightsResponse.Content.ReadAsStringAsync();

                _logger.LogInformation(responseBody);

                return IsValidJson(responseBody) ? responseBody : String.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return "ERROR: " + ex.Message;
            }
        }

        private bool IsValidJson(string stringValue)
        {
            if (string.IsNullOrWhiteSpace(stringValue))
            {
                return false;
            }

            var value = stringValue.Trim();

            if ((value.StartsWith("{") && value.EndsWith("}")) || //For object
                (value.StartsWith("[") && value.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(value);
                    return true;
                }
                catch (JsonReaderException)
                {
                    return false;
                }
            }

            return false;
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

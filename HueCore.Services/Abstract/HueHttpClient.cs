using HueCore.Services.Configs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace HueCore.Services.Abstract
{
    public abstract class HueHttpClient
    {
        private static HueSettingsConfig HueSettings { get; set; }

        private ILogger<HueHttpClient> _logger;

        public HueHttpClient(IConfiguration configuration, ILogger<HueHttpClient> logger)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            if (logger == null)
                throw new ArgumentException(nameof(logger));

            HueSettings = new HueSettingsConfig(configuration);

            _logger = logger;
        }

        protected async Task<T> MakeRequest<T>(string httpMethod, string route, Dictionary<string, string> postParams = null)
        {
            using (var client = new HttpClient(new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) =>
                {
                    return true;
                }
            }))
            {
                HttpRequestMessage requestMessage = new HttpRequestMessage(new HttpMethod(httpMethod), $"{HueSettings.HueAPIAddress}/{route}");
                requestMessage.Headers.Add("hue-application-key", HueSettings.HueRegisterKey);

                if (postParams != null)
                {
                    requestMessage.Content = JsonContent.Create(postParams);
                }

                HttpResponseMessage response = await client.SendAsync(requestMessage);
                response.EnsureSuccessStatusCode();

                string apiResponse = await response.Content.ReadAsStringAsync();

                try
                {
                    _logger.LogInformation(apiResponse);

                    // Attempt to deserialise the reponse to the desired type, otherwise throw an expetion with the response from the api.
                    if (!string.IsNullOrEmpty(apiResponse))
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

        protected async Task<T> MakeRequest<T>(string httpMethod, string route, object postObject)
        {
            using (var client = new HttpClient(new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) =>
                {
                    return true;
                }
            }))
            {
                HttpRequestMessage requestMessage = new HttpRequestMessage(new HttpMethod(httpMethod), $"{HueSettings.HueAPIAddress}/{route}");
                requestMessage.Headers.Add("hue-application-key", HueSettings.HueRegisterKey);

                if (postObject != null)
                {                                    requestMessage.Content = JsonContent.Create(postObject);
                    requestMessage.Headers.Add("media-type", "application/json");
                }

                HttpResponseMessage response = await client.SendAsync(requestMessage);
                response.EnsureSuccessStatusCode();

                string apiResponse = await response.Content.ReadAsStringAsync();

                try
                {
                    _logger.LogInformation(apiResponse);

                    // Attempt to deserialise the reponse to the desired type, otherwise throw an expetion with the response from the api.
                    if (!string.IsNullOrEmpty(apiResponse))
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
    }
}

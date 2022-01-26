using HueCore.Services.Configs;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace HueCore.Services.Abstract
{
    public abstract class HueHttpClient
    {
        private static HueSettingsConfig HueSettings { get; set; }

        public HueHttpClient()
        {
            HueSettings = new HueSettingsConfig();
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
                requestMessage.Headers.Add("hue-application-key", HueSettings.HueRegisteredKey);

                if (postParams != null)
                {
                    requestMessage.Content = JsonContent.Create(postParams);
                }

                HttpResponseMessage response = await client.SendAsync(requestMessage);
                response.EnsureSuccessStatusCode();

                string apiResponse = await response.Content.ReadAsStringAsync();

                try
                {
                    // Attempt to deserialise the reponse to the desired type, otherwise throw an expetion with the response from the api.
                    if (!string.IsNullOrEmpty(apiResponse))
                        return JsonConvert.DeserializeObject<T>(apiResponse);
                    else
                        throw new Exception();
                }
                catch (Exception ex)
                {
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
                requestMessage.Headers.Add("hue-application-key", HueSettings.HueRegisteredKey);

                if (postObject != null)
                {
                    requestMessage.Content = JsonContent.Create(postObject);
                    requestMessage.Headers.Add("media-type", "application/json");
                }

                HttpResponseMessage response = await client.SendAsync(requestMessage);
                response.EnsureSuccessStatusCode();

                string apiResponse = await response.Content.ReadAsStringAsync();

                try
                {
                    // Attempt to deserialise the reponse to the desired type, otherwise throw an expetion with the response from the api.
                    if (!string.IsNullOrEmpty(apiResponse))
                        return JsonConvert.DeserializeObject<T>(apiResponse);
                    else
                        throw new Exception();
                }
                catch (Exception ex)
                {
                    throw new Exception($"An error ocurred while calling the API. It responded with the following message: {response.StatusCode} {response.ReasonPhrase}");
                }
            }
        }
    }
}

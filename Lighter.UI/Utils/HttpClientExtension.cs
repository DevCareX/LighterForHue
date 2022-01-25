namespace Lighter.UI.Utils
{
    public static class HttpClientExtension
    {
        public static HttpClient GetHueAPIClient(this HttpClient httpClient)
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(@"https://HUEBridgeLocalIP/api/");

            return httpClient;
        }
    }
}

namespace LightControl.UI.Services
{
    public class HueLightService : HueServiceAbstract, IHueLightService
    {
        private string ApiServicePrefix { get; set; }

        private ILogger<HueLightService> _lightLogger { get; set; }

        public HueLightService(IConfiguration configuration, ILogger<HueServiceAbstract> logger, ILogger<HueLightService> lightLogger) : base(configuration, logger)
        {
            ApiServicePrefix = "/lights";
            _lightLogger = lightLogger;
        }

        public async Task<string> GetLights()
        {
            try
            {
               var response = await DoGetCall(ApiServicePrefix, null);
                _lightLogger.LogInformation(response);

                return response;
            }
            catch (Exception ex)
            {
                _lightLogger.LogError("ERROR: " + ex.Message);
                throw;
            }
        }
    }
}

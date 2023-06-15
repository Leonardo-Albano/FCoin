using FCoin.Business.Interfaces;
using Newtonsoft.Json;
using RestSharp;

namespace FCoin.Business
{
    public class HourManagement : IHourManagement
    {
        private readonly RestClient _restClient;
        private readonly IConfiguration _configuration;

        public HourManagement(IConfiguration configuration)
        {
            _configuration = configuration;
            string ipConnection = _configuration["IpConnection"];
            _restClient = new RestClient($"{ipConnection}/hora");
        }

        public async Task<DateTime?> GetHour()
        {
            try
            {
                RestRequest request = new RestRequest("", Method.Get);
                RestResponse response = await _restClient.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    DateTime hour = JsonConvert.DeserializeObject<DateTime>(response.Content);
                    return hour;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

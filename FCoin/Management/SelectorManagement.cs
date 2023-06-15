using FCoin.Business.Interfaces;
using FCoin.Models;
using Newtonsoft.Json;
using RestSharp;

namespace FCoin.Business
{
    public class SelectorManagement : ISelectorManagement
    {
        private readonly RestClient _restClient;
        private readonly IConfiguration _configuration;

        public SelectorManagement(IConfiguration configuration)
        {
            _configuration = configuration;
            string ipConnection = _configuration["IpConnection"];
            _restClient = new RestClient($"{ipConnection}/seletor");
        }

        public async Task<dynamic> GetSelector(int? id)
        {
            try
            {
                RestRequest request;

                if (id.HasValue)
                {
                    request = new RestRequest($"/{id}", Method.Get);
                }
                else
                {
                    request = new RestRequest();
                }

                RestResponse response = await _restClient.ExecuteAsync(request);

                dynamic selector;
                if (response.IsSuccessful)
                {
                    if (id.HasValue)
                    {
                        selector = JsonConvert.DeserializeObject<Selector>(response.Content);
                    }
                    else
                    {
                        selector = JsonConvert.DeserializeObject<List<Selector>>(response.Content);
                    }

                    return selector;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Selector> CreateSelector(Selector selector)
        {
            try
            {
                RestRequest request = new RestRequest($"/{selector.Nome}/{selector.Ip}", Method.Post);
                RestResponse response = await _restClient.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    Selector newSelector = JsonConvert.DeserializeObject<Selector>(response.Content);
                    return newSelector;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<dynamic> UpdateSelector(Selector selector)
        {
            try
            {
                RestRequest request = new($"/{selector.Id}/{selector.Nome}/{selector.Ip}", Method.Post);
                RestResponse response = await _restClient.ExecuteAsync(request);

                Dictionary<dynamic, dynamic> responseObject = JsonConvert.DeserializeObject<Dictionary<dynamic, dynamic>>(response.Content);
                if (response.IsSuccessful)
                {
                    return responseObject.Count > 1 ? JsonConvert.DeserializeObject<Selector>(response.Content) : responseObject;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteSelector(int id)
        {
            try
            {
                RestRequest request = new($"/{id}", Method.Delete);
                RestResponse response = await _restClient.ExecuteAsync(request);

                Dictionary<dynamic, dynamic> responseObject = JsonConvert.DeserializeObject<Dictionary<dynamic, dynamic>>(response.Content);
                if (response.IsSuccessful)
                {
                    if (responseObject.ContainsValue("Validador Deletado com Sucesso"))
                    {
                        return true;
                    }

                }

                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}

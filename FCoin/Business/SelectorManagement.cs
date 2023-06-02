using FCoin.Business.Interfaces;
using FCoin.Models;
using Newtonsoft.Json;
using RestSharp;

namespace FCoin.Business
{
    public class SelectorManagement : ISelectorManagement
    {
        private readonly RestClient _restClient = new RestClient("http://192.168.15.22:5000/seletor");

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
    }
}

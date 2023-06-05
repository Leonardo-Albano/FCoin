using FCoin.Business.Interfaces;
using FCoin.Models;
using Newtonsoft.Json;
using RestSharp;

namespace FCoin.Business
{
    public class ClientManagement : IClientManagement
    {
        //private readonly RestClient _restClient = new("http://192.168.15.22:5000/cliente");
        private readonly RestClient _restClient;
        private readonly IConfiguration _configuration;

        public ClientManagement(IConfiguration configuration)
        {
            _configuration = configuration;
            string ipConnection = _configuration["IpConnection"];
            _restClient = new RestClient($"{ipConnection}/cliente");
        }
        public async Task<dynamic> GetClient(int? id)
        {
            try
            {
                dynamic client;
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

                if (response.IsSuccessful)
                {
                    if (id.HasValue)
                    {
                        client = JsonConvert.DeserializeObject<Client>(response.Content);
                    }
                    else
                    {
                        client = JsonConvert.DeserializeObject<List<Client>>(response.Content);
                    }

                    return client;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    
        public async Task<Client> CreateClient(Client client)
        {
            try
            {
                RestRequest request = new($"/{client.Nome}/{client.Senha}/{client.QtdMoeda}", Method.Post);
                RestResponse response = await _restClient.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    client = JsonConvert.DeserializeObject<Client>(response.Content);
                    return client;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<dynamic> UpdateClient(int id, int qtdMoeda)
        {
            try
            {
                RestRequest request = new($"/{id}/{qtdMoeda}", Method.Post);
                RestResponse response = await _restClient.ExecuteAsync(request);

                Dictionary<dynamic, dynamic> responseObject = JsonConvert.DeserializeObject<Dictionary<dynamic, dynamic>>(response.Content);
                if (response.IsSuccessful)
                {
                    return responseObject.Count > 1 ? JsonConvert.DeserializeObject<Client>(response.Content) : responseObject;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteClient(int id)
        {
            try
            {
                RestRequest request = new($"/{id}", Method.Delete);
                RestResponse response = await _restClient.ExecuteAsync(request);

                Dictionary<dynamic, dynamic> responseObject = JsonConvert.DeserializeObject<Dictionary<dynamic, dynamic>>(response.Content);
                if (response.IsSuccessful)
                {
                    if (responseObject.ContainsValue("Cliente Deletado com Sucesso"))
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

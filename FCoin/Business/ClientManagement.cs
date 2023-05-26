using FCoin.Business.Interfaces;
using FCoin.Models;
using Newtonsoft.Json;
using RestSharp;

namespace FCoin.Business
{
    public class ClientManagement : IClientManagement
    {
        private readonly RestClient _restClient = new("http://10.130.48.37:5000/cliente");
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

                if (response.IsSuccessful)
                {
                    var responseObject = JsonConvert.DeserializeObject(response.Content);

                    //if (responseObject is Client)
                    //{
                    //    return JsonConvert.DeserializeObject<Client>(response.Content);
                    //}
                    //else
                    //{
                    return responseObject;
                    //}
                }


            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

using FCoin.Business.Interfaces;
using FCoin.Models;
using FCoin.Repositories;
using Newtonsoft.Json;
using RestSharp;

namespace FCoin.Business
{
    public class ClientManagement : IClientManagement
    {
        //private readonly RestClient _restClient = new("http://192.168.15.22:5000/cliente");
        private readonly RestClient _restClient;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public ClientManagement(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            string ipConnection = _configuration["IpConnection"];
            _restClient = new RestClient($"{ipConnection}/cliente");
            _unitOfWork = unitOfWork;
        }
        public async Task<dynamic> GetClient(int? id)
        {
            try
            {
                //dynamic client;
                //RestRequest request;

                //if (id.HasValue)
                //{
                    //request = new RestRequest($"/{id}", Method.Get);
                //}
                //else
                //{
                    //request = new RestRequest();
                //}


                //RestResponse response = await _restClient.ExecuteAsync(request);

                //if (response.IsSuccessful)
                //{
                //    if (id.HasValue)
                //    {
                //        client = JsonConvert.DeserializeObject<Client>(response.Content);
                //    }
                //    else
                //    {
                //        client = JsonConvert.DeserializeObject<List<Client>>(response.Content);
                //    }

                //    return client;
                //}

                //return null;

                if (id.HasValue)
                {
                    return await _unitOfWork.Client.GetByIdAsync(id.Value);
                }
                else
                {
                    return await _unitOfWork.Client.GetAllAsync();
                }
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
                //RestRequest request = new($"/{client.Nome}/{client.Senha}/{client.QtdMoeda}", Method.Post);
                //RestResponse response = await _restClient.ExecuteAsync(request);

                //if (response.IsSuccessful)
                //{
                //    _unitOfWork.Client.Add(client);
                //    await _unitOfWork.CommitAsync();
                //    client = JsonConvert.DeserializeObject<Client>(response.Content);

                //    return client;
                //}

                //return null;

                _unitOfWork.Client.Add(client);
                await _unitOfWork.CommitAsync();

                return client;

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
                //RestRequest request = new($"/{id}/{qtdMoeda}", Method.Post);
                //RestResponse response = await _restClient.ExecuteAsync(request);

                //Dictionary<dynamic, dynamic> responseObject = JsonConvert.DeserializeObject<Dictionary<dynamic, dynamic>>(response.Content);
                //if (response.IsSuccessful)
                //{
                //    if (responseObject.Count > 1)
                //    {
                //        Client client = JsonConvert.DeserializeObject<Client>(response.Content);
                //        return client;
                //    }
                //    else
                //    {
                //        return responseObject;
                //    }
                //}

                //return null;

                Client client = await _unitOfWork.Client.GetByIdAsync(id);
                client.QtdMoeda = qtdMoeda;

                _unitOfWork.Client.Update(client);
                await _unitOfWork.CommitAsync();

                return client;
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
                //RestRequest request = new($"/{id}", Method.Delete);
                //RestResponse response = await _restClient.ExecuteAsync(request);

                //Dictionary<dynamic, dynamic> responseObject = JsonConvert.DeserializeObject<Dictionary<dynamic, dynamic>>(response.Content);
                //if (response.IsSuccessful)
                //{

                    //if (responseObject.ContainsValue("Cliente Deletado com Sucesso"))
                //    {
                //        return true;
                //    }

                //}

                //return false;

                Client client = await _unitOfWork.Client.GetByIdAsync(id);
                _unitOfWork.Client.Remove(client);
                return await _unitOfWork.CommitAsync() > 0;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}

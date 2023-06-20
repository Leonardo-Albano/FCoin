using FCoin.Business.Interfaces;
using FCoin.Models;
using FCoin.Repositories;
using Newtonsoft.Json;
using RestSharp;
using System.Runtime.CompilerServices;

namespace FCoin.Business
{
    public class SelectorManagement : ISelectorManagement
    {
        private readonly RestClient _restClient;
        private readonly IConfiguration _configuration;
        private readonly IClientManagement _clientManagement;
        private readonly IValidatorManagement _validatorManagement;
        private readonly IUnitOfWork _unitOfWork;

        public SelectorManagement(IConfiguration configuration, IClientManagement clientManagement, IValidatorManagement validatorManagement, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            string ipConnection = _configuration["IpConnection"];
            _restClient = new RestClient($"{ipConnection}/seletor");
            _clientManagement = clientManagement;
            _validatorManagement = validatorManagement;
            _unitOfWork = unitOfWork;
        }

        public async Task<dynamic> GetSelector(int? id)
        {
            try
            {
                //RestRequest request;


                //if (id.HasValue)
                //{
                //    //request = new RestRequest($"/{id}", Method.Get);
                //}
                //else
                //{
                //    //request = new RestRequest();
                //}

                //RestResponse response = await _restClient.ExecuteAsync(request);

                //dynamic selector;
                //if (response.IsSuccessful)
                //{
                //    if (id.HasValue)
                //    {
                //        selector = JsonConvert.DeserializeObject<Selector>(response.Content);
                //    }
                //    else
                //    {
                //        selector = JsonConvert.DeserializeObject<List<Selector>>(response.Content);
                //    }

                //    return selector;
                //}

                //return null;

                if (id.HasValue)
                {
                    return await _unitOfWork.Selector.GetByIdAsync(id.Value);
                }
                else
                {
                    return await _unitOfWork.Selector.GetAllAsync();
                }
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
                //RestRequest request = new RestRequest($"/{selector.Nome}/{selector.Ip}", Method.Post);
                //RestResponse response = await _restClient.ExecuteAsync(request);

                //if (response.IsSuccessful)
                //{
                //    Selector newSelector = JsonConvert.DeserializeObject<Selector>(response.Content);
                //    return newSelector;
                //}

                //return null;

                selector.Id = 0;
                _unitOfWork.Selector.Add(selector);
                await _unitOfWork.CommitAsync();

                return selector;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<dynamic> UpdateSelector(int id, Selector selector)
        {
            try
            {
                //RestRequest request = new($"/{selector.Id}/{selector.Nome}/{selector.Ip}", Method.Post);
                //RestResponse response = await _restClient.ExecuteAsync(request);

                //Dictionary<dynamic, dynamic> responseObject = JsonConvert.DeserializeObject<Dictionary<dynamic, dynamic>>(response.Content);
                //if (response.IsSuccessful)
                //{
                //    return responseObject.Count > 1 ? JsonConvert.DeserializeObject<Selector>(response.Content) : responseObject;
                //}

                //return null;

                Selector updatedSelector = await _unitOfWork.Selector.GetByIdAsync(id);
                updatedSelector.Nome = selector.Nome;
                updatedSelector.Ip = selector.Ip;

                _unitOfWork.Selector.Update(updatedSelector);
                await _unitOfWork.CommitAsync();

                return updatedSelector;
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
                //RestRequest request = new($"/{id}", Method.Delete);
                //RestResponse response = await _restClient.ExecuteAsync(request);

                //Dictionary<dynamic, dynamic> responseObject = JsonConvert.DeserializeObject<Dictionary<dynamic, dynamic>>(response.Content);
                //if (response.IsSuccessful)
                //{
                //    if (responseObject.ContainsValue("Validador Deletado com Sucesso"))
                //    {
                //        return true;
                //    }

                //}
                //return false;

                Selector selector = await _unitOfWork.Selector.GetByIdAsync(id);
                _unitOfWork.Selector.Remove(selector);
                return await _unitOfWork.CommitAsync() > 0;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public async Task<List<int>> SelectValidators(int id, int transactionId)
        {
            List<int> selectedValidators = new List<int>();

            try
            {
                int totalOffers = await _unitOfWork.Validator.OffersBySelector(id);
                List<Validator> validators = await _unitOfWork.Validator.ValidatorsBySelectorId(id);

                Dictionary<int, int> values = new Dictionary<int, int>();
                foreach (Validator validator in validators)
                {
                    int percentual = (validator.Offer / totalOffers) * 100;
                    if (percentual < 5)
                    {
                        percentual = 5;
                    }
                    else if (percentual > 40)
                    {
                        percentual = 40;
                    }
                    values.Add(validator.Id, percentual);
                }

                var sortedValidators = values.OrderByDescending(pair => pair.Value).Take(3);

                selectedValidators = sortedValidators.Select(pair => pair.Key).ToList();
                
                foreach (int validatorId in selectedValidators)
                {
                    _unitOfWork.TransactionLink.Add(new()
                    {
                        TransactionId = transactionId,
                        ValidatorId = validatorId
                    });

                }
                await _unitOfWork.CommitAsync();

                return selectedValidators;
            }
            catch (Exception)
            {
                return new();
            }
        }

    }
}

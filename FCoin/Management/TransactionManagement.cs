using FCoin.Business.Interfaces;
using FCoin.Models;
using FCoin.Repositories;
using Newtonsoft.Json;
using RestSharp;
using System.Runtime.CompilerServices;

namespace FCoin.Business
{
    public class TransactionManagement : ITransactionManagement
    {
        private readonly RestClient _restClient;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionManagement(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            string ipConnection = _configuration["IpConnection"];
            _restClient = new RestClient($"{ipConnection}/transacoes");
            _unitOfWork = unitOfWork;
        }

        public async Task<dynamic> GetTransaction(int? id)
        {
            try
            {
                //RestRequest request;

                //if (id.HasValue)
                //{
                //    request = new RestRequest($"/{id}", Method.Get);
                //}
                //else
                //{
                //    request = new RestRequest();
                //}

                //RestResponse response = await _restClient.ExecuteAsync(request);

                //dynamic transaction;
                //if (response.IsSuccessful)
                //{
                //    if (id.HasValue)
                //    {
                //        transaction = JsonConvert.DeserializeObject<Transaction>(response.Content);
                //    }
                //    else
                //    {
                //        transaction = JsonConvert.DeserializeObject<List<Transaction>>(response.Content);
                //    }

                //    return transaction;
                //}

                //return null;

                if (id.HasValue)
                {
                    return await _unitOfWork.Transaction.GetByIdAsync(id.Value);
                }
                else
                {
                    return await _unitOfWork.Transaction.GetAllAsync();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Transaction> CreateTransaction(Transaction transaction)
        {
            try
            {
                //RestRequest request = new RestRequest($"/{transaction.Remetente}/{transaction.Recebedor}/{transaction.Valor}", Method.Post);
                //RestResponse response = await _restClient.ExecuteAsync(request);

                //if (response.IsSuccessful)
                //{
                //    Transaction newTransaction = JsonConvert.DeserializeObject<Transaction>(response.Content
                //    return newTransaction;
                //}

                //return null;

                transaction.Id = 0;
                _unitOfWork.Transaction.Add(transaction);
                await _unitOfWork.CommitAsync();

                return transaction;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<dynamic> UpdateTransaction(int id, int status)
        {
            try
            {
                //RestRequest request = new($"/{transaction.Id}/{transaction.Status}", Method.Post);
                //RestResponse response = await _restClient.ExecuteAsync(request);

                //Dictionary<dynamic, dynamic> responseObject = JsonConvert.DeserializeObject<Dictionary<dynamic, dynamic>>(response.Content);
                //if (response.IsSuccessful)
                //{
                //    return responseObject.Count > 1 ? JsonConvert.DeserializeObject<Transaction>(response.Content) : responseObject;
                //}

                //return null;

                Transaction UpdatedTransaction = await _unitOfWork.Transaction.GetByIdAsync(id);
                UpdatedTransaction.Status = status;

                _unitOfWork.Transaction.Update(UpdatedTransaction);
                await _unitOfWork.CommitAsync();

                return UpdatedTransaction;

            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

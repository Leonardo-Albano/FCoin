using FCoin.Business.Interfaces;
using FCoin.Models;
using FCoin.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;
using System.Runtime.CompilerServices;

namespace FCoin.Business
{
    public class TransactionManagement : ITransactionManagement
    {
        private readonly RestClient _restClient;
        private readonly IConfiguration _configuration;
        private readonly IHourManagement _hourManagement;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionManagement(IConfiguration configuration, IUnitOfWork unitOfWork, IHourManagement hourManagement)
        {
            _configuration = configuration;
            string ipConnection = _configuration["IpConnection"];
            _restClient = new RestClient($"{ipConnection}/transacoes");
            _unitOfWork = unitOfWork;
            _hourManagement = hourManagement;
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
                transaction.Data = await _hourManagement.GetHour();
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

        public async Task<int> CheckStatus(int id)
        {
            try
            {
                int result = await _unitOfWork.TransactionLink.CheckIfIsCompleted(id);

                if (result == 1)
                {
                    Transaction transaction = await _unitOfWork.Transaction.GetByIdAsync(id);
                    Client clientSender = await _unitOfWork.Client.GetByIdAsync(transaction.Remetente);
                    Client clientReceiver = await _unitOfWork.Client.GetByIdAsync(transaction.Recebedor);

                    clientSender.QtdMoeda -= transaction.Valor;
                    clientReceiver.QtdMoeda += transaction.Valor;

                    _unitOfWork.Client.Update(clientSender);
                    _unitOfWork.Client.Update(clientReceiver);

                    bool anyIncorrectValidator = await _unitOfWork.TransactionLink.AnyIncorrectValidator(id);
                    if (anyIncorrectValidator)
                    {
                        List<int> incorrectValidatorIds = await _unitOfWork.TransactionLink.GetIncorrectValidatorIds(id);
                        foreach (int validatorId in incorrectValidatorIds)
                        {
                            Validator validator = await _unitOfWork.Validator.GetByIdAsync(validatorId);
                            if (validator != null)
                            {
                                validator.Flags += 1;
                                _unitOfWork.Validator.Update(validator);
                            }
                        }
                    }

                    await _unitOfWork.CommitAsync();
                }

                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}

using FCoin.Business.Interfaces;
using FCoin.Models;
using FCoin.Repositories;
using Newtonsoft.Json;
using RestSharp;

namespace FCoin.Business
{
    public class ValidatorManagement : IValidatorManagement
    {
        private readonly ITransactionManagement _transactionManagement;
        private readonly IClientManagement _clientManagement;
        private readonly IHourManagement _hourManagement;
        private readonly IUnitOfWork _unitOfWork;

        public ValidatorManagement(ITransactionManagement transactionManagement, IClientManagement clientManagement, IHourManagement hourManagement, IUnitOfWork unitOfWork)
        {
            _transactionManagement = transactionManagement;
            _clientManagement = clientManagement;
            _hourManagement = hourManagement;
            _unitOfWork = unitOfWork;
        }

        public async Task<dynamic> GetValidator(int? id)
        {
            try
            {
                //dynamic validator;

                //if (id.HasValue)
                //{
                //    validator = await _unitOfWork.Validator.GetByIdAsync(id.Value);
                //}
                //else
                //{
                //    validator = await _unitOfWork.Validator.GetAllAsync();
                //}

                //return validator;


                if (id.HasValue)
                {
                    return await _unitOfWork.Validator.GetByIdAsync(id.Value);
                }
                else
                {
                    return await _unitOfWork.Validator.GetAllAsync();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Validator>> GetValidatorsBySelector(int selectorId)
        {
            try
            {
                List<Validator> validators = await _unitOfWork.Validator.ValidatorsBySelectorId(selectorId);
                return validators;
            }
            catch (Exception)
            {
                return new();
            }
        }

        public async Task<KeyValuePair<int, string>> CreateValidator(Validator validator)
        {
            try
            {
                Selector selector = await _unitOfWork.Selector.GetByIdAsync(validator.SelectorId);

                if (selector == null)
                    return new();

                _unitOfWork.Validator.Add(validator);

                await _unitOfWork.CommitAsync();
                return new(validator.Id, validator.Token);

            }
            catch (Exception)
            {
                return new();
            }
        }

        public async Task<bool> DeleteValidator(int id)
        {
            try
            {
                Validator validator = await _unitOfWork.Validator.GetByIdAsync(id);

                if (validator == null)
                {
                    return false;
                }

                _unitOfWork.Validator.Remove(validator);

                if (await _unitOfWork.CommitAsync() > 0)
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ValidateTransaction(int validatorId, string tokenValidator, int transactionId)
        {
            try
            {
                TransactionLink transactionLink = await _unitOfWork.TransactionLink.TransactionLinkByValidatorIdAndTransactionId(validatorId, transactionId);

                if(transactionLink == null)
                {
                    return false;
                }

                Transaction? transaction = await _transactionManagement.GetTransaction(transactionId);

                if (transaction == null)
                {
                    transactionLink.Success = 2;
                    _unitOfWork.TransactionLink.Update(transactionLink);
                    await _unitOfWork.CommitAsync();
                    return false;
                }

                Client? clientSender = await _clientManagement.GetClient(transaction.Remetente);
                Client? clientReceiver = await _clientManagement.GetClient(transaction.Recebedor);
                Validator validator = await GetValidator(validatorId);
                DateTime hour = await _hourManagement.GetHour();
                DateTime lastSuccessTransactionHour = await _unitOfWork.Transaction.LastTransaction();
                int numberOfTransactionsInLastMinute = await _unitOfWork.Transaction.TransactionsInLastMinuteCountByClientId(transaction.Remetente);

                if (clientSender == null || clientReceiver == null || validator == null)
                {
                    transactionLink.Success = 2;
                    _unitOfWork.TransactionLink.Update(transactionLink);
                    await _unitOfWork.CommitAsync();
                    return false;
                }

                bool isOnMaxTransactionsPermitted = numberOfTransactionsInLastMinute < 1000;
                if (!isOnMaxTransactionsPermitted)
                {
                    clientSender.InvalidoAte = hour.AddMinutes(1);
                    _unitOfWork.Client.Update(clientSender);
                }

                if (transaction.Valor < clientSender.QtdMoeda &&
                    hour >= transaction.Data &&
                    hour >= clientSender.InvalidoAte &&
                    transaction.Data > lastSuccessTransactionHour &&
                    isOnMaxTransactionsPermitted &&
                    tokenValidator == validator.Token)
                {
                    transactionLink.Success = 1;
                    _unitOfWork.TransactionLink.Update(transactionLink);
                    await _unitOfWork.CommitAsync();
                    return true;
                }

                transactionLink.Success = 2;
                _unitOfWork.TransactionLink.Update(transactionLink);
                await _unitOfWork.CommitAsync();
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<int?> LastTransactionToValidate(int validatorId)
        {
            try
            {
                return await _unitOfWork.TransactionLink.LastTransactionByValidator(validatorId);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

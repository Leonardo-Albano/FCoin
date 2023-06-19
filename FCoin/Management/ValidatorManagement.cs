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

        public async Task<int> CreateValidator(Validator validator)
        {
            try
            {
                Selector selector = await _unitOfWork.Selector.GetByIdAsync(validator.SelectorId);

                if (selector == null)
                    return 0;

                validator.Selector = selector;
                _unitOfWork.Validator.Add(validator);

                await _unitOfWork.CommitAsync();
                return validator.Id;

            }
            catch (Exception)
            {
                return 0;
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

        public async Task<bool> ValidateTransaction(int idValidator, string tokenValidator, int id)
        {
            try
            {
                Transaction? transaction = await _transactionManagement.GetTransaction(id);

                if (transaction == null)
                {
                    return false;
                }

                Client? clientSender = await _clientManagement.GetClient(transaction.Remetente);
                Client? clientReceiver = await _clientManagement.GetClient(transaction.Recebedor);
                Validator validator = await GetValidator(idValidator);
                DateTime? hour = await _hourManagement.GetHour();
                DateTime lastTransactionHour = await _unitOfWork.Transaction.LastTransaction();
                int numberOfTransactionsInLastMinute = await _unitOfWork.Transaction.TransactionsInLastMinuteCountByClientId(transaction.Remetente);

                if (clientSender == null || clientReceiver == null || hour == null)
                {
                    return false;
                }

                if (transaction.Valor > clientSender.QtdMoeda ||
                    hour >= transaction.Data ||
                    hour >= clientSender.InvalidoAte ||
                    transaction.Data > lastTransactionHour ||
                    numberOfTransactionsInLastMinute > 1000 ||
                    tokenValidator != validator.Token
                    //transaction.Time > lastTransaction.Time
                    //uniqueKey
                    )
                {
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

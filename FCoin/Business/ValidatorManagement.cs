using FCoin.Business.Interfaces;
using FCoin.Models;

namespace FCoin.Business
{
    public class ValidatorManagement : IValidatorManagement
    {
        private readonly ITransactionManagement _transactionManagement;
        private readonly IClientManagement _clientManagement;
        private readonly IHourManagement _hourManagement;

        public ValidatorManagement(ITransactionManagement transactionManagement, IClientManagement clientManagement, IHourManagement hourManagement)
        {
            _transactionManagement = transactionManagement;
            _clientManagement = clientManagement;
            _hourManagement = hourManagement;
        }

        public async Task<bool> ValidateTransaction(int id)
        {
            try
            {
                Transaction? transaction = await _transactionManagement.GetTransaction(id);

                if(transaction == null)
                {
                    return false;
                }

                Client? clientSender = await _clientManagement.GetClient(transaction.Remetente);
                Client? clientReceiver = await _clientManagement.GetClient(transaction.Recebedor);
                DateTime? hour = await _hourManagement.GetHour();

                if(clientSender == null || clientReceiver == null || hour == null)
                {
                    return false;
                }

                if (transaction.Valor > clientSender.QtdMoeda ||
                    hour > DateTime.Now
                    //transaction.Time > lastTransaction.Time
                    //numberOfTransactions > 1000      //Implementar Cooldown
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

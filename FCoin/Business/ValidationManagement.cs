using FCoin.Business.Interfaces;
using FCoin.Models;

namespace FCoin.Business
{
    public class ValidationManagement : IValidationManagement
    {
        public int ValidateTransaction(Transaction transaction)
        {
			try
			{
                if (transaction.CurrentAccountValue > transaction.TransfValue &&
                    transaction.ActualTime <= DateTime.Now && transaction.ActualTime > transaction.LastTransactionTime &&
                    transaction.QuantityTransactions < 1000)
                    // || transaction.key == uniqueKey )
                {
                    return 1;
                }

                return 2;
			}
			catch (Exception e)
			{
                return 0;
            }
        }
    }
}

using FCoin.Models;

namespace FCoin.Business.Interfaces
{
    public interface IValidationManagement
    {
        int ValidateTransaction(Transaction transaction);
    }
}

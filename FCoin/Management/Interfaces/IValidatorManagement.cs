using FCoin.Models;

namespace FCoin.Business.Interfaces
{
    public interface IValidatorManagement
    {
        Task<dynamic> GetValidator(int? id);
        Task<List<Validator>> GetValidatorsBySelector(int selectorId);
        Task<KeyValuePair<int, string>> CreateValidator(Validator validator);
        Task<bool> DeleteValidator(int id);
        Task<bool> ValidateTransaction(int validatorId, string tokenValidator, int transactionId);
        Task<int?> LastTransactionToValidate(int validatorId);
    }
}
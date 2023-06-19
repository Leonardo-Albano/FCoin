using FCoin.Models;

namespace FCoin.Business.Interfaces
{
    public interface IValidatorManagement
    {
        Task<dynamic> GetValidator(int? id);
        Task<List<Validator>> GetValidatorsBySelector(int selectorId);
        Task<int> CreateValidator(Validator validator);
        Task<bool> DeleteValidator(int id);
        Task<bool> ValidateTransaction(int idValidator, string tokenValidator, int id);
    }
}
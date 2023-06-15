using FCoin.Models;

namespace FCoin.Business.Interfaces
{
    public interface IValidatorManagement
    {
        Task<bool> ValidateTransaction(int idValidator, string tokenValidator, int id);
        Task<dynamic> GetValidator(int? id);
        Task<Validator> CreateValidator(Validator validator);
        Task<bool> DeleteValidator(int id);
    }
}
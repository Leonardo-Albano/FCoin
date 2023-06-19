using FCoin.Models;

namespace FCoin.Repositories.Interfaces
{
    public interface IValidatorRepository : IRepository<Validator>
    {
        Task<List<Validator>> ValidatorsBySelectorId(int selectorId);
    }
}
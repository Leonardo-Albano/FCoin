using FCoin.Models;

namespace FCoin.Repositories.Interfaces
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<DateTime> LastTransaction();
        Task<int> TransactionsInLastMinuteCountByClientId(int id);
    }
}
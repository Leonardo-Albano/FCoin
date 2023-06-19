using FCoin.Models;

namespace FCoin.Business.Interfaces
{
    public interface ITransactionManagement
    {
        Task<Transaction> CreateTransaction(Transaction transaction);
        Task<dynamic> GetTransaction(int? id);
        Task<dynamic> UpdateTransaction(int id, int status);
    }
}
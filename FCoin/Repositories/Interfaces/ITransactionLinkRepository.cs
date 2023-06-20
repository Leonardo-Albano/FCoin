using FCoin.Models;

namespace FCoin.Repositories.Interfaces
{
    public interface ITransactionLinkRepository : IRepository<TransactionLink>
    {
        Task<int> LastTransactionByValidator(int validatorId);
        Task<TransactionLink> TransactionLinkByValidatorIdAndTransactionId(int validatorId, int transactionId);
        Task<bool> AnyIncorrectValidator(int transactionId);
        Task<List<int>> GetIncorrectValidatorIds(int transactionId);
        Task<int> CheckIfIsCompleted(int transactionId);
    }
}
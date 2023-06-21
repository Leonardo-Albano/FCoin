using FCoin.Models;
using FCoin.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace FCoin.Repositories
{
    public class TransactionLinkRepository : Repository<TransactionLink>, ITransactionLinkRepository
    {
        private readonly FDbContext _context;
        public TransactionLinkRepository(DbContext context) : base(context)
        {
        }

        public async Task<TransactionLink> TransactionLinkByValidatorIdAndTransactionId(int validatorId, int transactionId)
        {
            return await _context.TransactionsLink
                            .FirstOrDefaultAsync(t => t.ValidatorId == validatorId 
                                                 && t.TransactionId == transactionId
                                                 && t.Success == 0);
        }

        public async Task<bool> AnyIncorrectValidator(int transactionId)
        {
            return await _context.TransactionsLink
                .AnyAsync(t => t.TransactionId == transactionId && t.Success == 2);
        }

        public async Task<List<int>> GetIncorrectValidatorIds(int transactionId)
        {
            return await _context.TransactionsLink
                .Where(t => t.TransactionId == transactionId && t.Success == 2)
                .Select(t => t.ValidatorId)
                .Distinct()
                .ToListAsync();
        }

        public async Task<int> CheckIfIsCompleted(int transactionId)
        {
            List<TransactionLink> transactionLinks = await _context.TransactionsLink
                .Where(t => t.TransactionId == transactionId)
                .ToListAsync();

            bool allCompleted = transactionLinks.All(t => t.Success != 0);
            if (!allCompleted)
                return 0;

            int trueCount = transactionLinks.Count(t => t.Success == 1);
            int falseCount = transactionLinks.Count(t => t.Success == 2);

            Transaction transaction = await _context.Transactions.FindAsync(transactionId);

            if (transaction != null)
            {
                if (trueCount > falseCount)
                {
                    transaction.Status = 1;
                }
                else
                {
                    transaction.Status = 2;
                }

                _context.Transactions.Update(transaction);
                await _context.SaveChangesAsync();

                return transaction.Status;
            }

            return 0;
        }

        public async Task<List<int>> BusyValidators()
        {
            return await _context.TransactionsLink
                            .Where(t => t.Success == 0)
                            .Select(t=>t.ValidatorId)
                            .ToListAsync();
        }
        public async Task<int> LastTransactionByValidator(int validatorId)
        {
            return await _context.TransactionsLink
                            .Where(t => t.ValidatorId == validatorId && t.Success == 0)
                            .OrderBy(t => t.Id)
                            .Select(f=>f.TransactionId)
                            .FirstOrDefaultAsync();
        }

        public TransactionLinkRepository(FDbContext STContext) : base(STContext)
        {
            _context = STContext;
        }
    }
}

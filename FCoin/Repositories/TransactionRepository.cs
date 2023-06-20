using FCoin.Models;
using FCoin.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FCoin.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        private readonly FDbContext _context;
        public TransactionRepository(DbContext context) : base(context)
        {
        }

        public async Task<int> TransactionsInLastMinuteCountByClientId(int id)
        {
            DateTime lastMinute = DateTime.Now.AddMinutes(-1);

            return await _context.Transactions
                .CountAsync(t => t.Remetente == id && t.Data >= lastMinute);
        }

        public async Task<DateTime> LastTransaction()
        {
            return await _context.Transactions
                .Where(t=>t.Status == 1)
                .OrderByDescending(t => t.Data)
                .Select(t=>t.Data)
                .FirstOrDefaultAsync();
        }
        public TransactionRepository(FDbContext STContext) : base(STContext)
        {
            _context = STContext;
        }
    }
}

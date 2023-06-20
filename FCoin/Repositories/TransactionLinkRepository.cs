using FCoin.Models;
using Microsoft.EntityFrameworkCore;

namespace FCoin.Repositories
{
    public class TransactionLinkRepository : Repository<TransactionLink>, ITransactionLinkRepository
    {
        private FDbContext _context;
        public TransactionLinkRepository(DbContext context) : base(context)
        {
        }

        public TransactionLinkRepository(FDbContext STContext) : base(STContext)
        {
            _context = STContext;
        }
    }
}

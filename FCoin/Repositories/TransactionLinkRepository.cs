using FCoin.Models;
using Microsoft.EntityFrameworkCore;

namespace FCoin.Repositories
{
    public class TransactionLinkRepository : Repository<TransactionLink>, ITransactionLinkRepository
    {
        public TransactionLinkRepository(DbContext context) : base(context)
        {
        }
    }
}

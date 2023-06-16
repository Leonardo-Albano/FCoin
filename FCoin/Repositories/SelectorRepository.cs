using FCoin.Models;
using Microsoft.EntityFrameworkCore;

namespace FCoin.Repositories
{
    public class SelectorRepository : Repository<Selector>, ISelectorRepository
    {
        private readonly FDbContext _context;
        public SelectorRepository(DbContext context) : base(context)
        {

        }

        public SelectorRepository(FDbContext STContext) : base(STContext)
        {
            _context = STContext;
        }
    }
}

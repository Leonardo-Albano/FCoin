using FCoin.Models;
using FCoin.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FCoin.Repositories
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        private readonly FDbContext _context;
        public ClientRepository(DbContext context) : base(context)
        {
        }


        public ClientRepository(FDbContext STContext) : base(STContext)
        {
            _context = STContext;
        }
    }
}

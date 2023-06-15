using FCoin.Models;
using FCoin.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FCoin.Repositories
{
    public class ValidatorRepository : Repository<Validator>, IValidatorRepository
    {
        private FDbContext _context;
        public ValidatorRepository(DbContext context) : base(context)
        {
        }



        public ValidatorRepository(FDbContext STContext) : base(STContext)
        {
            _context = STContext;
        }
    }
}

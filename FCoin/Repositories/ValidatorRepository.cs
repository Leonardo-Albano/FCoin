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
        
        public async Task<List<Validator>> ValidatorsBySelectorId(int selectorId)
        {
            return await _context.Validators
                        .Where(v=>v.SelectorId == selectorId)
                        .ToListAsync();
        }
        
        public async Task<int> OffersBySelector (int selectorId)
        {
            return await _context.Validators.Where(v => v.SelectorId == selectorId).SumAsync(v => v.Offer);
        }

        public ValidatorRepository(FDbContext STContext) : base(STContext)
        {
            _context = STContext;
        }
    }
}

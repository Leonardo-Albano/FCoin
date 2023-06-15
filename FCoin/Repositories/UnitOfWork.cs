using FCoin.Repositories.Interfaces;

namespace FCoin.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IClientRepository Client { get; }
        ITransactionRepository Transaction { get; }
        IValidatorRepository Validator { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<int> CommitAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly FDbContext _context;
        private IClientRepository _clientRepository;
        private ITransactionRepository _transactionRepository;
        private IValidatorRepository _validatorRepository;

        public UnitOfWork(FDbContext context,
                          ITransactionRepository transactionRepository,
                          IClientRepository clientRepository,
                          IValidatorRepository validatorRepository)
        {
            _context = context;
            _transactionRepository = transactionRepository;
            _clientRepository = clientRepository;
            _validatorRepository = validatorRepository;
        }

        public IClientRepository Client => _clientRepository;
        public ITransactionRepository Transaction => _transactionRepository;
        public IValidatorRepository Validator => _validatorRepository;

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}

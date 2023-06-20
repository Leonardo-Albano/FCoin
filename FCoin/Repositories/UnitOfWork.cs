using FCoin.Repositories.Interfaces;

namespace FCoin.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IClientRepository Client { get; }
        ISelectorRepository Selector { get; }
        ITransactionRepository Transaction { get; }
        IValidatorRepository Validator { get; }
        ITransactionLinkRepository TransactionLink { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<int> CommitAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly FDbContext _context;
        private IClientRepository _clientRepository;
        private ISelectorRepository _selectorRepository;
        private ITransactionRepository _transactionRepository;
        private IValidatorRepository _validatorRepository;
        private ITransactionLinkRepository _transactionLinkRepository;

        public UnitOfWork(FDbContext context,
                          ITransactionRepository transactionRepository,
                          IClientRepository clientRepository,
                          IValidatorRepository validatorRepository,
                          ITransactionLinkRepository transactionLinkRepository)
        {
            _context = context;
            _transactionRepository = transactionRepository;
            _clientRepository = clientRepository;
            _validatorRepository = validatorRepository;
            _transactionLinkRepository = transactionLinkRepository;
        }

        public IClientRepository Client => _clientRepository;
        public ISelectorRepository Selector => _selectorRepository;
        public ITransactionRepository Transaction => _transactionRepository;
        public IValidatorRepository Validator => _validatorRepository;
        public ITransactionLinkRepository TransactionLink => _transactionLinkRepository;

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

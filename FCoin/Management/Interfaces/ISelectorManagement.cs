using FCoin.Models;

namespace FCoin.Business.Interfaces
{
    public interface ISelectorManagement
    {
        Task<dynamic> GetSelector(int? id);
        Task<Selector> CreateSelector(Selector selector);
        Task<dynamic> UpdateSelector(int id, Selector selector);
        Task<bool> DeleteSelector(int id);
        Task<List<int>> SelectValidators(int id, int transactionId);
    }
}
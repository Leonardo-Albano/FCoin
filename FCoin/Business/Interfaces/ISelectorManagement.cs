using FCoin.Models;

namespace FCoin.Business.Interfaces
{
    public interface ISelectorManagement
    {
        Task<dynamic> GetSelector(int? id);
        Task<Selector> CreateSelector(Selector selector);
        //Task<dynamic> UpdateSelector(int id, int qtdMoeda);
        //Task<bool> DeleteSelector(int id);
    }
}
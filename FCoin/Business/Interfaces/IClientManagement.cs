using FCoin.Models;

namespace FCoin.Business.Interfaces
{
    public interface IClientManagement
    {
        Task<dynamic> GetClient(int? id);
        Task<Client> CreateClient(Client client);
        Task<dynamic> UpdateClient(int id, int qtdMoeda);
    }
}
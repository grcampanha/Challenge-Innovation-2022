using Revosoft.Models;

namespace Revosoft.Repositories.Interfaces
{
    public interface IStoreRepository
    {
        IEnumerable<Store> Store { get; }
        Store GetStoreById(int storeId);
    }
}

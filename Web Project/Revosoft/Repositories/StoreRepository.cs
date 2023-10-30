using Revosoft.Context;
using Revosoft.Models;
using Revosoft.Repositories.Interfaces;

namespace Revosoft.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly AppDbContext _context;
        public StoreRepository(AppDbContext contexto)
        {
            _context = contexto;
        }

        public IEnumerable<Store> Store => _context.Store;

        public Store GetStoreById(int storeId)
        {
            return _context.Store.FirstOrDefault(s => s.StoreId == storeId);
        }
    }
}

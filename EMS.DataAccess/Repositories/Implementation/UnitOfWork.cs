using EMS.DataAccess.Data;
using EMS.DataAccess.Repositories.Contracts;
namespace EMS.DataAccess.Repositories.Implementation
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly AppDbContext _context;
        private bool _disposed = false;

        public UnitOfWork(AppDbContext context) => _context = context;
        public IGenericRepository<T> GenericRepository<T>() where T : class
        {
            return new GenericRepository<T>(_context);
        }
        public void Complete() => _context.SaveChanges();
        ~UnitOfWork()
        {
            Dispose();
        }
        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

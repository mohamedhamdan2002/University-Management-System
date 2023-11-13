using EMS.DataAccess.Data;
using EMS.DataAccess.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;


namespace EMS.DataAccess.Repositories.Implementation
{
    public class GenericRepository<T> : IDisposable, IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        private bool _disposed = false;
        internal DbSet<T> _dbset;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
            
        }
        public void Add(T entity) =>_dbset.Add(entity);
        public void AddRange(IEnumerable<T> entities) => _dbset.AddRange(entities);
        public void Delete(T entity) =>_dbset.Remove(entity);
        public void DeleteRange(IEnumerable<T> entities) => _dbset.RemoveRange(entities);
        public IQueryable<T> GetAll(Expression<Func<T, bool>>? predicate = null, string includeProperties = "")
        {
            IQueryable<T> query = _dbset;
            if(predicate is not null)
            {
                query = _dbset.Where(predicate);

            }
            foreach(var property in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(property);
            }
            return query;
        }
        public T? GetById(object id) => _dbset.Find(id);

        public virtual void Update(T entity) => _dbset.Update(entity);
        public void UpdateRange(IEnumerable<T> entities) => _dbset.UpdateRange(entities);

        public T? GetByWith(Expression<Func<T, bool>> filter, string withProperties = "", Expression<Func<T, bool>>? filter2 = null)
        {
            IQueryable<T> result = _dbset.Where(filter);

            foreach (var property in withProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                if(filter2 is not null)
                    result = _dbset.Include(property).Where(filter2);
                else
                {
                    result = result.Include(property);

                }
            }
            return result.FirstOrDefault();
        }

        // implementation of IDisposable pattern

        ~GenericRepository()
        {
            Dispose(false);
        }
        protected void Dispose(bool disposing)
        { 
            if(!_disposed)
            {
                if(disposing)
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

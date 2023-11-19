using Microsoft.EntityFrameworkCore;
using UMS.DataAccess.Data;
using UMS.DataAccess.Repositories.Contracts;
using System.Linq.Expressions;

namespace UMS.DataAccess.Repositories.Implementation
{
    public abstract class RepositoryBase<T> : IDisposable, IRepositoryBase<T> where T : class
    {
        protected readonly AppDbContext Context;
        private bool _disposed = false;
        public RepositoryBase(AppDbContext context) => Context = context;
        public void Create(T entity) => Context.Set<T>().Add(entity);

        public void Delete(T entity) => Context.Set<T>().Remove(entity);


        public IQueryable<T> GetAll(bool trackChanges, string[]? includes = null)
        {
            IQueryable<T> query = Context.Set<T>();
            if (!trackChanges)
               query = query.AsNoTracking();
            if(includes is not null)
            {
                foreach (var include in includes)
                    query = query.Include(include.Trim());
            }
            return query;
           
        }

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression, bool trackChanges, string[]? includes = null)
        {
            IQueryable<T> query = Context.Set<T>().Where(expression);
            if(!trackChanges)
                query = query.AsNoTracking();

            if(includes is not null)
            {
                foreach(var include in includes)
                    query = query.Include(include.Trim());
            }
            return query;
        }
        public void Update(T entity) => Context.Set<T>().Update(entity);
        
        
        protected void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if(disposing)
                {
                    Context.Dispose();
                }
                _disposed = true;
            }
        }
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~RepositoryBase() => Dispose(false);
    }
}

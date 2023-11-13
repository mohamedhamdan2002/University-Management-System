using System.Linq.Expressions;

namespace EMS.DataAccess.Repositories.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        
        IQueryable<T> GetAll(Expression<Func<T, bool>>? predicate = null,
            string includeProperties = "");
        T? GetById(object id);
        T? GetByWith(Expression<Func<T, bool>> filter, string withProperties = "", Expression<Func<T, bool>>? filter2 = null );
        
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void  UpdateRange(IEnumerable<T> entities);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);

    }
}

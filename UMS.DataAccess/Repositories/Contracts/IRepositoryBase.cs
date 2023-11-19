using System.Linq.Expressions;

namespace UMS.DataAccess.Repositories.Contracts
{
    public interface IRepositoryBase<T> where T : class
    {
        IQueryable<T> GetAll(bool trackChanges, string[]? includes = null);
        IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression, bool trackChanges, string[]? includes = null);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}

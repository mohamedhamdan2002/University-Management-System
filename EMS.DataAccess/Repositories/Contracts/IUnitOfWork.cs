
namespace EMS.DataAccess.Repositories.Contracts
{
    public interface IUnitOfWork
    {
        public IGenericRepository<T> GenericRepository<T>() where T : class;
        void Complete();
    }
}

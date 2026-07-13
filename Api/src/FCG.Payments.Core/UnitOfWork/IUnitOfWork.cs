using FCG.Payments.Core.Repository;

namespace FCG.Payments.Core.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChanges();

    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
}

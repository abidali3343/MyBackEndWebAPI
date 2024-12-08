using System.Data;

namespace FMS_Camerige.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        Task<int> SaveChangesAsync();
        void BeginTransaction();
        void Commit();
        void Rollback();  // Add this line to define the Rollback method
    }
}

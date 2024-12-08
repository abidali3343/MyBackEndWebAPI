using System.Data;
using System.Data.SqlClient;

namespace FMS_Camerige.UoW
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IDbConnection _connection;
        private IDbTransaction _transaction;

        public UnitOfWork(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            try
            {
                _connection.Open();
            }
            catch (Exception ex)
            {
                // Log or handle connection error
                throw new InvalidOperationException("Failed to open database connection.", ex);
            }
        }

        public IDbConnection Connection => _connection;
        public IDbTransaction Transaction => _transaction;

        public void BeginTransaction()
        {
            if (_connection.State != ConnectionState.Open)
            {
                throw new InvalidOperationException("Connection must be open to begin a transaction.");
            }
            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("Transaction has not been started.");
            }

            try
            {
                _transaction.Commit();
                _transaction = null; // Ensure it cannot be used again
            }
            catch (Exception ex)
            {
                Rollback();
                throw new InvalidOperationException("Failed to commit the transaction.", ex);
            }
        }

        public void Rollback()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("Transaction has not been started.");
            }

            try
            {
                _transaction.Rollback();
                _transaction = null; // Ensure it cannot be used again
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to rollback the transaction.", ex);
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            // If there are pending operations, implement logic here
            try
            {
                // Placeholder logic: return success as 1
                return await Task.FromResult(1);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to save changes.", ex);
            }
        }

        public void Dispose()
        {
            try
            {
                _transaction?.Dispose();
            }
            catch (Exception ex)
            {
                // Log transaction disposal error
                Console.WriteLine($"Transaction disposal failed: {ex.Message}");
            }

            try
            {
                _connection?.Dispose();
            }
            catch (Exception ex)
            {
                // Log connection disposal error
                Console.WriteLine($"Connection disposal failed: {ex.Message}");
            }
        }
    }

}

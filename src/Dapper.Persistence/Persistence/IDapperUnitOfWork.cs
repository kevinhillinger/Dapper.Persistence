using System;
using System.Data;

namespace Dapper.Persistence
{
    /// <summary>
    /// Unit of work that can be extended.
    /// </summary>
    public interface IDapperUnitOfWork : IDisposable
    {
        /// <summary>
        /// Begins a transaction for work. The default isolation level is Read Committed. <see cref="IsolationLevel"/>
        /// </summary>
        IDapperDbTransactionHandle Begin(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

        /// <summary>
        /// Commits the changes
        /// </summary>
        void Commit();

        /// <summary>
        /// Rolls back the changes since Begin was called
        /// </summary>
        void Rollback();
    }
}
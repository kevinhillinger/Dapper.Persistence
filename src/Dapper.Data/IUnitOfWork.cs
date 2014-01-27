using System;
using System.Data;

namespace Dapper.Data
{
    /// <summary>
    /// Unit of work / data session
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets the instance id
        /// </summary>
        Guid InstanceId { get; }

        /// <summary>
        /// Begins a transaction for work. The default isolation level is Read Committed. <see cref="IsolationLevel"/>
        /// </summary>
        ITransactionHandle Begin(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

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
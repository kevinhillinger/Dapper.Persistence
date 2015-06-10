using System;
using System.Data;

namespace Dapper.Persistence
{
    /// <summary>
    /// Provides a handle to a transaction
    /// </summary>
    /// <example>
    /// 
    /// using (unitofWork.Begin())
    /// {
    ///     //do work
    /// } //disposes of the transaction without throwing away the unit of work
    /// 
    /// </example>
    /// <remarks>
    /// Why? This will be used by the IUnitOfWork so that a Begin (which creates an underlying db transaction) can return
    /// a handle, supporting the using() {} statement. Yes, this is fencing in what you can do with the db transaction
    /// and avoids direct access to the underlying transaction object
    /// </remarks>
    public interface IDapperDbTransactionHandle : IDisposable
    {
        /// <summary>
        /// Triggered after Dispose is called
        /// </summary>
        event EventHandler<EventArgs> Disposed;

        /// <summary>
        /// Gets the isolation level
        /// </summary>
        IsolationLevel IsolationLevel { get; }
    }
}
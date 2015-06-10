using System;
using System.Data;

namespace Dapper.Persistence
{
    /// <summary>
    /// default implementation of transaction handle
    /// </summary>
    class TransactionHandle : IDapperDbTransactionHandle
    {
        private IDbTransaction transaction;

        /// <summary>
        /// Triggered after Dispose is called
        /// </summary>
        public event EventHandler<EventArgs> Disposed;

        /// <summary>
        /// Gets the isolation level of the transaction
        /// </summary>
        public IsolationLevel IsolationLevel { get; private set; }

        public TransactionHandle(IDbTransaction transaction)
        {
            this.transaction = transaction;
            IsolationLevel = transaction.IsolationLevel;
        }

        public void Dispose()
        {
            if (transaction != null)
            {
                transaction.Commit();
                transaction.Dispose();

                OnDisposed();

                transaction = null;
            }
        }

        protected virtual void OnDisposed()
        {
            var handler = Disposed;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
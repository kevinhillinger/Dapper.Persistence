using System;
using System.Data;

namespace Dapper.Persistence
{
    /// <summary>
    /// default implementation of transaction handle
    /// </summary>
    internal class TransactionHandle : ITransactionHandle
    {
        private IDbTransaction _transaction;

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
            _transaction = transaction;
            IsolationLevel = transaction.IsolationLevel;
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Commit();
                _transaction.Dispose();

                OnDisposed();

                _transaction = null;
            }
        }

        protected virtual void OnDisposed()
        {
            var handler = Disposed;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
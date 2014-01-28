using System;
using System.Data;
using NSubstitute;
using Xunit;

namespace Dapper.Persistence.Tests
{
    public class DbContextTest
    {
        [Fact]
        public void should_use_a_db_connection()
        {
            var connectionMock = Substitute.For<IDbConnection>();

            Assert.DoesNotThrow(() => new DbContext(connectionMock));
        }

        [Fact]
        public void should_need_a_connection_instance()
        {
            Assert.Throws<NullReferenceException>(() => new DbContext(null));
        }

        [Fact]
        public void should_open_connection()
        {
            var mock = Mocks.New();

            // closed connection
            new DbContext(mock.Connection);

            mock.Connection.Received(1).Open();

            //try with an already open connection
            mock.Connection.State.Returns(ConnectionState.Open);
            mock.Connection.ClearReceivedCalls();

            new DbContext(mock.Connection);

            mock.Connection.Received(0).Open();
        }

        [Fact]
        public void should_open_connection_for_BeingScope()
        {
            var mock = Mocks.New();

            var context = new DbContext(mock.Connection);

            mock.Connection.State.Returns(ConnectionState.Closed); //close the connection to test since it will open in the constructor
            mock.Connection.ClearReceivedCalls();

            context.Begin();

            mock.Connection.Received(1).Open();
        }

        [Fact]
        public void should_create_transactions_from_connection()
        {
            var mock = Mocks.New();

            var context = new DbContextSpy(mock.Connection, mock.TransactionHandle);

            mock.Connection.State.Returns(ConnectionState.Closed); //close the connection to test since it will open in the constructor
            mock.Connection.ClearReceivedCalls();

            var handle = context.Begin();

            mock.Connection.ReceivedWithAnyArgs(1).BeginTransaction(mock.AnyIsolationLevel);
            Assert.Same(mock.TransactionHandle, handle);
        }

        [Fact]
        public void should_throw_if_BeginScope_called_after_Dispose()
        {
            var mock = Mocks.New();

            var context = new DbContext(mock.Connection);
            context.Dispose();
            
            Assert.Throws<ObjectDisposedException>(() => context.Begin());
        }

        [Fact]
        public void should_commit_transaction()
        {
            var mock = Mocks.New();

            var context = new DbContext(mock.Connection);
            context.Begin();

            using (context)
            {
                context.Commit();
                mock.Transaction.Received(1).Commit();
            }
        }

        private class DbContextSpy : DbContext
        {
            private readonly ITransactionHandle _transactionHandleMock;

            public DbContextSpy(IDbConnection connection, ITransactionHandle transactionHandleMock) : base(connection)
            {
                _transactionHandleMock = transactionHandleMock;
            }

            protected override ITransactionHandle EnsureTransaction(IsolationLevel isolationLevel)
            {
                base.EnsureTransaction(isolationLevel);
                return _transactionHandleMock;
            }
        }

        private class Mocks
        {
            public IsolationLevel AnyIsolationLevel = IsolationLevel.ReadCommitted;

            public IDbConnection Connection { get; private set; }
            public ITransactionHandle TransactionHandle { get; private set; }
            public IDbTransaction Transaction { get; private set; }


            private Mocks()
            {
                Transaction = Substitute.For<IDbTransaction>();

                Connection = Substitute.For<IDbConnection>();
                Connection.State.Returns(ConnectionState.Closed);
                Connection.BeginTransaction(AnyIsolationLevel).ReturnsForAnyArgs(Transaction);

                TransactionHandle = Substitute.For<ITransactionHandle>();
            }

            public static Mocks New()
            {
                return new Mocks();
            }
        }
    }
}
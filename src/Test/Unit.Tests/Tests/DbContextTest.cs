namespace Unit.Tests.Tests
{
    using System;
    using System.Data;
    using Dapper.Persistence;
    using NSubstitute;
    using Xunit;

    public class DbContextTest
    {
        [Fact]
        public void should_use_a_db_connection()
        {
            var connectionMock = Substitute.For<IDbConnection>();
            var context = new DapperContext(connectionMock);

            Assert.NotNull(context);
        }

        [Fact]
        public void should_need_a_connection_instance()
        {
            Assert.Throws<NullReferenceException>(() => new DapperContext(null));
        }

        [Fact]
        public void should_open_connection()
        {
            var mock = Mocks.New();

            // closed connection
            new DapperContext(mock.Connection);

            mock.Connection.Received(1).Open();

            //try with an already open connection
            mock.Connection.State.Returns(ConnectionState.Open);
            mock.Connection.ClearReceivedCalls();

            new DapperContext(mock.Connection);

            mock.Connection.Received(0).Open();
        }

        [Fact]
        public void should_open_connection_for_BeingScope()
        {
            var mock = Mocks.New();

            var context = new DapperContext(mock.Connection);

            mock.Connection.State.Returns(ConnectionState.Closed); //close the connection to test since it will open in the constructor
            mock.Connection.ClearReceivedCalls();

            context.Begin();

            mock.Connection.Received(1).Open();
        }

        [Fact]
        public void should_create_transactions_from_connection()
        {
            var mock = Mocks.New();

            var context = new DapperContextSpy(mock.Connection, mock.DapperDbTransactionHandle);

            mock.Connection.State.Returns(ConnectionState.Closed); //close the connection to test since it will open in the constructor
            mock.Connection.ClearReceivedCalls();

            var handle = context.Begin();

            mock.Connection.ReceivedWithAnyArgs(1).BeginTransaction(mock.AnyIsolationLevel);
            Assert.Same(mock.DapperDbTransactionHandle, handle);
        }

        [Fact]
        public void should_throw_if_BeginScope_called_after_Dispose()
        {
            var mock = Mocks.New();

            var context = new DapperContext(mock.Connection);
            context.Dispose();
            
            Assert.Throws<ObjectDisposedException>(() => context.Begin());
        }

        [Fact]
        public void should_commit_transaction()
        {
            var mock = Mocks.New();

            var context = new DapperContext(mock.Connection);
            context.Begin();

            using (context)
            {
                context.Commit();
                mock.Transaction.Received(1).Commit();
            }
        }

        private class DapperContextSpy : DapperContext
        {
            private readonly IDapperDbTransactionHandle dapperDbTransactionHandleMock;

            public DapperContextSpy(IDbConnection connection, IDapperDbTransactionHandle dapperDbTransactionHandleMock) : base(connection)
            {
                this.dapperDbTransactionHandleMock = dapperDbTransactionHandleMock;
            }

            protected override IDapperDbTransactionHandle EnsureTransaction(IsolationLevel isolationLevel)
            {
                base.EnsureTransaction(isolationLevel);
                return dapperDbTransactionHandleMock;
            }
        }

        private class Mocks
        {
            public IsolationLevel AnyIsolationLevel = IsolationLevel.ReadCommitted;

            public IDbConnection Connection { get; private set; }
            public IDapperDbTransactionHandle DapperDbTransactionHandle { get; private set; }
            public IDbTransaction Transaction { get; private set; }


            private Mocks()
            {
                Transaction = Substitute.For<IDbTransaction>();

                Connection = Substitute.For<IDbConnection>();
                Connection.State.Returns(ConnectionState.Closed);
                Connection.BeginTransaction(AnyIsolationLevel).ReturnsForAnyArgs(Transaction);

                DapperDbTransactionHandle = Substitute.For<IDapperDbTransactionHandle>();
            }

            public static Mocks New()
            {
                return new Mocks();
            }
        }
    }
}
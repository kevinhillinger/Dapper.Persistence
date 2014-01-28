using System.Data;
using NSubstitute;
using Xunit;

namespace Dapper.Persistence.Tests
{
    public class TransactionHandleTest
    {
        [Fact(DisplayName = "TransactionHandle: should be ITransactionHandle")]
        public void should_be_ITransactionHandle()
        {
            var transMock = Substitute.For<IDbTransaction>();
            Assert.IsAssignableFrom<ITransactionHandle>(new TransactionHandle(transMock));
        }

        [Fact(DisplayName = "TransactionHandle: should dipose of transaction")]
        public void should_dispose_of_transaction()
        {
            var transactionMock = Substitute.For<IDbTransaction>();
            var scope = new TransactionHandle(transactionMock);

            scope.Dispose();

            transactionMock.Received(1).Dispose();
        }

        [Fact(DisplayName = "TransactionHandle: should commit transaction if disposed")]
        public void should_commit_transaction()
        {
            var transactionMock = Substitute.For<IDbTransaction>();
            var scope = new TransactionHandle(transactionMock);

            scope.Dispose();

            transactionMock.Received(1).Commit();
        }

        [Fact(DisplayName = "TransactionHandle: should only commit and dispose of valid transaction")]
        public void should_not_use_transaction_if_null()
        {
            var transactionMock = Substitute.For<IDbTransaction>();
            var scope = new TransactionHandle(transactionMock);

            scope.Dispose();

            //call it again (shouldn't do anything)
            scope.Dispose();

            transactionMock.Received(1).Commit();
        }

        [Fact(DisplayName = "TransactionHandle: should trigger disposed event")]
        public void should_trigger_disposed_event()
        {
            var wasDisposedEventHandlerTriggered = false;
            var transaction = Substitute.For<IDbTransaction>();
            var scope = new TransactionHandle(transaction);

            scope.Disposed += (sender, e) => wasDisposedEventHandlerTriggered = true;
            scope.Dispose();

            Assert.True(wasDisposedEventHandlerTriggered);
        } 
    }
}
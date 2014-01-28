using NSubstitute;
using Xunit;

namespace Dapper.Persistence.Tests
{
    public class DbRepositoryTest
    {
        [Fact]
        public void should_provide_context_to_inheritor()
        {
            var contextMock = Substitute.For<IDbContext>();
            var repository = new DbRepositorySpy(contextMock);

            Assert.Same(contextMock, repository.GetContext());
        } 

        internal class DbRepositorySpy : DbRepository
        {
            public DbRepositorySpy(IDbContext context) : base(context)
            {
            }

            public IDbContext GetContext()
            {
                return Context;
            }
        }
    }
}
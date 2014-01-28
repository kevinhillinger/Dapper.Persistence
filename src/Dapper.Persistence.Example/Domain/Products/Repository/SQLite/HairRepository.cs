using Dapper.Persistence;

namespace Example.Domain.Products.Repository.SQLite
{
    public class HairRepository : DbRepository
    {
        public HairRepository(IDbContext context) : base(context)
        {
        }
    }
}
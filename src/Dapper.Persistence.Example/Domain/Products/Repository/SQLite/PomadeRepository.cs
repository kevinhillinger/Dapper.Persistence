using Dapper.Persistence;

namespace Example.Domain.Products.Repository.SQLite
{
    public class PomadeRepository : DbRepository
    {
        public PomadeRepository(IDbContext context) : base(context)
        {
        }

        public void Insert(Pomade pomade)
        {
            
        }
    }

    public class Brand
    {
        public string Name { get; set; }
    }
}
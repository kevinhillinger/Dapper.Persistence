using System.Data;
using Dapper.Persistence;
using StructureMap.Configuration.DSL;

namespace Example.Infrastructure
{
    public class ProgramRegistry : Registry
    {
         public ProgramRegistry()
         {
             ForSingletonOf<SqliteConnectionFactory>().Use<SqliteConnectionFactory>();

             //wireup dapper persistence. that's it!
             For<IDbConnection>().Use(c => c.GetInstance<SqliteConnectionFactory>().Create());
             For<IUnitOfWork>().Use<DbContext>();
             For<IDbContext>().Use<DbContext>();

         }
    }
}
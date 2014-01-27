namespace Dapper.Mousse
{
    /// <summary>
    /// Base repository implementation that depends on an <see cref="IDbContext"/>
    /// </summary>
    public abstract class DbRepository
    {
        private readonly IDbContext _context;

        /// <summary>
        /// Gets the db context
        /// </summary>
        protected IDbContext Context
        {
            get { return _context; }
        }

        /// <summary>
        /// Creates a DbRepository with an <see cref="IDbContext"/>
        /// </summary>
        /// <param name="context"></param>
        protected DbRepository(IDbContext context)
        {
            _context = context;
        }
    }
}
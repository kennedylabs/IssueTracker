using System.Data.Entity;

namespace IssueTrackerCommon.Repositories
{
    public class RepositoryManager : RepositoryManagerBase
    {
        DbContext _context;

        public RepositoryManager(DbContext context)
        {
            _context = context;
        }

        public override void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch {
            }
        }

        protected override IRepository<T> CreateRepository<T>()
        {
            return new Repository<T>(_context);
        }

        protected override void DisposeResources()
        {
            _context.Dispose();
        }
    }
}

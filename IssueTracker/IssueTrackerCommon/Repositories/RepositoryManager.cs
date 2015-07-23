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
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine("DbEntityValidationException");

                foreach (var error in e.EntityValidationErrors)
                {
                    foreach (var validationError in error.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine(validationError.PropertyName + ": " +
                            validationError.ErrorMessage);
                    }
                }
#endif
                throw e;
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


namespace IssueTrackerCommon.Repositories
{
    public class MockRepositoryManager : RepositoryManagerBase
    {
        protected override IRepository<T> CreateRepository<T>()
        {
            return new MockRepository<T>();
        }

        public override void SaveChanges()
        {
        }

        protected override void DisposeResources()
        {
        }
    }
}

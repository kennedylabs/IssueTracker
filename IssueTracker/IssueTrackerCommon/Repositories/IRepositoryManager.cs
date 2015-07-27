using System;

namespace IssueTrackerCommon.Repositories
{
    public interface IRepositoryManager : IDisposable
    {
        IRepository<T> GetOrCreateRepository<T>() where T : class;
        void SaveChanges();
    }
}

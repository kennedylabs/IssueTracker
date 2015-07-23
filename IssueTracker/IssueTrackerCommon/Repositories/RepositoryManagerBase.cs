using IssueTrackerCommon.Infrastructure;
using System;
using System.Collections.Concurrent;

namespace IssueTrackerCommon.Repositories
{
    public abstract class RepositoryManagerBase : DisposableBase, IRepositoryManager
    {
        ConcurrentDictionary<Type, object> _repositories =
            new ConcurrentDictionary<Type, object>();

        public IRepository<T> GetOrCreateRepository<T>() where T : class
        {
            return (IRepository<T>)_repositories.GetOrAdd(typeof(T), t => CreateRepository<T>());
        }

        public abstract void SaveChanges();

        protected abstract IRepository<T> CreateRepository<T>() where T : class;
    }
}

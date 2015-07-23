using IssueTrackerCommon.Infrastructure;
using IssueTrackerCommon.Repositories;
using System;

namespace IssueTrackerCommon.Services
{
    public class ServiceFactory : DisposableBase, IServiceFactory
    {
        Lazy<IRepositoryManager> _repositoryManager;

        public ServiceFactory(Func<IRepositoryManager> repositoryManagerCreator)
        {
            _repositoryManager = new Lazy<IRepositoryManager>(repositoryManagerCreator);
        }

        public T CreateService<T>() where T : IService
        {
            var service = Activator.CreateInstance<T>();
            
            if (service != null)
            {
                service.RepositoryManager = _repositoryManager.Value;
                service.ServiceFactory = this;
            }

            return service;
        }

        protected override void DisposeResources()
        {
            if (_repositoryManager.IsValueCreated)
                _repositoryManager.Value.Dispose();
        }
    }
}

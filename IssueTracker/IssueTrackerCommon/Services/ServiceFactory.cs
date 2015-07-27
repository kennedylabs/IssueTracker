using IssueTrackerCommon.Infrastructure;
using IssueTrackerCommon.Mapping;
using IssueTrackerCommon.Repositories;
using System;

namespace IssueTrackerCommon.Services
{
    public class ServiceFactory : DisposableBase, IServiceFactory
    {
        Lazy<IMapperManager> _mapperManager;
        Lazy<IRepositoryManager> _repositoryManager;

        public ServiceFactory(Func<IMapperManager> mapperManagerCreator,
            Func<IRepositoryManager> repositoryManagerCreator)
        {
            _mapperManager = new Lazy<Mapping.IMapperManager>(mapperManagerCreator);
            _repositoryManager = new Lazy<IRepositoryManager>(repositoryManagerCreator);
        }

        public T CreateService<T>() where T : IService
        {
            var service = Activator.CreateInstance<T>();
            
            if (service != null)
            {
                service.MapperManager = _mapperManager.Value;
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

using Microsoft.Practices.Unity;
using System;
using IssueTrackerCommon.Services;
using IssueTrackerCommon.Repositories;
using IssueTrackerEntities;
using IssueTrackerCommon.Mapping;
using IssueTrackerDomain.Mapping;

namespace IssueTrackerWeb
{
    public class UnityConfig
    {
        static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(CreateContainer);

        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }

        public static IServiceFactory CreateServiceFactory()
        {
            return container.Value.Resolve<IServiceFactory>();
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IMapperManager, IssueTrackerMapperManager>(
                new InjectionFactory(c => new IssueTrackerMapperManager()));
            container.RegisterType<IRepositoryManager, RepositoryManager>(
                new InjectionFactory(c => new RepositoryManager(new IssueTrackerContext())));
            container.RegisterType<IServiceFactory, ServiceFactory>(
                new InjectionFactory(c => new ServiceFactory(
                    () => container.Resolve<IMapperManager>(),
                    () => container.Resolve<IRepositoryManager>())));
        }

        static IUnityContainer CreateContainer()
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        }
    }
}

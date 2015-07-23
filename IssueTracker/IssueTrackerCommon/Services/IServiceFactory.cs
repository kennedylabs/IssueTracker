using System;

namespace IssueTrackerCommon.Services
{
    public interface IServiceFactory : IDisposable
    {
        T CreateService<T>() where T : IService;
    }
}

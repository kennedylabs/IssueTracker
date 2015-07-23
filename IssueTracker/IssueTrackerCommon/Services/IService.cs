using IssueTrackerCommon.Repositories;

namespace IssueTrackerCommon.Services
{
    public interface IService
    {
        IRepositoryManager RepositoryManager { get; set; }
        IServiceFactory ServiceFactory { get; set; }
    }
}

using IssueTrackerCommon.Mapping;
using IssueTrackerCommon.Repositories;

namespace IssueTrackerCommon.Services
{
    public interface IService
    {
        IMapperManager MapperManager { get; set; }
        IRepositoryManager RepositoryManager { get; set; }
        IServiceFactory ServiceFactory { get; set; }
    }
}

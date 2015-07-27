using IssueTrackerCommon.Mapping;
using System.Collections.Generic;

namespace IssueTrackerDomain.Mapping
{
    public class IssueTrackerMapperManager : MapperManagerBase
    {
        protected override IEnumerable<IMapper> GetMappers()
        {
            yield return new UserMapper();
        }
    }
}

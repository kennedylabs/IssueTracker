
namespace IssueTrackerCommon.Mapping
{
    public interface IMapperManager
    {
        T Map<T>(object source) where T : new();
        void Map(object source, object target);
    }
}

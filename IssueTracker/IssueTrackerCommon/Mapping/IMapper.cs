using System;

namespace IssueTrackerCommon.Mapping
{
    public interface IMapper
    {
        IMapperManager Manager { get; set; }

        bool CanMap(Type sourceType, Type targetType);

        T Map<T>(object source) where T : new();


        void Map(object source, object target);
    }
}

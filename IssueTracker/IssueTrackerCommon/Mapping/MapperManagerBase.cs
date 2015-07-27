using System;
using System.Collections.Generic;
using System.Linq;

namespace IssueTrackerCommon.Mapping
{
    public abstract class MapperManagerBase : IMapperManager
    {
        Lazy<List<IMapper>> _mappers;

        internal static IMapperManager Instance { get; set; }

        public MapperManagerBase()
        {
            _mappers = new Lazy<List<IMapper>>(() => GetMappers().ToList());
        }

        public T Map<T>(object source) where T : new()
        {
            var mapper = _mappers.Value.FirstOrDefault(
                m => m.CanMap(source.GetType(), typeof(T)));

            return mapper != null ? mapper.Map<T>(source) : default(T);
        }

        public void Map(object source, object target)
        {
            var mapper = _mappers.Value.FirstOrDefault(
                m => m.CanMap(source.GetType(), target.GetType()));

            if (mapper != null) mapper.Map(source, target);
        }

        protected abstract IEnumerable<IMapper> GetMappers();

        IEnumerable<IMapper> CreateMappers()
        {
            foreach (var mapper in GetMappers())
            {
                mapper.Manager = this;
                yield return mapper;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IssueTrackerCommon.Mapping
{
    public abstract class MapperBase : IMapper
    {
        Lazy<IList<Tuple<Type, Type, Action<object, object>>>> _mapActions;

        public IMapperManager Manager { get; set; }

        public MapperBase()
        {
            _mapActions =
                new Lazy<IList<Tuple<Type, Type, Action<object, object>>>>(GetMapActions);
        }

        public bool CanMap(Type sourceType, Type targetType)
        {
            if (sourceType == null || targetType == null) return false;

            return _mapActions.Value.FirstOrDefault(
                ma => ma.Item1 == sourceType && ma.Item2 == targetType) != null;
        }

        public T Map<T>(object source) where T : new()
        {
            var target = new T();
            Map(source, target);
            return target;
        }

        public void Map(object source, object target)
        {
            if (source == null || target == null) return;

            var mapAction = _mapActions.Value.FirstOrDefault(
                ma => ma.Item1 == source.GetType() && ma.Item2 == target.GetType());

            if (mapAction != null) mapAction.Item3(source, target);
        }

        List<Tuple<Type, Type, Action<object, object>>> GetMapActions()
        {
            var mapActions = new List<Tuple<Type, Type, Action<object, object>>>();

            foreach (var method in GetType().GetMethods().Where(m => m.Name.StartsWith("Map")))
            {
                var pararmeters = method.GetParameters();

                if (pararmeters.Length == 2)
                {
                    var sourceType = pararmeters[0].ParameterType;
                    var targetType = pararmeters[1].ParameterType;
                    Action<object, object> mapAction =
                        (object s, object t) => TryInvoke(method, s, t);

                    mapActions.Add(Tuple.Create(sourceType, targetType, mapAction));
                }
            }

            return mapActions;
        }

        void TryInvoke(MethodInfo method, object source, object target)
        {
            try
            {
                method.Invoke(this, new object[] { source, target });
            }
            catch
            {
            }
        }
    }
}

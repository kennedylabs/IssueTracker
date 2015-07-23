using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace IssueTrackerCommon.Repositories
{
    public class MockRepository<T> : IRepository<T> where T : class
    {
        int _nextId;
        List<T> _list = new List<T>();

        public IQueryable<T> Find()
        {
            return _list.AsQueryable();
        }

        public T Find(object id)
        {
            if (id == (object)0) return null;

            try { return _list.FirstOrDefault(e => GetIdOrZero(e) == (int)id); }
            catch { return null; }
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _list.Where(predicate.Compile()).AsQueryable();
        }

        public void Insert(T entity)
        {
            if (!_list.Contains(entity))
                _list.Add(TrySetNextId(entity));
        }

        public void Update(T entity)
        {
        }

        public void Delete(object id)
        {
            Delete(id: id);
        }

        public void Delete(T entity)
        {
            Delete(entity: entity);
        }

        static int GetIdOrZero(T entity)
        {
            try { return (int)entity.GetType().GetProperty("Id").GetValue(entity); }
            catch { return 0; }
        }

        T TrySetNextId(T entity)
        {
            try { entity.GetType().GetProperty("Id").SetValue(entity, ++_nextId); }
            catch { }

            return entity;
        }

        void Delete(T entity = null, object id = null)
        {
            var intId = id is int ? (int)id : entity != null ? GetIdOrZero(entity) : 0;
            var matchedEntity = _list.FirstOrDefault(e => e != entity && GetIdOrZero(e) == intId);

            if (entity != null && _list.Contains(entity))
                _list.Remove(entity);

            if (matchedEntity != null && _list.Contains(matchedEntity))
                _list.Remove(matchedEntity);
        }
    }
}

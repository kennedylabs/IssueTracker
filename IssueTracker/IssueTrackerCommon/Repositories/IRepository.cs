using System;
using System.Linq;
using System.Linq.Expressions;

namespace IssueTrackerCommon.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Find();
        T Find(object id);
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
        void Insert(T entity);
        void Update(T entity);
        void Delete(object id);
        void Delete(T entity);
    }
}

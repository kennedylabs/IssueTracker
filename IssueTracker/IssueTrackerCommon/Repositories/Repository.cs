using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace IssueTrackerCommon.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        DbContext _context;
        DbSet<T> _set;

        public Repository(DbContext context)
        {
            _context = context;
            _set = context.Set<T>();
        }

        public IQueryable<T> Find()
        {
            return _set.AsQueryable();
        }

        public T Find(object id)
        {
            return _set.Find(id);
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _set.Where(predicate);
        }

        public void Insert(T entity)
        {
            if (entity != null)
            {
                _set.Add(entity);
                _context.Entry(entity).State = EntityState.Added;
            }
        }

        public void Update(T entity)
        {
            _set.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            T entity = Find(id);
            if (entity != null)
                _set.Remove(entity);
        }

        public void Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
                _set.Attach(entity);
            _set.Remove(entity);
        }
    }
}

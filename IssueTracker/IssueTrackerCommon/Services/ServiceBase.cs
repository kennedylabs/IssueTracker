using IssueTrackerCommon.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace IssueTrackerCommon.Services
{
    public abstract class ServiceBase : IService
    {
        public IRepositoryManager RepositoryManager { get; set; }
        public IServiceFactory ServiceFactory { get; set; }

        protected IRepository<T> Repo<T>() where T : class
        {
            return RepositoryManager.GetOrCreateRepository<T>();
        }

        protected T Service<T>() where T : IService
        {
            return ServiceFactory.CreateService<T>();
        }

        protected void SaveChanges()
        {
            RepositoryManager.SaveChanges();
        }

        protected V FindSingleValue<T, V>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, V>> selector) where T : class
        {
            return Repo<T>().Find(predicate).Select(selector).FirstOrDefault();
        }

        protected T FindSingleOrNull<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return Repo<T>().Find(predicate).FirstOrDefault();
        }

        protected T EnsureFindSingle<T>(int id) where T : class
        {
            var entity = Repo<T>().Find(id);
            ThrowNotFoundIfNull(entity, id);
            return entity;
        }

        protected T EnsureFindSingle<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            var entity = FindSingleOrNull(predicate);
            ThrowNotFoundIfNull(entity);
            return entity;
        }

        protected T EnsureFindSingle<T>(ICollection<T> entities) where T : class
        {
            var entity = entities.FirstOrDefault();
            ThrowUnexpectedIfNull(entity);
            return entity;
        }

        protected T InsertAndSave<T>(T entity) where T : class
        {
            Repo<T>().Insert(entity);
            SaveChanges();

            return entity;
        }

        protected T UpdateAndSave<T>(T entity) where T : class
        {
            Repo<T>().Update(entity);
            SaveChanges();

            return entity;
        }

        protected void DeleteAndSave<T>(int id) where T : class
        {
            Repo<T>().Delete(id);
            SaveChanges();
        }

        protected void DeleteAndSave<T>(T entity) where T : class
        {
            Repo<T>().Delete(entity);
            SaveChanges();
        }

        protected void ThrowUnexpected(string message = null, Exception innerException = null)
        {
            throw new ServiceException(ServiceExceptionType.Unexpected, message, innerException);
        }

        protected void ThrowUnexpectedIfNull(object obj,
            string message = null, Exception innerException = null)
        {
            if (obj == null)
                throw new ServiceException(ServiceExceptionType.Unexpected,
                    message, innerException);
        }

        protected void ThrowBadDataIf(bool shouldThrow,
            string message = null, Exception innerException = null)
        {
            if (shouldThrow)
                throw new ServiceException(ServiceExceptionType.BadData, message, innerException);
        }

        protected void ThrowNotFoundIfNull<T>(T entity)
        {
            if (entity == null)
            {
                var message = "Can't find item of type " + typeof(T).Name + ".";
                throw new ServiceException(ServiceExceptionType.NotFound, message);
            }
        }

        protected void ThrowNotFoundIfNull<T>(T entity, int id)
        {
            if (entity == null)
            {
                var message = "Can't find item of type " + typeof(T).Name + " for id " + id + ".";
                throw new ServiceException(ServiceExceptionType.NotFound, message);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using IMMRequest.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;

namespace IMMRequest.DataAccess
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        protected DbContext Context { get; set; }
        protected readonly DbSet<T> dbSetBase;

        public void Add(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public void Remove(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public abstract IEnumerable<T> GetAll();

        public abstract T Get(Guid id);

        public abstract IEnumerable<T> Query(string query);

        public abstract bool Exist(Func<T, bool> predicate);

        public void Save()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (System.Exception)
            {
                //TODO throw exception.
                throw;
            }
        }
    }
}

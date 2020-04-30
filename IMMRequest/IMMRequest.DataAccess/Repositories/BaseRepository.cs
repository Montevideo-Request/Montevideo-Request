using IMMRequest.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace IMMRequest.DataAccess
{
    public abstract class BaseRepository<T, X> : IRepository<T, X> where T : class where X : class
    {
        protected DbContext Context { get; set; }
        protected readonly DbSet<T> dbSetBase;

        public void Add(T entity)
        {
            try
            {
                Context.Set<T>().Add(entity);    
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            
        }

        public void Remove(T entity)
        {
            try
            {
             Context.Set<T>().Remove(entity);   
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public void Update(T entity)
        {
            try
            {
                Context.Entry(entity).State = EntityState.Modified;    
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }            
        }

        public abstract IEnumerable<T> GetAll();

        public abstract T Get(Guid id);

        public abstract X GetParent(Guid id);

        public abstract IEnumerable<T> Query(string query);

        public abstract bool Exist(Func<T, bool> predicate);

        public void Save()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}

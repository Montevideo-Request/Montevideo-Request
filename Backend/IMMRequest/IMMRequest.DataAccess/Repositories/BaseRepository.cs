using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using IMMRequest.Exceptions;
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
                throw new ExceptionController(DataAccessExceptions.GENERIC_ELEMENT_ALREADY_EXISTS);
            }
            
        }

        // public void Remove(T entity)
        // {
        //     try
        //     {
        //      Context.Set<T>().Remove(entity);   
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         throw new ExceptionController(DataAccessExceptions.GENERIC_ELEMENT_ALREADY_EXISTS);
        //     }
        // }

        public void Update(T entity)
        {
            try
            {
                Context.Entry(entity).State = EntityState.Modified;    
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new ExceptionController(DataAccessExceptions.GENERIC_ELEMENT_ALREADY_EXISTS);
            }            
        }

        public abstract void Remove(T entity);
        
        public abstract IEnumerable<T> GetAll();

        public abstract T Get(Guid id);

        public abstract X GetParent(Guid id);

        public abstract bool Exist(T entity);

        public abstract bool NameExists(T entity);

        public void Save()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new ExceptionController(DataAccessExceptions.GENERIC_ELEMENT_ALREADY_EXISTS);
            }
        }
    }
}

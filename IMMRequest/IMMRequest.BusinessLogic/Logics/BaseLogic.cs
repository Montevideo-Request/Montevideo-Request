using IMMRequest.BusinessLogic.Interface;
using IMMRequest.DataAccess.Interface;
using IMMRequest.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System;

namespace IMMRequest.BusinessLogic 
{
    public abstract class BaseLogic<T> : ILogic<T> where T : class
    {
        protected IRepository<T> repository { get; set; }
        public abstract void Update(T entity);
        public T Create(T entity)
        {
            try
            {
                this.repository.Add(entity);
                this.repository.Save();
                return entity;
            }
            catch {
                throw new ExceptionController(ExceptionMessage.INVALID_ID);
            }
        }
        public T Get(Guid id)
        {
            try
            {
                return this.repository.Get(id);
            }
            catch 
            {
                throw new ExceptionController(ExceptionMessage.INVALID_ID);
            }
        }

        public IEnumerable<T> GetAll()
        {
            IEnumerable<T> entities = this.repository.GetAll();
            
            if (entities.Count() == 0) 
            {
                throw new ExceptionController(ExceptionMessage.NO_ELEMENTS_FOUND);
            }

            return entities;
		}

        public void Remove(T entity)
        {
            try
            {
                this.repository.Remove(entity);
                this.repository.Save();
            }
            catch
            {
                throw new ExceptionController(ExceptionMessage.INVALID_ID);
            }
        }

        public void Save()
        {
            this.repository.Save();
        }

    }
}

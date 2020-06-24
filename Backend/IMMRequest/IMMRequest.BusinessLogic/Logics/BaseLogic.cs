using System.Collections.Generic;
using IMMRequest.DataAccess;
using IMMRequest.Exceptions;
using System.Linq;
using System;

namespace IMMRequest.BusinessLogic 
{
    public abstract class BaseLogic<T, X> : ILogic<T> where T : class where X : class
    {
        protected IRepository<T, X> repository { get; set; }

        public abstract T Update(T entity);
        
        public abstract void Remove(Guid id);

        public abstract void IsValid(T entity);
        
        public abstract void EntityExist(T entity);

        public abstract void NotExist(Guid id);

        public abstract bool Exists(Guid id);

        public T Create(T entity)
        {
            IsValid(entity);
            this.repository.Add(entity);
            this.repository.Save();
            return entity;
        }

        public T Get(Guid id)
        {
            NotExist(id);
            return this.repository.Get(id);
        }

        public IEnumerable<T> GetAll()
        {
            IEnumerable<T> entities = this.repository.GetAll();
            
            if (entities.Count() == 0) 
            {
                throw new ExceptionController(LogicExceptions.GENERIC_NO_ELEMENTS);
            }

            return entities;
		}

        public void Save()
        {
            this.repository.Save();
        }

    }
}

using System;
using System.Collections.Generic;
using IMMRequest.DataAccess.Interface;
using IMMRequest.BusinessLogic.Interface;
using System.Linq;

namespace IMMRequest.BusinessLogic 
{
    public abstract class BaseLogic<T> : ILogic<T> where T : class
    {
        public IRepository<T> repository;

        public abstract void Update(T entity);

        public void Add(T entity)
        {
            try
            {
                this.repository.Add(entity);
            }
            catch {
                throw new ArgumentException("Invalid guid");
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
                throw new ArgumentException("Invalid Id");
            }
        }

        public IEnumerable<T> GetAll()
        {
            IEnumerable<T> entities = this.repository.GetAll();
            
            if (entities.Count() == 0) 
            {
                throw new ArgumentException("There are no Entities");
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
                throw new ArgumentException("Invalid guid");
            }
        }

        public void Save()
        {
            this.repository.Save();
        }

    }
}
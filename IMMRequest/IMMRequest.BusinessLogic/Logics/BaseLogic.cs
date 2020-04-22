using IMMRequest.BusinessLogic.Interface;
using IMMRequest.DataAccess.Interface;
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

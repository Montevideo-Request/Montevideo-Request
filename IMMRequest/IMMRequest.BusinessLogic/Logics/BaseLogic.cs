using System;
using System.Collections.Generic;
using IMMRequest.DataAccess.Interface;
using IMMRequest.BusinessLogic.Interface;

namespace IMMRequest.BusinessLogic 
{
    public abstract class BaseLogic<T> : ILogic<T> where T : class
    {
        public IRepository<T> repository;

        public void Add(T entity)
        {
            this.repository.Add(entity);
            this.repository.Save();
        }

        public T Get(Guid id)
        {
            return this.repository.Get(id);
        }

        public IEnumerable<T> GetAll()
        {
            return this.repository.GetAll();
        }

        public void Remove(T entity)
        {
            this.repository.Remove(entity);
            this.repository.Save();
        }

        public void Save()
        {
            this.repository.Save();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
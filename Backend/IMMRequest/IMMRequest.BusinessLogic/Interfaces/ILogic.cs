using System;
using System.Collections.Generic;
using IMMRequest.Domain;

namespace IMMRequest.BusinessLogic
{
    public interface ILogic<T>
    {
        T Create(T entity);
        
        void Save();
        
        void Remove(Guid id);

        T Update(T entity);

        T Get(Guid id);

        bool Exists(Guid id);

        IEnumerable<T> GetAll();
    }
}

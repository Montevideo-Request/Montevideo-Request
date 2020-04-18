using System;
using System.Collections.Generic;

namespace IMMRequest.BusinessLogic.Interface
{
    public interface ILogic<T>
    {
        void Add(T entity);

        void Save();

        void Remove(T entity);

        void Update(T entity);

        T Get(Guid id);

        IEnumerable<T> GetAll();
    }
}
using System.Collections.Generic;
using System;

namespace IMMRequest.DataAccess
{
    public interface IAreaRepository<T> 
    {
        void Add(T entity);

        void Remove(T entity);

        void Update(T entity);

        IEnumerable<T> GetAll();

        T Get(Guid id);

        void Save();

        bool Exist(T entity);
        
        bool NameExists(T entity);
    }
}

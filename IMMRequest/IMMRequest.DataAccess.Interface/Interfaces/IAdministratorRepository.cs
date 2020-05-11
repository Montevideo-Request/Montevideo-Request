using System.Collections.Generic;
using System;

namespace IMMRequest.DataAccess.Interface
{
    public interface IAdministratorRepository<T>
    {
        void Add(T entity);
        void Remove(T entity);
        void Update(T entity);
        IEnumerable<T> GetAll();
        T Get(Guid id);
        void Save();
        bool Exist(T entity);
        bool Exist(Guid id);
        bool TokenExists(Guid token);
        T GetByCredentials(Func<T, bool> predicate);
    }
}

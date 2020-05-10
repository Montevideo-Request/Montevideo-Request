using System.Collections.Generic;
using System;

namespace IMMRequest.BusinessLogic.Interface
{
    public interface IAdministratorLogic<T>
    {
        T Create(T entity);
        void Save();
        T Update(T entity);
        T Get(Guid id);
        IEnumerable<T> GetAll();
        void Remove(Guid id);
    }
}

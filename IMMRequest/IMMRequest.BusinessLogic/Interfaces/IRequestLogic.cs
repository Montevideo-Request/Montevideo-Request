using System;
using System.Collections.Generic;
using IMMRequest.Domain;

namespace IMMRequest.BusinessLogic
{
    public interface IRequestLogic<T, X>
    {
        T Create(T entity);
        void Save();
        T Update(T entity);
        T Get(Guid id);
        X GetTypeWithFields(Guid id);
        IEnumerable<T> GetAll();
    }
}

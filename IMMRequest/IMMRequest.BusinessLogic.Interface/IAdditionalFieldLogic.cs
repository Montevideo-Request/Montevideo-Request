using System;
using System.Collections.Generic;
using IMMRequest.Domain;

namespace IMMRequest.BusinessLogic.Interface
{
    public interface IAdditionalFieldLogic<T, X>
    {
        T Create(T entity);
        void Save();
        void Remove(T entity);
        void Update(T entity);
        void EntityExist(T entity);
        void NotExist(Guid id);
        T Get(Guid id);
        IEnumerable<T> GetAll();
        X AddFieldRange(Guid id, X entity);
    }
}

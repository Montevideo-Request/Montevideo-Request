using System.Collections.Generic;
using System;

namespace IMMRequest.BusinessLogic
{
    public interface IAdditionalFieldLogic<T, X>
    {
        T Create(T entity);
        void Save();
        void Remove(Guid id);
        T Update(T entity);
        void EntityExist(T entity);
        void NotExist(Guid id);
        T Get(Guid id);
        IEnumerable<T> GetAll();
        IEnumerable<X> GetAllRanges(Guid id);
        X AddFieldRange(Guid id, X entity);
    }
}

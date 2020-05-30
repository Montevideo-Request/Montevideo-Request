using System.Collections.Generic;
using System;

namespace IMMRequest.DataAccess
{
    public interface IRequestRepository<T, X> where T : class where X : class
    {
        void Add(T entity);

        void Update(T entity);

        IEnumerable<T> GetAll();

        T Get(Guid id);

        X GetTypeWithFields(Guid id);

        void Save();

        bool Exist(Func<T, bool> predicate);

        bool Exist(T entity);
    }
}

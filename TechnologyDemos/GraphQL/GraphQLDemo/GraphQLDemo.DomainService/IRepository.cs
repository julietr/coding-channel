using GraphQLDemo.Domain;
using System;
using System.Collections.Generic;

namespace GraphQLDemo.DomainService
{
    public interface IRepository<T>
        where T : DomainObject
    {
        IEnumerable<T> Find(Func<T, bool> predicate);
        IEnumerable<T> FindAll();
        T Add(T item);
        T Retrieve(int id);
        bool Update(T item);
        bool Delete(int id);
    }
}

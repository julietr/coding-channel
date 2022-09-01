using GraphQLDemo.Domain;
using GraphQLDemo.DomainService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphQLDemo.ApplicationService
{
    public class Repository<T> : IRepository<T>
        where T : DomainObject
    {
        static object _locker = new object();
        static int _id = 1;
        static Dictionary<int, T> _items = new Dictionary<int, T>();

        public T Add(T item)
        {
            lock (_locker)
            {
                if (item.Id == 0)
                {
                    item.Id = _id++;
                    item.CreatedOn = DateTime.Now;
                    item.ModifiedOn = DateTime.Now;
                }
                else
                {
                    _id = Math.Max(_id, item.Id + 1);
                }

                _items.Add(item.Id, item);
                return item;
            }
        }

        public bool Delete(int id)
        {
            lock (_locker)
            {
                return _items.Remove(id);
            }
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            lock (_locker)
            {
                return _items.Values.Where(predicate).ToArray();
            }
        }

        public IEnumerable<T> FindAll()
        {
            lock (_locker)
            {
                return _items.Values.ToArray();
            }
        }

        public T Retrieve(int id)
        {
            lock (_locker)
            {
                if (_items.TryGetValue(id, out var item))
                    return item;
                return null;
            }
        }

        public bool Update(T item)
        {
            lock (_locker)
            {
                if (_items.ContainsKey(item.Id))
                {
                    item.ModifiedOn = DateTime.Now;
                    _items[item.Id] = item;
                    return true;
                }
                return false;
            }
        }
    }
}

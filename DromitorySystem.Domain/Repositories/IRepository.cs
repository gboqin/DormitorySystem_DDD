using DromitorySystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DromitorySystem.Domain.Repositories
{
    public interface IRepository<TAggregateRoot,TKey>
        where TAggregateRoot:class,IAggregateRoot<TKey>
    {
        void Add(TAggregateRoot aggregateRoot);
        void Remove(TAggregateRoot aggregateRoot);
        void Update(TAggregateRoot aggregateRoot);
        IQueryable<TAggregateRoot> GetAll();
        TAggregateRoot GetByKey(long key);
    }
}

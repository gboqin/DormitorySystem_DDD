using DormitorySystem.Infrastructure.Helpers;
using DromitorySystem.Domain;
using DromitorySystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitorySystem.Repositories.EntityFramework
{
    public abstract class EntityFrameworkRepository<TAgreegateRoot,TKey> : IRepository<TAgreegateRoot,TKey>
        where TAgreegateRoot : class, IAggregateRoot<TKey>
    {
        private readonly IEntityFrameworkRepositoryContext _efContext;

        protected EntityFrameworkRepository(IRepositoryContext context)
        {
            var efContext = context as IEntityFrameworkRepositoryContext;
            if (efContext != null)
                this._efContext = efContext;
        }

        protected IEntityFrameworkRepositoryContext EfContext
        {
            get { return this._efContext; }
        }

        public void Add(TAgreegateRoot aggregateRoot)
        {
            _efContext.RegisterNew<TAgreegateRoot,TKey>(aggregateRoot);
            _efContext.Commit();
        }

        public void Remove(TAgreegateRoot aggregateRoot)
        {
            _efContext.RegisterDeleted<TAgreegateRoot, TKey>(aggregateRoot);
            _efContext.Commit();
        }

        public void Update(TAgreegateRoot aggregateRoot)
        {
            _efContext.RegisterModified<TAgreegateRoot, TKey>(aggregateRoot);
            _efContext.Commit();
        }

        public IQueryable<TAgreegateRoot> GetAll()
        {
            return _efContext.DbContext.Set<TAgreegateRoot>();
        }

        public TAgreegateRoot GetByKey(long key)
        {          
            return _efContext.DbContext.Set<TAgreegateRoot>().Find(key);
        }

    }
}

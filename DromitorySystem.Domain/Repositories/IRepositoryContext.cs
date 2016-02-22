using DormitorySystem.Infrastructure;
using DromitorySystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DromitorySystem.Domain.Repositories
{
    public interface IRepositoryContext:IUnitOfWork
    {
        /// <summary>
        ///   注册一个新的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        void RegisterNew<TAggregateRoot,TKey>(TAggregateRoot entity) where TAggregateRoot : class, IAggregateRoot<TKey>;

        /// <summary>
        ///   批量注册多个新的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entities"> 要注册的对象集合 </param>
        void RegisterNew<TAgreegateRoot, TKey>(IEnumerable<TAgreegateRoot> entities) where TAgreegateRoot : class,IAggregateRoot<TKey>;

        /// <summary>
        ///   注册一个更改的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        void RegisterModified<TAgreegateRoot, TKey>(TAgreegateRoot entity) where TAgreegateRoot : class,IAggregateRoot<TKey>;

        /// <summary>
        ///   注册一个删除的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        void RegisterDeleted<TAgreegateRoot, TKey>(TAgreegateRoot entity) where TAgreegateRoot : class,IAggregateRoot<TKey>;

        /// <summary>
        ///   批量注册多个删除的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entities"> 要注册的对象集合 </param>
        void RegisterDeleted<TAgreegateRoot, TKey>(IEnumerable<TAgreegateRoot> entities) where TAgreegateRoot : class,IAggregateRoot<TKey>;
    }
}

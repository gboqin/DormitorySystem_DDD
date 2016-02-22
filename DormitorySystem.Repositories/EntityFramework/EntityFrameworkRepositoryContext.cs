using DromitorySystem.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DromitorySystem.Domain.Repositories;
using DormitorySystem.Infrastructure.Helpers;

namespace DormitorySystem.Repositories.EntityFramework
{
    public class EntityFrameworkRepositoryContext:IEntityFrameworkRepositoryContext
    {
        // ThreadLocal代表线程本地存储，主要相当于一个静态变量
        // 但静态变量在多线程访问时需要显式使用线程同步技术。
        // 使用ThreadLocal变量，每个线程都会一个拷贝，从而避免了线程同步带来的性能开销

        private readonly ThreadLocal<DromityDbContext> _localCtx = new ThreadLocal<DromityDbContext>(() => new DromityDbContext());
        public DromityDbContext DbContext
        {
            get { return _localCtx.Value; }
        }

        #region IRepositoryContext Members

        /// <summary>
        ///     获取 当前单元操作是否已被提交
        /// </summary>
        public bool Committed { get; private set; }

        /// <summary>
        ///     提交当前单元操作的结果
        /// </summary>
        /// <returns></returns>
        public int Commit()
        {
            if (Committed)
            {
                return 0;
            }
            try
            {
                int result = _localCtx.Value.SaveChanges();
                Committed = true;
                return result;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null && e.InnerException.InnerException is SqlException)
                {
                    SqlException sqlEx = e.InnerException.InnerException as SqlException;
                    string msg = DataHelper.GetSqlExceptionMessage(sqlEx.Number);
                    throw PublicHelper.ThrowDataAccessException("提交数据更新时发生异常：" + msg, sqlEx);
                }
                throw;
            }
        }

        /// <summary>
        ///     把当前单元操作回滚成未提交状态
        /// </summary>
        public void Rollback()
        {
            Committed = false;
        }

        public void Dispose()
        {
            if (!Committed)
            {
                Commit();
            }
            _localCtx.Value.Dispose();
        }

        /// <summary>
        ///     注册一个新的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        public void RegisterNew<TAgreegateRoot,TKey>(TAgreegateRoot entity) where TAgreegateRoot : class,IAggregateRoot<TKey>
        {
            EntityState state = _localCtx.Value.Entry(entity).State;
            if (state == EntityState.Detached)
            {
                _localCtx.Value.Entry(entity).State = EntityState.Added;
            }
            Committed = false;
        }

        /// <summary>
        ///     批量注册多个新的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entities"> 要注册的对象集合 </param>
        public void RegisterNew<TAgreegateRoot, TKey>(IEnumerable<TAgreegateRoot> entities) where TAgreegateRoot : class,IAggregateRoot<TKey>
        {
            try
            {
                _localCtx.Value.Configuration.AutoDetectChangesEnabled = false;
                foreach (TAgreegateRoot entity in entities)
                {
                    RegisterNew<TAgreegateRoot,TKey>(entity);
                }
            }
            finally
            {
                _localCtx.Value.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        /// <summary>
        ///     注册一个更改的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        public void RegisterModified<TAgreegateRoot, TKey>(TAgreegateRoot entity) where TAgreegateRoot:class,IAggregateRoot<TKey>
        {
            if (_localCtx.Value.Entry(entity).State == EntityState.Detached)
            {
                _localCtx.Value.Set<TAgreegateRoot>().Attach(entity);
            }
            _localCtx.Value.Entry(entity).State = EntityState.Modified;
            Committed = false;
        }

        /// <summary>
        ///   注册一个删除的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entity"> 要注册的对象 </param>
        public void RegisterDeleted<TAgreegateRoot, TKey>(TAgreegateRoot entity) where TAgreegateRoot : class,IAggregateRoot<TKey>
        {
            _localCtx.Value.Entry(entity).State = EntityState.Deleted;
            Committed = false;
        }

        /// <summary>
        ///   批量注册多个删除的对象到仓储上下文中
        /// </summary>
        /// <typeparam name="TEntity"> 要注册的类型 </typeparam>
        /// <param name="entities"> 要注册的对象集合 </param>
        public void RegisterDeleted<TAgreegateRoot, TKey>(IEnumerable<TAgreegateRoot> entities) where TAgreegateRoot : class,IAggregateRoot<TKey>
        {
            try
            {
                _localCtx.Value.Configuration.AutoDetectChangesEnabled = false;
                foreach (TAgreegateRoot entity in entities)
                {
                    RegisterDeleted<TAgreegateRoot,TKey>(entity);
                }
            }
            finally
            {
                _localCtx.Value.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        #endregion
    }
}

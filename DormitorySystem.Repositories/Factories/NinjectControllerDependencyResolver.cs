using DormitorySystem.Application;
using DormitorySystem.Application.Impl;
using DormitorySystem.Repositories.EntityFramework;
using DromitorySystem.Domain.Repositories;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;

namespace DormitorySystem.Repositories.Factories
{
    public class NinjectControllerDependencyResolver : NinjectDependencyScope, System.Web.Mvc.IDependencyResolver
    {
        private IKernel ninjectKernel;

        public NinjectControllerDependencyResolver(IKernel kernel)
            : base(kernel)
        {
            if (kernel == null)
            {
                throw new ArgumentNullException("kernel");
            }
            this.ninjectKernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyScope(ninjectKernel);
        }

        private void AddBinddings()
        {
            ninjectKernel.Bind<IRepositoryContext>().To<EntityFrameworkRepositoryContext>();

            ninjectKernel.Bind<IUserRepository>().To<UserRepository>();
            ninjectKernel.Bind<IUserService>().To<UserService>();

            ninjectKernel.Bind<ILevelRepository>().To<LevelRepository>();
            ninjectKernel.Bind<ILevelService>().To<LevelService>();

            ninjectKernel.Bind<IDepartmentRepository>().To<DepartmentRepository>();
            ninjectKernel.Bind<IDepartmentService>().To<DepartmentService>();

            ninjectKernel.Bind<IAccountService>().To<AccountService>();
        }
    }
}

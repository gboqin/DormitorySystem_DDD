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
using System.Web.Mvc;

namespace DormitorySystem.Repositories.Factories
{
    public class NinjectControllerFactory: DefaultControllerFactory
    {
        private IKernel ninjectKernel;
        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBinddings();
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBinddings()
        {
            ninjectKernel.Bind<IRepositoryContext>().To<EntityFrameworkRepositoryContext>();

            ninjectKernel.Bind<IUserRepository>().To<UserRepository>();
            ninjectKernel.Bind<IUserService>().To<UserService>();

            ninjectKernel.Bind<ILevelRepository>().To<LevelRepository>();
            ninjectKernel.Bind<ILevelService>().To<LevelService>();

            ninjectKernel.Bind<IAccountService>().To<AccountService>();
        }

    }
}

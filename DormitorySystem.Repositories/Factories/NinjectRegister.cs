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
using System.Web.Http;
using System.Web.Mvc;

namespace DormitorySystem.Repositories.Factories
{
    public class NinjectRegister
    {
        private static readonly IKernel Kernel;
        static NinjectRegister()
        {
            Kernel = new StandardKernel();
            AddBindings();
        }

        public static void RegisterFovMvc()
        {
            DependencyResolver.SetResolver(new NinjectDependencyResolverForMvc(Kernel));
        }

        public static void RegisterFovWebApi(HttpConfiguration config)
        {
            config.DependencyResolver = new NinjectDependencyResolverForWebApi(Kernel);
        }

        private static void AddBindings()
        {
            Kernel.Bind<IRepositoryContext>().To<EntityFrameworkRepositoryContext>();

            Kernel.Bind<IUserRepository>().To<UserRepository>();
            Kernel.Bind<IUserService>().To<UserService>();

            Kernel.Bind<ILevelRepository>().To<LevelRepository>();
            Kernel.Bind<ILevelService>().To<LevelService>();

            Kernel.Bind<IDepartmentRepository>().To<DepartmentRepository>();
            Kernel.Bind<IDepartmentService>().To<DepartmentService>();

            Kernel.Bind<IGoodsRepository>().To<GoodsRepository>();
            Kernel.Bind<IGoodsService>().To<GoodsService>();

            Kernel.Bind<IDormOptiontypeRepository>().To<DormOptiontypeRepository>();
            Kernel.Bind<IDormOptiontypeService>().To<DormOptiontypeService>();

            Kernel.Bind<IDormSettingRepository>().To<DormSettingRepository>();
            Kernel.Bind<IDormSettingService>().To<DormSettingService>();

            Kernel.Bind<IDormitoryRepository>().To<DormitoryRepository>();
            Kernel.Bind<IDormitoryService>().To<DormitoryService>();

            Kernel.Bind<IDormSetRelationRepository>().To<DormSetRelationRepository>();
            Kernel.Bind<IDormSetRelationService>().To<DormSetRelationService>();

            Kernel.Bind<IAccountService>().To<AccountService>();
        }
    }
}

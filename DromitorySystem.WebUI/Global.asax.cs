using DormitorySystem.Repositories.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DromitorySystem.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Code First 生成测试数据库用;
            System.Data.Entity.Database.SetInitializer(new DormitorySystem.Repositories.EntityFramework.DormitoryInitializer());
            //ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
            NinjectRegister.RegisterFovMvc(); //为ASP.NET MVC注册IOC容器
            NinjectRegister.RegisterFovWebApi(GlobalConfiguration.Configuration);//为WebApi注册IOC容器
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}

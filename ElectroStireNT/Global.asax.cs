using BLL.Infrastracture;
using ElectroStireNT.Util;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using ElectroStireNT.App_Start;

namespace ElectroStireNT
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            ServiceModule serviceModule = new ServiceModule("EFdbContext");
            ProductModule orderModule = new ProductModule();
            var kernel = new StandardKernel(serviceModule, orderModule);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));

            //NinjectModule productModule = new ProductModule();
            //NinjectModule serviceModule = new ServiceModule("EFdbContext");
            //var kernel = new StandardKernel(productModule, serviceModule);
            //DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));


        }
    }
}

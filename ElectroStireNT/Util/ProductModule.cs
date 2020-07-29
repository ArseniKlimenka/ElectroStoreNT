using BLL.Interfaces;
using BLL.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace ElectroStireNT.Util
{
    public class ProductModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IOrderService>().To<OrderService>();
            Bind<IProductService>().To<ProductService>();
            Kernel.Unbind<ModelValidatorProvider>();
        }
    }
}
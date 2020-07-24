using DLL.Interfaces;
using DLL.Repositories;
using Ninject.Modules;


namespace BLL.Infrastracture
{
    public class ServiceModule : NinjectModule
    {
        private string connectionString;
        public ServiceModule(string connection)
        {
            connectionString = connection;
        }
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWorkRepository>().WithConstructorArgument(connectionString);
        }
    }
}

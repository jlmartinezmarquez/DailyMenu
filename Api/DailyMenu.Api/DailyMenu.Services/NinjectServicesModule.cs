using Ninject.Extensions.Conventions;
using Ninject.Modules;

namespace DailyMenu.Services
{
    public class NinjectServicesModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind(x => x
                .From(GetType().Assembly)
                .SelectAllClasses().InNamespaceOf(GetType())
                .BindAllInterfaces()
            );
        }
    }
}

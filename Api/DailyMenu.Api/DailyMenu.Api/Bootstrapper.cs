using System.Collections.Generic;
using System.Web.Http;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Modules;

namespace DailyMenu.Api
{
    public enum BootstrapperModeType
    {
        Static,
        Instance
    }

    public class Bootstrapper
    {
        private static StandardKernel _kernel;

        public StandardKernel Kernel
        {
            get { return _kernel; }
        }

        public static BootstrapperModeType Mode { get; set; } = BootstrapperModeType.Instance;

        public IKernel Start(params INinjectModule[] additionalModules)
        {
            if (Mode == BootstrapperModeType.Static && _kernel != null)
                return _kernel;

            _kernel = new StandardKernel(new NinjectSettings { LoadExtensions = true });

            _kernel.Bind(x => x.FromThisAssembly()
                .Select(y => y.BaseType == typeof(ApiController))
                .BindToSelf());

            //_kernel.Bind<DataContext>().ToSelf().InRequestScope();

            var modules = new List<INinjectModule>();


            if (additionalModules != null && additionalModules.Length > 0)
                modules.AddRange(additionalModules);

            _kernel.Load(modules.ToArray());

            return _kernel;
        }
    }
}
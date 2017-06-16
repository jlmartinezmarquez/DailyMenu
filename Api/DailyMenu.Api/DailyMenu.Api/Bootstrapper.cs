using System;
using System.Collections.Generic;
using System.Web.Http;
using DailyMenu.Configuration;
using DailyMenu.Repositories;
using DailyMenu.Services;
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

    public class Bootstrapper : IDisposable
    {
        private StandardKernel _kernel;

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

            var modules = new List<INinjectModule>();

            modules.Add(new NinjectRepositoryModule());

            modules.Add(new NinjectServicesModule());

            modules.Add(new NinjectConfigurationModule());

            if (additionalModules != null && additionalModules.Length > 0)
                modules.AddRange(additionalModules);

            _kernel.Load(modules.ToArray());

            return _kernel;
        }

        public void Dispose()
        {
            _kernel?.Dispose();
        }
    }
}
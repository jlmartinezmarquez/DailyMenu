using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using DailyMenu.Api;
using DailyMenu.Configuration;
using DailyMenu.Lifetime;
using DailyMenu.Repositories;
using DailyMenu.Services;
using Ninject;
using Ninject.Modules;

namespace DailyMenu.Tests.Framework
{
    public sealed class TestsBootstrapper : IDisposable
    {
        private readonly CompositeDisposable _objectsToTearDown = new CompositeDisposable();

        private StandardKernel _kernel;

        public StandardKernel Kernel => _kernel;

        public static Api.BootstrapperModeType Mode { get; set; } = Api.BootstrapperModeType.Instance;

        public IKernel Start(params INinjectModule[] additionalModules)
        {
            if (Mode == Api.BootstrapperModeType.Static && _kernel != null) return _kernel;

            var existingKernel = Startup.Kernel as StandardKernel;

            _kernel = existingKernel ?? new StandardKernel(new NinjectSettings { LoadExtensions = true });

            //_kernel.Bind<DataContext>().ToSelf().InCallScope();

            var modules = new List<INinjectModule>();

            modules.Add(new NinjectConfigurationModule());
            modules.Add(new NinjectRepositoryModule());
            modules.Add(new NinjectServicesModule());

            if (additionalModules != null && additionalModules.Length > 0) modules.AddRange(additionalModules);

            var ninjectModules = modules.ToArray();
            _kernel.Load(ninjectModules);
            if (existingKernel == null)
            {
                _objectsToTearDown.Add(new LoadedKernel(_kernel, ninjectModules));
            }

            return _kernel;
        }

        public void Dispose() => _objectsToTearDown.Clear();
    }
}

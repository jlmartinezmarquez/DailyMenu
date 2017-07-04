using System;
using Ninject;
using Ninject.Modules;

namespace DailyMenu.Lifetime
{
    public sealed class LoadedKernel : IDisposable
    {
        private readonly IKernel _kernel;
        private readonly INinjectModule[] _loadedModules;

        public LoadedKernel(IKernel kernel, params INinjectModule[] loadedModules)
        {
            _kernel = kernel;
            _loadedModules = loadedModules;
        }

        public void Dispose()
        {
            if (_kernel == null) return;

            foreach (var module in _loadedModules)
            {
                _kernel.Unload(module.Name);
            }

            _kernel.Dispose();
        }
    }
}

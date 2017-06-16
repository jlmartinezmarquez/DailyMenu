using System;
using System.Reactive.Disposables;
using Ninject;
using Ninject.Extensions.NamedScope;

namespace DailyMenu.Tests.Framework
{
    public sealed class NinjectResolver : IDisposable
    {
        private readonly IKernel _standardKernel;

        private readonly CompositeDisposable _objectsToTearDown = new CompositeDisposable();

        public NinjectResolver(IKernel kernel = null)
        {
            if (kernel == null)
            {
                var bootstrapper = new TestsBootstrapper();
                bootstrapper.Start();
                _standardKernel = bootstrapper.Kernel;
                _objectsToTearDown.Add(bootstrapper);
            }
            else
            {
                _standardKernel = kernel;
            }
        }

        public T Resolve<T>() => _standardKernel.Get<T>();

        public void Replace<TCurrent, TReplacement>() where TReplacement : TCurrent => _standardKernel.Rebind<TCurrent>().To<TReplacement>();
        public void ReplaceWithConstant<TCurrent, TReplacement>(TReplacement value) where TReplacement : TCurrent => _standardKernel.Rebind<TCurrent>().ToConstant(value);
        public void Replace<TCurrent, TReplacement>(string constructorArgument, object value) where TReplacement : TCurrent => _standardKernel.Rebind<TCurrent>().To<TReplacement>().WithConstructorArgument(constructorArgument, value);


        public NamedScope CreateScope() => CreateScope("TestScope");

        public NamedScope CreateScope(string scopeName) => _standardKernel.CreateNamedScope(scopeName);

        public void Dispose()
        {
            _objectsToTearDown.Clear();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Ninject.Extensions.Interception.Infrastructure;
using Ninject.Parameters;
using Ninject.Syntax;

namespace DailyMenu.Api.Ninject
{
    public class NinjectDependencyScope : DisposableObject, IDependencyScope
    {
        protected IResolutionRoot ResolutionRoot;

        public NinjectDependencyScope(IResolutionRoot resolutionRoot)
        {
            ResolutionRoot = resolutionRoot;
        }

        public object GetService(Type serviceType)
        {
            var request = ResolutionRoot.CreateRequest(serviceType, null, new Parameter[0], true, true);
            return ResolutionRoot.Resolve(request).SingleOrDefault();
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            var request = ResolutionRoot.CreateRequest(serviceType, null, new Parameter[0], true, true);
            return ResolutionRoot.Resolve(request).ToList();
        }
    }
}
using System.Threading;
using System.Web.Http;
using DailyMenu.Api.App_Start;
using DailyMenu.Api.Ninject;
using Microsoft.Owin;
using Ninject;
using Owin;

[assembly: OwinStartup(typeof(DailyMenu.Api.Startup))]
namespace DailyMenu.Api
{
    public class Startup
    {
        private const string HostOnAppDisposingKey = "host.OnAppDisposing";

        public static IKernel Kernel = null;

        public virtual void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            WebApiConfig.Register(config);

            InjectKernel(config, app);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);  //TODO: Restrict this a bit more

            app.UseWebApi(config);
        }

        private void InjectKernel(HttpConfiguration config, IAppBuilder app)
        {
            var bootstrapper = new WebApiBootstrapper();

            RegisterBootstrapperForDisposal(app, bootstrapper);

            Kernel = bootstrapper.Start();

            config.DependencyResolver = new NinjectDependencyResolver(Kernel);
        }

        private void RegisterBootstrapperForDisposal(IAppBuilder app, WebApiBootstrapper bootstrapper)
        {
            if (!app.Properties.ContainsKey(HostOnAppDisposingKey)) return;

            var cancellationToken = (CancellationToken) app.Properties[HostOnAppDisposingKey];

            cancellationToken.Register(() =>
            {
                bootstrapper.Dispose();
                Kernel.Dispose();
                Kernel = null;               
            });
        }
    }
}
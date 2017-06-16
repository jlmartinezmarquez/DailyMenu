using System;
using DailyMenu.Api;
using DailyMenu.Configuration;
using Microsoft.Owin.Testing;
using NUnit.Framework;

namespace DailyMenu.Tests.Framework
{
    public class EntToEndTestBase
    {
        protected static TestServer WebApiServer;

        protected NinjectResolver Ninject;

        private readonly IConfigurationService _configurationService = new ConfigurationService();

        public string BaseUri => _configurationService["Development.Uri"];

        //protected DataContext Context { get; set; }

        protected virtual void ReplaceBindingsWithMocks() { }

        [OneTimeSetUp]
        protected virtual void OneTimeSetup()
        {
            WebApiServer = TestServer.Create(app =>
            {
                var apiStartup = new Startup();
                apiStartup.Configuration(app);
            });

            WebApiServer.BaseAddress = new Uri("localhost");

            Ninject = new NinjectResolver(Startup.Kernel);

            ReplaceBindingsWithMocks();

            //Context = Ninject.Resolve<DataContext>();
        }

        [OneTimeTearDown]
        protected virtual void OneTimeTearDown()
        {
            WebApiServer?.Dispose();
        }
    }
}

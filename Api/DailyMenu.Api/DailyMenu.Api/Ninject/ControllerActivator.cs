using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Ninject;

namespace DailyMenu.Api.Ninject
{
    public class ControllerActivator : IHttpControllerActivator
    {
        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var controller = (IHttpController) Global.ServiceLocator.Get(controllerType);

            request.RegisterForDispose(new ControllerDeactivator(() => Global.ServiceLocator.Release(controller)));

            return controller;
        }
    }
}
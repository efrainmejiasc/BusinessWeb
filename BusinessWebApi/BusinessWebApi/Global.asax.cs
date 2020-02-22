using AppzApi.Engine;
using BusinessWebApi.Engine;
using BusinessWebApi.Engine.Interfaces;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace BusinessWebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            //Inyeccion de dependencias
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            container.Register<IEngineDb, EngineDb>(Lifestyle.Transient);
            container.Register<IEngineProject, EngineProject>(Lifestyle.Transient);
            container.Register<IEngineNotify, EngineNotify>(Lifestyle.Transient);
            container.Register<IEngineTool, EngineTool>(Lifestyle.Transient);
            //container.Register<Context, Context>(Lifestyle.Transient);
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.Verify();
            DependencyResolver dependencyResolver = new DependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = new DependencyResolver(container);
        }
    }
}

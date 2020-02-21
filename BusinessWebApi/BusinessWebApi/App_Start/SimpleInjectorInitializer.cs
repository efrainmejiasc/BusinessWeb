[assembly: WebActivator.PostApplicationStartMethod(typeof(BusinessWebApi.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace BusinessWebApi.App_Start
{
    using System.Reflection;
    using System.Web.Mvc;
    using BusinessWeb.Engine;
    using BusinessWebApi.Engine;
    using BusinessWebApi.Engine.Interfaces;
    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    
    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            
            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            
            container.Verify();
            
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
     
        private static void InitializeContainer(Container container)
        {
            container.Register<EngineContext, EngineContext>(Lifestyle.Scoped);
            container.Register<IEngineDb, EngineDb>(Lifestyle.Scoped);
            container.Register<IEngineNotify, EngineNotify>(Lifestyle.Scoped);
            container.Register<IEngineProject, EngineProject>(Lifestyle.Scoped);
            container.Register<IEngineTool, EngineTool>(Lifestyle.Scoped);
        }
    }
}
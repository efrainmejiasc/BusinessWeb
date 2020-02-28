[assembly: WebActivator.PostApplicationStartMethod(typeof(BusinessWebSite.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace BusinessWebSite.App_Start
{
    using System.Reflection;
    using System.Web.Mvc;
    using BusinessWebSite.Engine;
    using BusinessWebSite.Engine.Interfaces;
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
            container.Register<IEngineHttp, EngineHttp>(Lifestyle.Transient);
            container.Register<IEngineProject, EngineProject>(Lifestyle.Transient);
            //container.Register<IEngineNotify, EngineNotify>(Lifestyle.Transient);
            container.Register<IEngineTool, EngineTool>(Lifestyle.Transient);
            // For instance:
            // container.Register<IUserRepository, SqlUserRepository>(Lifestyle.Scoped);
        }
    }
}
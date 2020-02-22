
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dependencies;

namespace AppzApi.Engine
{
    public class DependencyResolver  :  IDependencyResolver, IDependencyScope
    {
       public Container container { get; private set; }

        public DependencyResolver(Container _container)
        {
            this.container = _container;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType.IsAbstract && typeof(ApiController).IsAssignableFrom(serviceType))
                return this.container.GetInstance(serviceType);
            return ((IServiceProvider)this.container).GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.container.GetAllInstances(serviceType);
        }

        IDependencyScope IDependencyResolver.BeginScope()
        {
            return this;
        }

        object IDependencyScope.GetService(Type serviceType)
        {
            return ((IServiceProvider)this.container).GetService(serviceType);
        }

        IEnumerable<object> IDependencyScope.GetServices(Type serviceType)
        {
            IServiceProvider provider = this.container;
            Type collectionType = typeof(IEnumerable<>).MakeGenericType(serviceType);
            var services = (IEnumerable<object>)provider.GetService(collectionType);
            return services ?? Enumerable.Empty<object>();
        }

         void IDisposable.Dispose() { }
    }
}
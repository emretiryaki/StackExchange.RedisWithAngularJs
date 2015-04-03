using System;
using System.Collections.Generic;
using System.Linq;
using Castle.MicroKernel;
using IDependencyResolver = System.Web.Mvc.IDependencyResolver;

namespace Session.Redis
{
    public class WindsorDependencyResolver : IDependencyResolver
    {
        private readonly IKernel kernel;

        public WindsorDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public object GetService(Type serviceType)
        {
            return kernel.HasComponent(serviceType) ? kernel.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.HasComponent(serviceType) ? kernel.ResolveAll(serviceType).Cast<object>() : new object[] {};
        }
    }
}
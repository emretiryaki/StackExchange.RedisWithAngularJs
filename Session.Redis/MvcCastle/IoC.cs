using System;
using System.Web.Mvc;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace Session.Redis
{
    public static class IoC
    {
        private static IKernel current;

        public static void Initialize(IKernel kernel)
        {
            IWindsorContainer container = new WindsorContainer(kernel, new DefaultComponentInstaller());
            container.Install(FromAssembly.This());
            DependencyResolver.SetResolver(new WindsorDependencyResolver(container.Kernel));
            container.Register(
                Component.For<IActionInvoker>()
                    .ImplementedBy<WindsorActionInvoker>()
                    .DependsOn(Property.ForKey("kernel").Eq(container.Kernel))
                    .LifeStyle.Transient);
            current = kernel;
        }

        public static T Resolve<T>()
        {
            try
            {
                return current.Resolve<T>();
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }

   
}
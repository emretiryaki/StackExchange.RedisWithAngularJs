using System.Web;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Session.Redis.Helpers.SessionProvider;
using Session.Redis.Helpers.UserManager;

namespace Session.Redis
{
    public class PortalInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ISessionProvider>().ImplementedBy<HttpContextSessionProvider>().LifeStyle.Singleton);
            container.Register(Component.For<IUserManager>().ImplementedBy<UserManager>().LifeStyle.Singleton);
         
            //container.Register(
            //    Component.For<HttpContextBase>().LifeStyle.PerWebRequest.UsingFactoryMethod(
            //        () => HttpContext.Current != null
            //            ? (new HttpContextWrapper(HttpContext.Current) as HttpContextBase)
            //            : (new FakeHttpContext("~/") as HttpContextBase)));
            container.Register(Component.For<HttpRequestBase>().LifeStyle.PerWebRequest.UsingFactoryMethod(() => IoC.Resolve<HttpContextBase>().Request));
            container.Register(Component.For<HttpResponseBase>().LifeStyle.PerWebRequest.UsingFactoryMethod(() => IoC.Resolve<HttpContextBase>().Response));
            container.Register(Component.For<HttpServerUtilityBase>().LifeStyle.PerWebRequest.UsingFactoryMethod(() => IoC.Resolve<HttpContextBase>().Server));
            container.Register(Component.For<HttpSessionStateBase>().LifeStyle.PerWebRequest.UsingFactoryMethod(() => IoC.Resolve<HttpContextBase>().Session));


        

        }

      
    }
}
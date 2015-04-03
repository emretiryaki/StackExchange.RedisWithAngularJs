using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel;
using log4net;
using log4net.Repository.Hierarchy;
using RedisSessionProvider.Config;
using Session.Redis.Helpers.UserManager;
using Session.Redis.Models;
using StackExchange.Redis;

namespace Session.Redis
{

    public class MvcApplication : System.Web.HttpApplication
    {
        private static IKernel _kernel;
     
        private static ConfigurationOptions redisConfigOpts;
       
        protected void Application_Start()
        {

            #region Fake User Data

            FakeUserList.UserModels.Add( new UserModel
            {
                FirstName = "Emre",
                LastName = "Tiryaki",
                Email = "emretiryaki3@gmail.com",
                Id = 1,
                MiddleName = "",
                Password = "123qwe",
                UserName = "etiryaki"
                
            });
            FakeUserList.UserModels.Add(new UserModel
            {
                FirstName = "Fatih",
                LastName = "Yalçın",
                Email = "fatihyalcin@gmail.com",
                Id = 1,
                MiddleName = "",
                Password = "123qwe",
                UserName = "fyalcin"

            });

            #endregion

           
            BootstrapContainer();
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
            if (LogManager.GetRepository().Configured == false)
                throw  new Exception("Log4net Not Configured");

            Logger.logger.Info("Startting MVC");
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            RedisConfiguration();
        }

        private void RedisConfiguration()
        {
            //https://github.com/welegan/RedisSessionProvider

            var redisServerIp = ConfigurationManager.AppSettings["RedisServerIp"];
            var redisServerPort = ConfigurationManager.AppSettings["RedisServerPort"];
            var redisServerVersion = ConfigurationManager.AppSettings["RedisServerVersion"];
            var redisConfigOpts = ConfigurationOptions.Parse(string.Format("{0}:{1}", redisServerIp, redisServerPort));


            RedisConnectionConfig.GetSERedisServerConfig = (context) =>
            {
                return new KeyValuePair<string, ConfigurationOptions>(
                "DefaultConnection",
                redisConfigOpts);
            };

            RedisConnectionConfig.GetRedisServerAddress = context => new RedisConnectionParameters()
            {
                ServerAddress = redisServerIp,
                ServerPort = int.Parse( redisServerPort),
                ServerVersion = redisServerVersion

            };

            RedisSessionConfig.SessionExceptionLoggingDel = e => Logger.logger.Error("Unhandled RedisSessionProvider exception", e);
            RedisConnectionConfig.LogConnectionActionsCountDel = (string name, long count) => Logger.logger.DebugFormat(string.Format("Redis connection {0} had {1} operations", name, count));
        }

        private static void BootstrapContainer()
        {
            _kernel = new Bootstrapper().kernel;
            IoC.Initialize(_kernel);
            WindsorControllerFactory controllerFactory = new WindsorControllerFactory(_kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }
    }
}
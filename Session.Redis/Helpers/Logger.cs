using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace Session.Redis
{
    public class Logger
    {
        private static readonly Lazy<ILog> _log = new Lazy<ILog>(() => LogManager.GetLogger(typeof(Logger)));

        public Logger()
        {

        }

        public static ILog logger
        {
            get
            {
                return _log.Value;
            }
        }
    }
}


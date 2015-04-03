using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Castle.MicroKernel;

namespace Session.Redis
{
    public class WindsorActionInvoker : ControllerActionInvoker
    {
        private readonly IKernel kernel;


        public WindsorActionInvoker(IKernel kernel)
        {
            this.kernel = kernel;
        }

        protected override ExceptionContext InvokeExceptionFilters(ControllerContext controllerContext,
            IList<IExceptionFilter> filters, Exception exception)
        {
            foreach (IExceptionFilter actionFilter in filters)
            {
                kernel.InjectProperties(actionFilter);
            }
            return base.InvokeExceptionFilters(controllerContext, filters, exception);
        }
    }
}
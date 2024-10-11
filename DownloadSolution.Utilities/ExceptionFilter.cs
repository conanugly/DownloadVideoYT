using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System.Diagnostics;

namespace DownloadSolution.Utilities
{
    public class ExceptionFilters : ActionFilterAttribute, IExceptionFilter
    {
        private ILogger _logger;
        public ExceptionFilters()
        {
            //_logger = ApplicationConfiguration.GetLogger();
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
          
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var actionArguments = context.ActionArguments.Values.ToList();
            var ctlstr = context.Controller.ToString();
            _logger = ApplicationConfiguration.GetLogger();
            _logger.Debug("LLLLLLLLLLLLLLLLLLLLLLLLKKKKKKKKKKKKKKKKKKKKKKKKKKKKK");
            base.OnActionExecuting(context);
        }

        
        public void OnException(ExceptionContext context)
        {
            //var yourvalue1 = ApplicationConfiguration.GetSetting("YourKey1");
            _logger.Error(context.Exception.Message);
        }
    }
}
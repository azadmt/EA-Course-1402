using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace APIGateway.Common
{
    public class MyCustomAuthorizeFilter : ActionFilterAttribute
    {
        public string Operation { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userClaim = context.HttpContext.RequestServices.GetService<UserClaim>();
            // our code before action executes
            if (!userClaim.HassPermission(Operation))
                context.Result = new UnauthorizedResult();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            // our code after action executes
        }
    }

    public class LogMetricFilter : ActionFilterAttribute
    {
        private Stopwatch stopwatch;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            stopwatch = Stopwatch.StartNew();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            stopwatch.Stop();
            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            var message = $"Action took {elapsedMilliseconds} ms to execute.";
            Debug.WriteLine(message);
        }
    }
}
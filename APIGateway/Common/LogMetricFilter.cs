using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace APIGateway.Common
{
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
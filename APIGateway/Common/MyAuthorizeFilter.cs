using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIGateway.Common
{
    public class MyCustomAuthorizeFilter : ActionFilterAttribute
    {
        public string Operation { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.User.Claims.Any(x => x.Value == Operation))
                context.Result = new UnauthorizedResult();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            // our code after action executes
        }
    }
}
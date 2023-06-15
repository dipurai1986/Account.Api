using Microsoft.AspNetCore.Mvc.Filters;

namespace Account.API.Middleware
{
    public class CustomFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Logic to be executed before the action method is called
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            // Logic to be executed after the action method has been called
        }
    }
}

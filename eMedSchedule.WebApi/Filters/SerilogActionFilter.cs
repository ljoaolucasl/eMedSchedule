using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace eMedSchedule.WebApi.Filters
{
    public class SerilogActionFilter : IActionFilter
    {
        private object? endpoint;
        private object? module;

        public void OnActionExecuting(ActionExecutingContext context)
        {
            endpoint = context.RouteData.Values["action"];

            module = context.RouteData.Values["controller"];

            Log.Logger.Information($"[Module {module}] -> Trying {endpoint}. . .");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
                Log.Logger.Information($"[Module {module}] -> Failed to execute {endpoint}.");
            else
                Log.Logger.Information($"[Module {module}] -> Success when executing {endpoint}!");
        }
    }
}
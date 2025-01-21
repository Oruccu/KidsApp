using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace KidsAppBackend.WebApi.Filters
{
    public class ExecutionTimeFilter : IAsyncActionFilter
    {
        private readonly ILogger<ExecutionTimeFilter> _logger;

        public ExecutionTimeFilter(ILogger<ExecutionTimeFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var stopwatch = Stopwatch.StartNew();

            var executedContext = await next(); // Action çalıştırılır

            stopwatch.Stop();

            var actionName = context.ActionDescriptor.DisplayName;
            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

            _logger.LogInformation($"Action '{actionName}' executed in {elapsedMilliseconds} ms.");
        }
    }
}

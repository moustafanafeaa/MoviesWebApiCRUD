using Microsoft.AspNetCore.Mvc.Filters;

namespace movies.Filters
{
	public class LogActivityFilter : IActionFilter
	{
		private ILogger<LogActivityFilter> _logger;

		public LogActivityFilter(ILogger<LogActivityFilter> logger)
		{
			_logger = logger;
		}

		public void OnActionExecuting(ActionExecutingContext context)
		{
			_logger.LogInformation($"Executing action {context.ActionDescriptor.DisplayName} on controller {context.Controller}");
		}
		public void OnActionExecuted(ActionExecutedContext context)
		{
			_logger.LogInformation($"finish action {context.ActionDescriptor.DisplayName} on controller {context.Controller}");
		}


	}
}

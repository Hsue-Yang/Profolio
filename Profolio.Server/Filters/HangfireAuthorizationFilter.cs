using Hangfire.Dashboard;

namespace Profolio.Server.Filters
{
	public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        private readonly IConfiguration _config;
        private readonly ILogger _logger;

		public HangfireAuthorizationFilter(IConfiguration config, ILogger logger)
		{
			_config = config;
			_logger = logger;
		}

		public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            var ipAddress = httpContext.Connection.RemoteIpAddress?.ToString() ?? httpContext.Request.Headers["X-Forwarded-For"];
            var whiteIpList = _config.GetSection("Hangfire:WhiteIpList").Get<List<string>>();
#if DEBUG
            whiteIpList.Add("::1");
#endif
            return whiteIpList.Any(x => ipAddress.StartsWith(x));
        }
    }
}
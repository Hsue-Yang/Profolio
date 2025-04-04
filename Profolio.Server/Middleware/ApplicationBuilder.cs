using Hangfire;
using Profolio.Server.Filters;
using Profolio.Server.Services;

namespace Profolio.Server.Middleware
{
	public static class ApplicationBuilder
	{
		public static IApplicationBuilder CustomMiddleware(this IApplicationBuilder app, IEndpointRouteBuilder endpoint, IWebHostEnvironment environment, IConfiguration configuration, ILogger logger)
		{
			app.UseMiddleware<RequestLogMiddleware>();
			app.UseDefaultFiles();
			app.UseStaticFiles();
			app.UseCors("AllowSpecificOrigins");
			if (environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHangfireDashboard("/hangfire", options: new DashboardOptions
			{
				Authorization = new[] { new HangfireAuthorizationFilter(configuration, logger) }
			});
			// 必須排在app.UseHangfireDashboard()之後
			RecurringJob.AddOrUpdate<ScheduleService>("HackMd", service => service.SyncHackMdData(),
				// 每月執行一次
				Cron.Monthly()
			);
			endpoint.MapHangfireDashboard();

			app.UseHttpsRedirection();

			app.UseAuthorization();
			app.UseResponseCaching();
			endpoint.MapControllers();

			endpoint.MapFallbackToFile("/index.html");

			return app;
		}
	}
}
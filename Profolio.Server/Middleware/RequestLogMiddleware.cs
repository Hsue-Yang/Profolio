using Confluent.Kafka;
using Profolio.Server.Tools.Kafka;

namespace Profolio.Server.Middleware
{
	public class RequestLogMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly KafkaProducer<string, string> _kafkaProducer;

		public RequestLogMiddleware(RequestDelegate next, KafkaProducer<string, string> kafkaProducer)
		{
			_next = next;
			_kafkaProducer = kafkaProducer;
		}

		public async Task Invoke(HttpContext context)
		{
			decimal startTime = DateTime.Now.Ticks;
			await _next(context);
			decimal endTime = DateTime.Now.Ticks;
			decimal duration = endTime - startTime;
			_kafkaProducer.Produce("test-topic", new Message<string, string>
			{
				Key = "Request",
				Value = $"Request: {context.Request.Path} {context.Request.QueryString} {context.Request.Method} {context.Request.Body} {context.Request.Headers}"
			}, _kafkaProducer.DeliverReportHandler);
		}
	}
}
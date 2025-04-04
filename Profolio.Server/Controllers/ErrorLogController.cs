using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Profolio.Server.Dto;
using Profolio.Server.Services.Interfaces;
using Profolio.Server.Tools.Kafka;

namespace Profolio.Server.Controllers
{
	public class ErrorLogController : BaseController
	{
		private readonly IErrorLogService _service;
		private readonly KafkaProducer<string, string> _kafkaProducer;

		public ErrorLogController(IErrorLogService service, KafkaProducer<string, string> kafkaProducer)
		{
			_service = service;
			_kafkaProducer = kafkaProducer;
		}

		[HttpPost("Log")]
		public async Task<IActionResult> LogError([FromBody] ErrorLogDto error)
		{
			if (error == null)
			{
				return BadRequest("Error data is required.");
			}
			await _service.LogErrorAsync(error.Error, error.ErrorInfo);
			return Ok();
		}

		[HttpGet("TestKafkaPublish")]
		public void TestPublish()
		{
			_kafkaProducer.Produce("test-topic", new Message<string, string>
			{
				Key = "test",
				Value = "test"
			}, _kafkaProducer.DeliverReportHandler);
		}
	}
}
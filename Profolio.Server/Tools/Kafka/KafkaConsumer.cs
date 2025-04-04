using Confluent.Kafka;
using Profolio.Server.Services.Interfaces;

namespace Profolio.Server.Tools.Kafka
{
	/// <summary> Subscribes Kafka topic and Send message to MongoDB </summary>
	/// <typeparam name="K"></typeparam>
	/// <typeparam name="V"></typeparam>
	public class KafkaConsumer<K, V> : BackgroundService
	{
		IConsumer<K, V> consumer;
		private readonly IMongoService _mongoService;
		private readonly string _topic;
		public KafkaConsumer(KafkaConsumeHandler<K, V> handle, IMongoService mongoService, string topic)
		{
			_topic = topic;
			_mongoService = mongoService;
			consumer = handle.kafkaConsumer;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			consumer.Subscribe("test-topic");
			try
			{
				while (!stoppingToken.IsCancellationRequested)
				{
					ConsumeMessage(stoppingToken);
				}
			}
			catch (ConsumeException ex)
			{
				await _mongoService.InsertOneAsync("Profolio", ex.Error.Reason);
			}
		}

		/// <summary> Pull message from Kafka and send to MongoDB </summary>
		/// <param name="stoppingToken"></param>
		public async void ConsumeMessage(CancellationToken stoppingToken)
		{
			var consumeResult = consumer.Consume(stoppingToken);
			var message = consumeResult.Message.Value;
			await _mongoService.InsertOneAsync("Profolio", message);
		}
	}
}
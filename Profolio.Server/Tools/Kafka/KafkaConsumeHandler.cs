using Confluent.Kafka;

namespace Profolio.Server.Tools.Kafka
{
	/// <summary> Configures the Kafka consumer </summary>
	/// <typeparam name="K"></typeparam>
	/// <typeparam name="V"></typeparam>
	public class KafkaConsumeHandler<K, V> : IDisposable
	{
		public IConsumer<K, V> kafkaConsumer { get; }
		public KafkaConsumeHandler(IConfiguration configuration)
		{
			ConsumerConfig config = new ConsumerConfig
			{
				GroupId = configuration.GetValue("Kafka:GroupId", string.Empty),
				BootstrapServers = configuration.GetValue("Kafka:BootstrapServers", string.Empty),
				AutoOffsetReset = AutoOffsetReset.Earliest
			};

			kafkaConsumer = new ConsumerBuilder<K, V>(config).Build();
		}

		public void Dispose()
		{
			kafkaConsumer.Close();
			kafkaConsumer.Dispose();
		}
	}
}
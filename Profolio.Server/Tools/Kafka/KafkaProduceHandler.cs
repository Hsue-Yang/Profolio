using Confluent.Kafka;

namespace Profolio.Server.Tools.Kafka
{
	public class KafkaProduceHandler<K, V> : IDisposable
	{
		public IProducer<K, V> kafkaProducer { get; }
		public KafkaProduceHandler(IConfiguration configuration)
		{
			ProducerConfig config = new ProducerConfig
			{
				BootstrapServers = configuration.GetValue("Kafka:BootstrapServers", string.Empty)
			};

			kafkaProducer = new ProducerBuilder<K, V>(config).Build();
		}
		public Handle Handle { get => kafkaProducer.Handle; }
		public void Dispose()
		{
			kafkaProducer.Flush();
			kafkaProducer.Dispose();
		}
	}
}
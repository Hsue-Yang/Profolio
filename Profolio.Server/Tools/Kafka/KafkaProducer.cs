using Confluent.Kafka;

namespace Profolio.Server.Tools.Kafka
{
	public class KafkaProducer<K, V>
	{
		// Key => Category, Value => Message
		IProducer<K, V> kafkaHandler;

		public KafkaProducer(KafkaProduceHandler<K, V> handle)
		{
			kafkaHandler = handle.kafkaProducer;
		}

		public Task ProduceAsync(string topic, Message<K, V> message)
			=> kafkaHandler.ProduceAsync(topic, message);

		public void Produce(string topic, Message<K, V> message, Action<DeliveryReport<K, V>> deliveryHandler = null)
			=> kafkaHandler.Produce(topic, message, deliveryHandler);

		public void Flush(TimeSpan timeout)
			=> kafkaHandler.Flush(timeout);

		public void DeliverReportHandler(DeliveryReport<K, V> deliveryReport)
		{
			if (deliveryReport.Status == PersistenceStatus.Persisted)
			{
				// success logic
				Console.WriteLine($"Delivered message to {deliveryReport.TopicPartitionOffset}");
			}
			if (deliveryReport.Status == PersistenceStatus.NotPersisted)
			{
				// add error handling
				Console.WriteLine($"Delivered message to {deliveryReport.TopicPartitionOffset}, but the status is unknown.");
			}
			if (deliveryReport.Error.Code != ErrorCode.NoError)
			{
				// add error handling
				Console.WriteLine($"Failed to deliver message: {deliveryReport.Error.Reason}");
			}
		}
	}
}
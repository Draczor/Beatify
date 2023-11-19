namespace UserService.Producer
{
    public interface IRabbitMQProducer
    {
        public void SendMessage<T>(T message);
    }
}

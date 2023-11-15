namespace UserService.Producer
{
    public interface IMessageProducer
    {
        void SendMessage<T>(T message);
    }
}

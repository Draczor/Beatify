using MassTransit;
using Microsoft.AspNetCore.Connections;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using UserService.Models;

namespace UserService.Producer
{
    public class RabbitMQProducer : IRabbitMQProducer
    {
        private readonly IBus _bus;

        public RabbitMQProducer(IBus bus)
        {
            _bus = bus;
        }

        public async Task SendUserCreated(User user)
        {
            await _bus.Publish(user);
        }

        public async Task SendMessageCreated(MyMessage message)
        {
            Console.WriteLine(message.Content);
            await _bus.Publish(message);
        }
    }
}

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
    }
}

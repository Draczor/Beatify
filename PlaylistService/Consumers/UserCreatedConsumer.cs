using MassTransit;
using UserService.Models;
using System.Diagnostics;
using System.Text.Json;

namespace PlaylistService.Consumers
{
    public class UserCreatedConsumer : IConsumer<User>
    {
        public async Task Consume(ConsumeContext<User> context)
        {
            var serializedMessage = JsonSerializer.Serialize(context.Message, new JsonSerializerOptions { });
            var content = context.Message;
            Console.WriteLine($"UserCreated event consumed from content. Message: {content}");
            Console.WriteLine($"UserCreated event consumed. Message: {serializedMessage}");
        }
    }
}

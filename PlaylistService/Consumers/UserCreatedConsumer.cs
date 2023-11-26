using MassTransit;
using PlaylistService.Models;
using System.Diagnostics;
using System.Text.Json;

namespace PlaylistService.Consumers
{
    public class UserCreatedConsumer : IConsumer<User>
    {
        public async Task Consume(ConsumeContext<User> context)
        {
            //var message = context.Message;
            var serializedMessage = JsonSerializer.Serialize(context.Message, new JsonSerializerOptions { });
            var content = context.Message;
            Console.WriteLine($"UserCreated event consumed from content. Message: {content}");
            //await Console.Out.WriteLineAsync($"Message from Producer : {serializedMessage}");
            Console.WriteLine($"UserCreated event consumed. Message: {serializedMessage}");
            Debug.WriteLine($"UserCreated event consumed. Message: {serializedMessage}");
        }
    }
}

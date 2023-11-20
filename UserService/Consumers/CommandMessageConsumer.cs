using MassTransit;
using System.Diagnostics;
using System.Text.Json;
using UserService.Models;

namespace UserService.Consumers
{
    public class CommandMessageConsumer : IConsumer<User>
    {
        public async Task Consume(ConsumeContext<User> context)
        {
            //var message = context.Message;
            var serializedMessage = JsonSerializer.Serialize(context.Message, new JsonSerializerOptions { });
            await Console.Out.WriteLineAsync($"Message from Producer : {serializedMessage}");
        }
    }
}

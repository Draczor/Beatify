using MassTransit;
using System.Diagnostics;
using UserService.Models;

namespace UserService.Consumers
{
    public class CommandMessageConsumer : IConsumer<User>
    {
        public async Task Consume(ConsumeContext<User> context)
        {
            var message = context.Message;
            await Console.Out.WriteLineAsync($"Message from Producer : {message}");
        }
    }
}

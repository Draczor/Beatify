using MassTransit;
using UserService.Shared;

namespace UserService.Consumers
{
    public class CommandMessageConsumer : IConsumer<CommandMessage>
    {
        public async Task Consume(ConsumeContext<CommandMessage> context)
        {
            var message = context.Message;
            await Console.Out.WriteLineAsync($"Message from Producer : {message.MessageString}");
        }
    }
}

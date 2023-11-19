/*using MassTransit;
using UserService.Shared;

namespace UserService.Consumers
{
    public interface IConsumer<in TMessage> : IConsumer
    where TMessage : class
    {
        Task Consume(ConsumeContext<TMessage> context);
    }
    public interface IConsumer { }

    public class CommandMessageConsumer : IConsumer<CommandMessage>
    {
        public async Task Consume(ConsumeContext<CommandMessage> context)
        {
            var message = context.Message;
            await Console.Out.WriteLineAsync("Message from Producer : {message.MessageString}");
            // Do something useful with the message

        }
    }
}
*/
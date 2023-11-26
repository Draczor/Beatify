using MassTransit;
using PlaylistService.Models;
using System.Diagnostics;

namespace PlaylistService.Consumers
{
    public class MyMessageConsumer : IConsumer<MyMessage>
    {
        public async Task Consume(ConsumeContext<MyMessage> context)
        {
            var content = context.Message.Content;
            // Process the message as needed
            Console.WriteLine("KAPPAKAPPAKAPPAKAPPAKAPPAKAPPAKAPPAKAPPAKAPPAV");
            Console.WriteLine($"Event consumed from content. Message: {content}");
            Debug.WriteLine($"Event consumed from content. Message: {content}");
        }
    }
}

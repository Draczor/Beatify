using Microsoft.AspNetCore.Connections;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace UserService.Producer
{
    public class RabbitMQProducer : IRabbitMQProducer
    {
        public void SendMessage<T>(T message)
        {
            var factory = new ConnectionFactory 
            { 
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };

            IConnection tryConnection = null;

            // Retry logic with a maximum number of attempts
            const int maxAttempts = 3;
            int attempts = 0;

            while (attempts < maxAttempts)
            {
                try
                {
                    tryConnection = factory.CreateConnection();
                    // Connection successful, break out of the loop
                    //var connection = factory.CreateConnection();
                    using var channel = tryConnection.CreateModel();

                    channel.QueueDeclare("users", exclusive: false);

                    var json = JsonConvert.SerializeObject(message);
                    var body = Encoding.UTF8.GetBytes(json);
                    channel.BasicPublish(exchange: "", routingKey: "users", body: body);
                    break;
                }
                catch (Exception ex)
                {
                    // Log the exception or take other appropriate action
                    Console.WriteLine($"Connection attempt failed: {ex.Message}");

                    // Increment the attempts and wait before retrying
                    attempts++;
                    Thread.Sleep(1000); // Wait for 1 second before retrying
                }
            }

            if (tryConnection == null)
            {
                // Handle the case where the maximum number of attempts is reached
                Console.WriteLine("Failed to establish connection after multiple attempts.");
            }

            /*var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare("users", exclusive: false);

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange: "", routingKey: "users", body: body);*/
        }
    }
}

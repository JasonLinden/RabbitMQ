using RabbitMQ.Client;
using System.Text;

namespace SendMQ
{
    class Program
    {
        // This is the publisher
        // The publisher will connect to RabbitMQ, send a single message, then exit.
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    // To send, we must declare a queue for us to send to; then we can publish a message to the queue:
                    channel.QueueDeclare(
                        queue: "hello",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );

                    string message = "Hello World!";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: "hello",
                        basicProperties: null,
                        body: body
                    );

                    System.Console.WriteLine(" [x] Sent {0}", message);
                }

                System.Console.WriteLine(" Press [enter] to exit.");
                System.Console.ReadLine();
            }
        }
    }
}
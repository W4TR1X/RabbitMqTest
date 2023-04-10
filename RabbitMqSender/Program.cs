using RabbitMQ.Client;
using System.Text;

const string DEFAULT_MESSAGE = "Helllo World!";

var factory = new ConnectionFactory() { HostName = "localhost" };
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.QueueDeclare(queue: "hello",
                         durable: true,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

    string message = DEFAULT_MESSAGE;

    do
    {
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(
            exchange: "",
            routingKey: "hello",
            basicProperties: null,
            body: body);

        Console.WriteLine(" Sended => '{0}'", message);
        Console.WriteLine(" Write exit to exit.");

        message = Console.ReadLine() ?? DEFAULT_MESSAGE;
    } while (message != "exit");
}
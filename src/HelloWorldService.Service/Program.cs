using HelloWorldService.Application;
using HelloWorldService.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.IO;
using System.Threading;

namespace HelloWorldService.Service
{
    class Program
    {
        private static IConfiguration _configuration;
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json");
            _configuration = builder.Build();

            var rabbitMQConfigurations = new RabbitMQConfigurations();
            new ConfigureFromConfigurationOptions<RabbitMQConfigurations>(
                _configuration.GetSection("RabbitMQConfigurations"))
                    .Configure(rabbitMQConfigurations);

            var factory = new ConnectionFactory()
            {
                HostName = rabbitMQConfigurations.HostName,
                Port = rabbitMQConfigurations.Port,
                UserName = rabbitMQConfigurations.UserName,
                Password = rabbitMQConfigurations.Password
            };

            bool readOwnMessage;

            if (args.Length.Equals(0))
                readOwnMessage = _configuration.GetValue<bool>("readOwnMessage");
            else
                readOwnMessage = bool.Parse(args[0]);

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "logs", type: "fanout");
                var res = channel.QueueDeclare(queue: "", exclusive: true, arguments: null);
                string clientID = res.QueueName;
                channel.QueueBind(clientID, "logs", "*");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += Process.Consumer_Received;
                channel.BasicConsume(queue: clientID,
                     autoAck: true,
                     consumer: consumer);

                Console.WriteLine("Meu id -> " + clientID);
                while (true)
                {
                    var messageToSend = new Message
                    {
                        Id = Guid.NewGuid(),
                        MicroserviceId = clientID,
                        Content = "Hello world!",
                        Timestamp = DateTime.Now.ToString("yyyyMMddHHmmssffff")
                    };
                    Process.EnviarMensagem(factory, messageToSend, readOwnMessage);
                    Thread.Sleep(5000);
                }
            }
        }
    }
}

using HelloWorldService.Application.JsonUtil;
using HelloWorldService.Domain;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorldService.Application
{
    public static class Process
    {
        private static string clientID;
        private static bool _readOwnMessage;
        public static void EnviarMensagem(ConnectionFactory factory, Message message, bool readOwnMessage)
        {
            _readOwnMessage = readOwnMessage;
            clientID = message.MicroserviceId;
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                string messageString = FormataMensagem(message);

                var body = Encoding.UTF8.GetBytes(messageString);

                channel.BasicPublish(exchange: "logs",
                                     routingKey: "",
                                     basicProperties: null,
                                     body: body);
            }
        }

        public static void Consumer_Received(
            object sender, BasicDeliverEventArgs e)
        {
            var messagestring = Encoding.UTF8.GetString(e.Body);
            var message = GetMessageFromMessageContent(messagestring);
            if (_readOwnMessage)
                Console.WriteLine(Environment.NewLine + string.Format("[Nova mensagem recebida de {0} - MenssagenId {1} - Timestamp {2}] - {3}", message.MicroserviceId, message.Id, message.Timestamp, message.Content));
            else if(message.MicroserviceId != clientID)
                Console.WriteLine(Environment.NewLine + string.Format("[Nova mensagem recebida de {0} - MenssagenId {1} - Timestamp {2}] - {3}", message.MicroserviceId, message.Id, message.Timestamp, message.Content));
        }

        private static string FormataMensagem(Message message)
        {
            return JsonConverter.Serialize<Message>(message);
        }

        private static Message GetMessageFromMessageContent(string messagecontent)
        {
            return JsonConverter.Desserialized<Message>(messagecontent);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace OrderService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqp://webapp:rabbitmq@host.docker.internal:5672");
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.ExchangeDeclare("demo.topic", ExchangeType.Topic, true);
            channel.QueueDeclare("demo", true, false, false);
            channel.QueueBind("demo", "demo.topic", "order.*");

            var headers = new Dictionary<string, object>{
                {"subject", "order"},
                {"action", "create"},
                {"x-match", "any"}
            };
            channel.ExchangeDeclare("demo.headers", ExchangeType.Headers, true);
            channel.QueueDeclare("order.service", true, false, false);
            channel.QueueBind("order.service", "demo.headers", "", headers);

            channel.Close();
            connection.Close();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {

        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Object Get()
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqp://webapp:rabbitmq@host.docker.internal:5672");
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            var consumer = new EventingBasicConsumer(channel);
            string msg;
            string subject;


            consumer.Received += (sender, eventArgs) => {
                msg = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
                subject = Encoding.UTF8.GetString(eventArgs.BasicProperties.Headers["subject"] as byte[]);
            
                Console.WriteLine($"Message: {msg}, Header: {subject}");
            };

            channel.BasicConsume("order.service", true, consumer);

            return new {
                success = true
            };
        }

         [HttpPost]
        public Object Create(){

            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqp://webapp:rabbitmq@host.docker.internal:5672");
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            var message = "Create new order";
            var bytes = Encoding.UTF8.GetBytes(message);

            // 1. publish directly to queue
            // channel.QueueDeclare("demo.order", true, false, false);
            // channel.BasicPublish("", "demo.order", null, bytes);

            // 2. publish by topic exchange
            // channel.BasicPublish("demo.topic", "order.create", null, bytes);

            // 3. publish by header exchange
            var headers = new Dictionary<string, object>{
                {"subject", "order"}
            };
            var props = channel.CreateBasicProperties();
            props.Headers = headers;
            channel.BasicPublish("demo.headers", "", props, bytes);

            channel.Close();
            connection.Close();
            return new { success = true };
        }
    }
}

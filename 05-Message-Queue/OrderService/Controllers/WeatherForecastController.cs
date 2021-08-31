using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

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
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55)
            })
            .ToArray();
        }

         [HttpPost]
        public Object Create(){

            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqp://webapp:rabbitmq@host.docker.internal:5672");
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            var message = "Create new order";
            var bytes = Encoding.UTF8.GetBytes(message);

            channel.QueueDeclare("demo.order", true, false, false);
            channel.BasicPublish("", "demo.order", null, bytes);

            channel.Close();
            connection.Close();
            return new { success = true };
        }
    }
}

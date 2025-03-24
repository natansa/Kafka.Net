using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;

namespace Api.Consumer.Controllers;

[ApiController]
[Route("[controller]")]
public class ConsumerController : ControllerBase
{
    private readonly ILogger<ConsumerController> _logger;
    private readonly ConsumerConfig _config;
    private readonly string _topic;

    public ConsumerController(ILogger<ConsumerController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _config = new ConsumerConfig
        {
            BootstrapServers = configuration.GetSection("Kafka:Server").Value,
            GroupId = configuration.GetSection("Kafka:Group").Value,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        _topic = configuration.GetSection("Kafka:Topic").Value!;
    }

    [HttpGet(Name = "Consumer")]
    public IActionResult ConsumerAsync()
    {
        using var consumer = new ConsumerBuilder<Ignore, string>(_config).Build();
        consumer.Subscribe(_topic);

        var messageConcsumer = consumer.Consume();
        var message = messageConcsumer.Message.Value;

        _logger.LogInformation("Message consumed in topic {topic}: {message}", _topic, message);

        return Ok(message);
    }
}
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;

namespace Api.Publisher.Controllers;

[ApiController]
[Route("[controller]")]
public class PublisherController : ControllerBase
{
    private readonly ILogger<PublisherController> _logger;
    private readonly ProducerConfig _config;
    private readonly string _topic;

    public PublisherController(ILogger<PublisherController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _config = new ProducerConfig { BootstrapServers = configuration.GetSection("Kafka:Server").Value };
        _topic = configuration.GetSection("Kafka:Topic").Value!;
    }

    [HttpPost(Name = "Publisher")]
    public async Task<IActionResult> PublisherAsync([FromBody] string message)
    {
        using var producer = new ProducerBuilder<Null, string>(_config).Build();
        var result = await producer.ProduceAsync(_topic, new Message<Null, string> { Value = message });
        _logger.LogInformation("Message posted in topic {topic}: {message}", _topic, message);
        return Created();
    }
}
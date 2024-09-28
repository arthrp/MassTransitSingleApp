using MassTransit;

namespace MassTransitSingleApp;

public class TimeMessageConsumer(ILogger<TimeMessageConsumer> _logger, MessageProvider _provider) : IConsumer<TimeMessageDto>
{
    public async Task Consume(ConsumeContext<TimeMessageDto> context)
    {
        _logger.LogInformation("Received message - current time is {timeUtc}", context.Message.TimeUtc);
        _provider.AddLatest(context.Message);
        await Task.CompletedTask;
    }
}
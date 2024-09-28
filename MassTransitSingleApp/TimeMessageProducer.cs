using MassTransit;

namespace MassTransitSingleApp;

public class TimeMessageProducer(IBus _bus) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _bus.Publish<TimeMessageDto>(new TimeMessageDto($"{DateTime.UtcNow:yyyy-MM-dd HH':'mm':'ss}"), stoppingToken);

            await Task.Delay(2000, stoppingToken);
        }
    }
}
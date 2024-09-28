using System.Collections.Concurrent;

namespace MassTransitSingleApp;

public class MessageProvider
{
    private readonly ConcurrentDictionary<int, TimeMessageDto> _dict = new();

    public void AddLatest(TimeMessageDto message)
    {
        _dict[0] = message;
    }

    public TimeMessageDto GetLatest()
    {
        return _dict[0];
    }
}
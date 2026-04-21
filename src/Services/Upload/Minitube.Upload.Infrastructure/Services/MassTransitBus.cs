
using Minitube.Upload.Application.Interfaces;

namespace Minitube.Upload.Infrastructure.Services;

public class MassTransitBus : IMessageBus
{
    public async Task PublishAsync<T>(T message) where T : class
    {
        // TODO: Implement MassTransit publishing logic here
    }
}

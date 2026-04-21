namespace Minitube.Upload.Application.Interfaces;
public interface IMessageBus
{
    Task PublishAsync<T>(T message) where T : class;
}

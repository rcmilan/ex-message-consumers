namespace Shared.MQ.Services
{
    public interface IMQService
    {
        T Publish<T>(string exchange, T message) where T : IBaseEvent;
    }
}

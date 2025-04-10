namespace Centuriin.CardGame.Core.Engine;

public interface IEventDispatcher<TEventType>
{
    public void Register<TEvent>(Func<TEvent, CancellationToken, Task> action)
        where TEvent : TEventType;

    public void Unregister<TEvent>(Func<TEvent, CancellationToken, Task> action)
        where TEvent : TEventType;

    public Task PublishAsync<TEvent>(TEvent published, CancellationToken token)
        where TEvent : TEventType;
}

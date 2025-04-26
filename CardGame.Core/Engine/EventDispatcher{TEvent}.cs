namespace Centuriin.CardGame.Core.Engine;

public sealed class EventDispatcher<TEvent> : IEventDispatcher<TEvent>
{
    private readonly Dictionary<Type, List<Delegate>> _actions = [];

    public void Register<TEvent1>(Func<TEvent1, CancellationToken, Task> action)
        where TEvent1 : TEvent
    {
        ArgumentNullException.ThrowIfNull(action);

        if (_actions.ContainsKey(typeof(TEvent1)))
        {
            _actions[typeof(TEvent1)].Add(action);
        }
        else
        {
            _actions.Add(typeof(TEvent1), [action]);
        }
    }

    public void Unregister<TEvent1>(Func<TEvent1, CancellationToken, Task> action)
        where TEvent1 : TEvent
    {
        ArgumentNullException.ThrowIfNull(action);

        if (_actions.ContainsKey(typeof(TEvent1)))
        {
            _actions[typeof(TEvent1)].Remove(action);
        }
    }

    public async Task PublishAsync<TEvent1>(TEvent1 published, CancellationToken token)
        where TEvent1 : TEvent
    {
        ArgumentNullException.ThrowIfNull(published);

        token.ThrowIfCancellationRequested();

        await Task.WhenAll(_actions
            .Where(x => x.Key.IsAssignableFrom(typeof(TEvent1)))
            .SelectMany(x => x.Value)
            .Cast<Func<TEvent1, CancellationToken, Task>>()
            .Select(x => x.Invoke(published, token)));
    }

}

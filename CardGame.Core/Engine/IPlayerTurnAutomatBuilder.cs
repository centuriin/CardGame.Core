using CardGame.Core.Events;

using Centuriin.CardGame.Core;
using Centuriin.CardGame.Core.Engine;

namespace CardGame.Core.Engine;

public interface IPlayerTurnAutomatBuilder
{
    public IPlayerTurnAutomat Build();

    public IPlayerTurnAutomatBuilder UseDispatcher(IEventDispatcher<IGameEvent> dispatcher);

    public IPlayerTurnAutomatBuilder Register<TEvent>(
        Func<TEvent, IPlayerTurnAutomat, CancellationToken, Task> action)
        where TEvent : IGameEvent;

    public IPlayerTurnAutomatBuilder AddPlayers(IReadOnlyCollection<PlayerId> players);

    public IPlayerTurnAutomatBuilder Reset();
}
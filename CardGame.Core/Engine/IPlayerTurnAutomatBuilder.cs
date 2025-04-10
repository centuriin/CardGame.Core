using CardGame.Core.Events;

using Centuriin.CardGame.Core;
using Centuriin.CardGame.Core.Engine;

namespace CardGame.Core.Engine;

public interface IPlayerTurnAutomatBuilder
{
    public IPlayerTurnAutomat Build();

    public IPlayerTurnAutomatBuilder UseDispatcher(IEventDispatcher<IGameEvent> dispatcher);

    public IPlayerTurnAutomatBuilder Register<TEvent>(Func<TEvent, CancellationToken, Task> action)
        where TEvent : IGameEvent;

    public IPlayerTurnAutomatBuilder AddPlayers(IReadOnlyCollection<IPlayer> players);

    public IPlayerTurnAutomatBuilder Reset();

    public Task NextMoveFirstPlayer();

    public Task NextMoveLastPlayer();

    public Task NextMovePlayer(IPlayer player);
}
using CardGame.Core.Events;

using Centuriin.CardGame.Core.Engine.Turn;
using Centuriin.CardGame.Core.Events;

namespace Centuriin.CardGame.Core.Engine.Default.Registrators;

public sealed class PlayerTurnEventRegistrator<TMarker> : IGameRegistrator<TMarker>
    where TMarker : IGameMarker
{
    private readonly IPlayerTurnAutomat<TMarker> _turnAutomat;
    private readonly IEventDispatcher<IGameEvent> _dispatcher;

    public PlayerTurnEventRegistrator(
        IPlayerTurnAutomat<TMarker> turnAutomat,
        IEventDispatcher<IGameEvent> dispatcher)
    {
        ArgumentNullException.ThrowIfNull(turnAutomat);
        _turnAutomat = turnAutomat;

        ArgumentNullException.ThrowIfNull(dispatcher);
        _dispatcher = dispatcher;
    }

    public void Register() => _dispatcher.Register<PlayerMoveEndedEvent>(OnPlayerMoveEnded);

    public void Dispose() => _dispatcher.Unregister<PlayerMoveEndedEvent>(OnPlayerMoveEnded);

    private Task OnPlayerMoveEnded(PlayerMoveEndedEvent @event, CancellationToken token) =>
        _turnAutomat.MoveNextAsync(token);
}

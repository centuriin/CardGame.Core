using CardGame.Core;
using CardGame.Core.Events;

using Centuriin.CardGame.Core.Events;
using Centuriin.CardGame.Core.State;

namespace Centuriin.CardGame.Core.Engine;

public sealed class GameInstance<TMarker> : IGameInstance<TMarker>
    where TMarker : IGameMarker
{
    private readonly GameId _gameId;

    public IEventDispatcher<IGameEvent> Dispatcher { get; }

    public IGameState<TMarker> State { get; }

    public GameInstance(
        GameId gameId,
        IEventDispatcher<IGameEvent> dispatcher,
        IGameState<TMarker> state)
    {
        _gameId = gameId;

        ArgumentNullException.ThrowIfNull(dispatcher);
        Dispatcher = dispatcher;

        ArgumentNullException.ThrowIfNull(state);
        State = state;
    }

    public async Task StartGameAsync(CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        await Dispatcher.PublishAsync(new GameStarted(_gameId), token);
    }

    public async Task DerailGameAsync(DerailedReason reason, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(reason);

        token.ThrowIfCancellationRequested();

        await Dispatcher.PublishAsync(new GameDerailed(_gameId, reason), token);
    }

    public async Task StopGameAsync(CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        await Dispatcher.PublishAsync(new GameStopped(_gameId), token);
    }
}

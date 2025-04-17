using CardGame.Core;
using CardGame.Core.Events;

using Centuriin.CardGame.Core.Events;

namespace Centuriin.CardGame.Core.Engine;

public sealed class GameEngine : IGameEngine
{
    private readonly IEventDispatcher<IGameEvent> _dispatcher;
    private readonly IGameCreator _gameCreator;

    private GameId? _gameId;

    public GameEngine(
        IEventDispatcher<IGameEvent> dispatcher,
        IGameCreator gameCreator)
    {
        ArgumentNullException.ThrowIfNull(dispatcher);
        _dispatcher = dispatcher;

        ArgumentNullException.ThrowIfNull(gameCreator);
        _gameCreator = gameCreator;
    }

    public async Task DerailGameAsync(DerailedReason reason, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(reason);

        cancellationToken.ThrowIfCancellationRequested();

        ThrowIfGameNotStarted();

        await _dispatcher.PublishAsync(new GameDerailed(_gameId!.Value, reason), cancellationToken);
    }

    public async Task StartGameAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _gameId = await _gameCreator.CreateGameAsync(cancellationToken);

        await _dispatcher.PublishAsync(new GameStarted(_gameId.Value), cancellationToken);
    }

    public async Task StopGameAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        ThrowIfGameNotStarted();

        await _dispatcher.PublishAsync(new GameStopped(_gameId!.Value), cancellationToken);
    }

    private void ThrowIfGameNotStarted()
    {
        if (_gameId is null)
        {
            throw new InvalidOperationException("Game is not started.");
        }
    }
}

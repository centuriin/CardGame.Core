using CardGame.Core.DefaultCard;
using CardGame.Core.Events;
using CardGame.Core.State;

using Microsoft.Extensions.Logging;

namespace CardGame.Core.Drunkard;

/// <summary>
/// Движок для игры "Пьяница".
/// </summary>
public sealed class DrunkardEngine : IGameEngine<Card<DefaultCardDescription>>
{
    private const int MAX_PLAYER_COUNT = 2;

    private readonly CardGameStateWithGlobalDeck _gameState;

    private readonly IRepository<IGameEvent> _gameEventRepository;

    private readonly ILogger<DrunkardEngine> _logger;

    public DrunkardEngine(
        CardGameStateWithGlobalDeck gameState,
        IRepository<IGameEvent> gameEventRepository,
        ILogger<DrunkardEngine> logger)
    {
        _gameState = gameState ?? throw new ArgumentNullException(nameof(gameState));

        _gameEventRepository = gameEventRepository 
            ?? throw new ArgumentNullException(nameof(gameEventRepository));

        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    private Task PlayerJoinAsync(Player player, CancellationToken token)
    {
        if (_gameState.PlayersCount < MAX_PLAYER_COUNT)
            _gameState.Join(player);
        else
            throw new InvalidOperationException("The maximum players count is already joined.");

        return Task.CompletedTask;
    }

    private static Task StartGameAsync(CancellationToken token) => Task.CompletedTask;

    public async Task<bool> TryHandleEventAsync(IGameEvent gameEvent, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(gameEvent);

        token.ThrowIfCancellationRequested();

        try
        {
            await _gameEventRepository.AddAsync(gameEvent, token);

            await (gameEvent switch
            {
                PlayerJoinedEvent e => PlayerJoinAsync(e.Player, token),

                GameStartedEvent e => StartGameAsync(token),

                _ => throw new InvalidOperationException(
                    $"Game event {gameEvent.GetType().Name} not supported.")
            });

            await _gameEventRepository.SaveAsync(token);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Cannot handle event");
            return false;
        }
    }
}

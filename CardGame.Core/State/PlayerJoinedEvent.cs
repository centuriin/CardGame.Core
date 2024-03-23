using CardGame.Core.Events;

namespace CardGame.Core.State;

public sealed class PlayerJoinedEvent : IGameEvent
{
    public GameId GameId { get; }

    public Player Player { get; }

    public PlayerJoinedEvent(GameId gameId, Player player)
    {
        GameId = gameId;
        Player = player ?? throw new ArgumentNullException(nameof(player));
    }
}

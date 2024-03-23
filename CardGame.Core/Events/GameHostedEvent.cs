namespace CardGame.Core.Events;

public sealed class GameHostedEvent
{
    public GameId GameId { get; }

    public GameHostedEvent(GameId gameId)
    {
        GameId = gameId;
    }
}

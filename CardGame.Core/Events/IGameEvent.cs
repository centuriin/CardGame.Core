namespace CardGame.Core.Events;

public interface IGameEvent
{
    public GameId GameId { get; }
}

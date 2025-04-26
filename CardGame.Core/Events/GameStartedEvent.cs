namespace CardGame.Core.Events;

public sealed record class GameStartedEvent(GameId GameId) : IGameEvent;
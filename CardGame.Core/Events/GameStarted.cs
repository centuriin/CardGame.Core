namespace CardGame.Core.Events;

public sealed record class GameStarted(GameId GameId) : IGameEvent;
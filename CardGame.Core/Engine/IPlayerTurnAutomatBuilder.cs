using Centuriin.CardGame.Core;
using Centuriin.CardGame.Core.Engine;

namespace CardGame.Core.Engine;

public interface IPlayerTurnAutomatBuilder
{
    public IPlayerTurnAutomatBuilder AddPlayers(IReadOnlyCollection<IPlayer> players);
    public IPlayerTurnAutomat Build();
    public IPlayerTurnAutomatBuilder Register<TEvent>(Func<Task> action);
    public IPlayerTurnAutomatBuilder Reset();

    public Task MoveToFirstPlayer();
    public Task MoveToLastPlayer();
    public Task MoveToPlayer(IPlayer player);
}
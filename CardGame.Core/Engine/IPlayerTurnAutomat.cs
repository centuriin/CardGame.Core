namespace Centuriin.CardGame.Core.Engine;

public interface IPlayerTurnAutomat
{
    public IPlayer? PlayerTurn { get; }

    public Task MoveNext();

    public Task MoveToFirstPlayer();

    public Task MoveToLastPlayer();

    public Task MoveToPlayer(IPlayer player);
}

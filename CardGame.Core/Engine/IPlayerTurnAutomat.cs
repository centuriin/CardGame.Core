using CardGame.Core;

namespace Centuriin.CardGame.Core.Engine;

public interface IPlayerTurnAutomat
{
    public PlayerId? PlayerTurn { get; }

    public Task MoveNext();

    public Task MoveToFirstPlayer();

    public Task MoveToLastPlayer();

    public Task MoveToPlayer(PlayerId player);
}

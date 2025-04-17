namespace Centuriin.CardGame.Core.Engine;

public interface IPlayerTurnAutomat
{
    public PlayerId? PlayerTurn { get; }

    public Task MoveNextAsync(CancellationToken token);

    public Task MoveToFirstPlayerAsync(CancellationToken token);

    public Task MoveToLastPlayerAsync(CancellationToken token);

    public Task MoveToPlayerAsync(PlayerId player, CancellationToken token);
}

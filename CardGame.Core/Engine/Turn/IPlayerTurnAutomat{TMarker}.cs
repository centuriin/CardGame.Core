namespace Centuriin.CardGame.Core.Engine.Turn;

public interface IPlayerTurnAutomat<TMarker>
    where TMarker : IGameMarker
{
    public PlayerId? PlayerTurn { get; }

    public Task MoveNextAsync(CancellationToken token);

    public Task MoveToFirstPlayerAsync(CancellationToken token);

    public Task MoveToLastPlayerAsync(CancellationToken token);

    public Task MoveToPlayerAsync(PlayerId player, CancellationToken token);
}

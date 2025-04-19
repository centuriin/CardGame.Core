namespace Centuriin.CardGame.Core.Engine.Turn;

public interface IPlayerTurnAutomatBuilder<TMarker>
    where TMarker : IGameMarker
{
    public IPlayerTurnAutomat<TMarker> Build();

    public IPlayerTurnAutomatBuilder<TMarker> AddPlayers(IReadOnlyCollection<PlayerId> players);

    public IPlayerTurnAutomatBuilder<TMarker> Reset();
}
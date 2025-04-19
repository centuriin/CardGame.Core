namespace Centuriin.CardGame.Core.Engine.Factories;

public interface IGameInstanceFactory<TMarker>
    where TMarker : IGameMarker
{
    public Task<IGameInstance<TMarker>> CreateAsync(CancellationToken token);
}

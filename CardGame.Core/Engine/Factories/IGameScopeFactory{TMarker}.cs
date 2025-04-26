using Centuriin.CardGame.Core.Engine.GameRoot;

namespace Centuriin.CardGame.Core.Engine.Factories;

public interface IGameScopeFactory<TMarker>
    where TMarker : IGameMarker
{
    public Task<IGameScope<TMarker>> CreateGameScopeAsync(CancellationToken token);
}

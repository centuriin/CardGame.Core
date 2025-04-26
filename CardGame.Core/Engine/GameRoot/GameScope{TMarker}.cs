using Microsoft.Extensions.DependencyInjection;

namespace Centuriin.CardGame.Core.Engine.GameRoot;

internal sealed class GameScope<TMarker> : IGameScope<TMarker>
    where TMarker : IGameMarker
{
    private readonly IServiceScope _serviceScope;

    public IGameInstance<TMarker> GameInstance { get; }

    public GameScope(IServiceScope serviceScope, IGameInstance<TMarker> gameInstance)
    {
        ArgumentNullException.ThrowIfNull(serviceScope);
        _serviceScope = serviceScope;

        ArgumentNullException.ThrowIfNull(gameInstance);
        GameInstance = gameInstance;
    }

    public void Dispose() => _serviceScope.Dispose();
}

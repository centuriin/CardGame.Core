using Centuriin.CardGame.Core.Engine.GameRoot;

using Microsoft.Extensions.DependencyInjection;

namespace Centuriin.CardGame.Core.Engine.Factories;

public sealed class GameScopeFactory<TMarker> : IGameScopeFactory<TMarker>
    where TMarker : IGameMarker
{
    private readonly IServiceScopeFactory _scopeFactory;

    public GameScopeFactory(
        IServiceScopeFactory scopeFactory)
    {
        ArgumentNullException.ThrowIfNull(scopeFactory);
        _scopeFactory = scopeFactory;
    }

    public async Task<IGameScope<TMarker>> CreateGameScopeAsync(CancellationToken token)
    {
        var scope = _scopeFactory.CreateScope();

        var gameInstance = await scope.ServiceProvider
            .GetRequiredService<IGameInstanceFactory<TMarker>>()
            .CreateAsync(token);

        return new GameScope<TMarker>(scope, gameInstance);
    }
}

using Microsoft.Extensions.DependencyInjection;

namespace Centuriin.CardGame.Core.Engine.Factories;

public sealed class GameInstanceFactory<TMarker> : IGameInstanceFactory<TMarker>
    where TMarker : IGameMarker
{
    private readonly IEnumerable<IGameRegistrator<TMarker>> _registrators;
    private readonly IGameIdGenerator _gameIdGenerator;
    private IServiceProvider _serviceProvider;

    public GameInstanceFactory(
        IEnumerable<IGameRegistrator<TMarker>> registrators,
        IGameIdGenerator gameIdGenerator,
        IServiceProvider serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(registrators);
        _registrators = registrators;

        ArgumentNullException.ThrowIfNull(gameIdGenerator);
        _gameIdGenerator = gameIdGenerator;

        ArgumentNullException.ThrowIfNull(serviceProvider);
        _serviceProvider = serviceProvider;
    }

    public async Task<IGameInstance<TMarker>> CreateAsync(CancellationToken token)
    {
        var gameId = await _gameIdGenerator.NextGameIdAsync(token);

        foreach (var registrator in _registrators)
            registrator.Register();

        return ActivatorUtilities.CreateInstance<IGameInstance<TMarker>>(
            _serviceProvider,
            gameId);
    }
}

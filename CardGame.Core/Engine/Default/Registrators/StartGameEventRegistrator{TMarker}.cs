using CardGame.Core.Events;

namespace Centuriin.CardGame.Core.Engine.Default.Registrators;

public sealed class StartGameEventRegistrator<TMarker> : IGameRegistrator<TMarker>
    where TMarker : IGameMarker
{
    private readonly IEventDispatcher<IGameEvent> _dispatcher;
    private readonly IEnumerable<ICardLoader<TMarker>> _cardLoaders;
    //private readonly ICardHolder _cardHolder;

    public StartGameEventRegistrator(
        IEventDispatcher<IGameEvent> dispatcher,
        IEnumerable<ICardLoader<TMarker>> cardLoaders)
    {
        ArgumentNullException.ThrowIfNull(dispatcher);
        _dispatcher = dispatcher;

        ArgumentNullException.ThrowIfNull(cardLoaders);
        _cardLoaders = cardLoaders;
    }

    public void Register() => _dispatcher.Register<GameStartedEvent>(CreateOneCardSpaceAsync);
    public void Dispose() => _dispatcher.Unregister<GameStartedEvent>(CreateOneCardSpaceAsync);

    private async Task CreateOneCardSpaceAsync(GameStartedEvent @event, CancellationToken token)
    {
        foreach (var cardLoader in _cardLoaders)
        {
            var cards = await cardLoader.LoadAsync(token);


        }

    }
}

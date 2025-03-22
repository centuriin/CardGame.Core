using CardGame.Core.Events;

using Centuriin.CardGame.Core.Cards;

namespace CardGame.Core;

public sealed class CardGame<TCard>
    where TCard : ICard
{
    private readonly GameId _gameId;

    private readonly ICardLoader<TCard> _cardLoader;
    private readonly IGameEngine<TCard> _engine;

    public CardGame(
        GameId gameId,
        ICardLoader<TCard> cardLoader,
        IGameEngine<TCard> engine)
    {
        _gameId = gameId;
        _cardLoader = cardLoader ?? throw new ArgumentNullException(nameof(cardLoader));
        _engine = engine ?? throw new ArgumentNullException(nameof(engine));
    }

    public async Task<bool> TryHandleEventAsync(IGameEvent gameEvent, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(gameEvent);

        token.ThrowIfCancellationRequested();

        return true;
    }
}

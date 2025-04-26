using CardGame.Core;
using CardGame.Core.Events;

using Centuriin.CardGame.Core.Cards;

namespace Centuriin.CardGame.Core.Events;

public sealed class PlayerTookCardsEvent : IGameEvent
{
    public GameId GameId { get; }

    public PlayerId PlayerId { get; set; }

    public IReadOnlyCollection<ICard> Cards { get; }

    public PlayerTookCardsEvent(
        GameId gameId,
        PlayerId playerId,
        IReadOnlyCollection<ICard> cards)
    {
        GameId = gameId;
        PlayerId = playerId;

        ArgumentNullException.ThrowIfNull(cards);
        Cards = cards;
    }
}

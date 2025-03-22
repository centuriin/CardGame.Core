using Centuriin.CardGame.Core.Cards;

namespace CardGame.Core.Events;

public sealed class CardDroppingEvent<TCard> : IGameEvent
    where TCard : ICard
{
    public GameId GameId { get; }

    public TCard Card { get; }

    public Player Player { get; }

    public CardDroppingEvent(GameId id, TCard card, Player player)
    {
        GameId = id;
        Card = card ?? throw new ArgumentNullException(nameof(card));
        Player = player ?? throw new ArgumentNullException(nameof(player));
    }
}

using System.ComponentModel;

namespace CardGame.Core.DefaultCard;

public class DefaultCardDescription : ICardDescription
{
    public CardSuit Suit { get; }

    public CardType Type { get; }

    public DefaultCardDescription(CardSuit suit, CardType type)
    {
        if (!Enum.IsDefined(suit))
        {
            throw new InvalidEnumArgumentException(nameof(suit), (byte)suit, typeof(CardSuit));
        }
        Suit = suit;

        if (!Enum.IsDefined(type))
        {
            throw new InvalidEnumArgumentException(nameof(type), (byte)type, typeof(CardType));
        }
        Type = type;
    }

    public bool Equals(ICardDescription? other) => 
        other is DefaultCardDescription description &&
        description.Suit == Suit &&
        description.Type == Type;
}

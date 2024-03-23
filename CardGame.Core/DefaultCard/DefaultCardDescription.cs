using System.ComponentModel;

namespace CardGame.Core.DefaultCard;

/// <summary>
/// Представляет описание обычных игральных карт от 
/// <see cref="CardType.Two"> до <see cref="CardType.Ace"> четырех мастей <see cref="CardSuit">.
/// </summary>
public class DefaultCardDescription : ICardDescription
{
    /// <summary>
    /// Масть карты.
    /// </summary>
    public CardSuit Suit { get; }

    /// <summary>
    /// Тип карты.
    /// </summary>
    public CardType Type { get; }

    /// <summary>
    /// Создает новый объект типа <see cref="DefaultCardDescription"/>.
    /// </summary>
    /// <param name="suit">
    /// Масть карты.
    /// </param>
    /// <param name="type">
    /// Тип карты.
    /// </param>
    /// <exception cref="InvalidEnumArgumentException">
    /// Если перечисления имели неверные значения.
    /// </exception>
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

    /// <inheritdoc/>
    public bool Equals(ICardDescription? other) => 
        other is DefaultCardDescription description &&
        description.Suit == Suit &&
        description.Type == Type;
}

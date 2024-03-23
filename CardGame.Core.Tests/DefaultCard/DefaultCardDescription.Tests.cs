using System.ComponentModel;

using CardGame.Core.DefaultCard;

using FluentAssertions;

using Xunit;

namespace CardGame.Core.Tests.DefaultCard;

public class DefaultCardDescriptionTests
{
    [Fact(DisplayName = "Can create.")]
    [Trait("Category", "Unit")]
    public void CanCreate()
    {
        // Arrange
        var suit = CardSuit.Diamonds;
        var type = CardType.Queen;

        // Act
        var descritpion = new DefaultCardDescription(suit, type);

        // Assert
        descritpion.Suit.Should().Be(suit);
        descritpion.Type.Should().Be(type);
    }

    [Fact(DisplayName = "Cannot create with invalid suit.")]
    [Trait("Category", "Unit")]
    public void CanNotCreateWithInvalidSuit()
    {
        // Arrange
        var suit = (CardSuit)byte.MaxValue;
        var type = CardType.Queen;

        // Act
        var exception = Record.Exception(() =>
            new DefaultCardDescription(suit, type));

        // Assert
        exception.Should().BeOfType<InvalidEnumArgumentException>();
    }

    [Fact(DisplayName = "Cannot create with invalid card type.")]
    [Trait("Category", "Unit")]
    public void CanNotCreateWithInvalidCardType()
    {
        // Arrange
        var suit = CardSuit.Diamonds;
        var type = (CardType)byte.MaxValue;

        // Act
        var exception = Record.Exception(() =>
            new DefaultCardDescription(suit, type));

        // Assert
        exception.Should().BeOfType<InvalidEnumArgumentException>();
    }

    [Theory(DisplayName = "Can equal description.")]
    [MemberData(nameof(EqualsData))]
    [Trait("Category", "Unit")]
    public void CanEqualDescription(
        DefaultCardDescription first,
        DefaultCardDescription second,
        bool expectedResult)
    {
        // Act
        var actual = first.Equals(second);

        // Assert
        actual.Should().Be(expectedResult);
    }

    public static TheoryData<DefaultCardDescription, DefaultCardDescription, bool> EqualsData = new()
    {
        {
            new DefaultCardDescription(CardSuit.Diamonds, CardType.Two),
            new DefaultCardDescription(CardSuit.Diamonds, CardType.Two),
            true
        },
        {
            new DefaultCardDescription(CardSuit.Diamonds, CardType.Jack),
            new DefaultCardDescription(CardSuit.Diamonds, CardType.Two),
            false
        },
        {
            new DefaultCardDescription(CardSuit.Spades, CardType.Two),
            new DefaultCardDescription(CardSuit.Diamonds, CardType.Two),
            false
        },
    };
}

﻿using CardGame.Core.DefaultCard;

namespace CardGame.Core;

public static class CardSets
{
    private const CardSuit FIRST_SUIT = CardSuit.Clubs;
    private const int SUIT_COUNT = 4;

    private const CardType FIRST_DEFAULT36_CARD = CardType.Six;
    private const int DEFAULT36_PACK_COUNT = 9;

    private const CardType FIRST_DEFAULT52_CARD = CardType.Two;
    private const int DEFAULT52_PACK_COUNT = 4;

    public static IReadOnlyList<Card<DefaultCardDescription>> Default36Set { get; } =
        Enumerable.Range((int)FIRST_SUIT, SUIT_COUNT)
            .Select(suit =>
                Enumerable.Range((int)FIRST_DEFAULT36_CARD, DEFAULT36_PACK_COUNT)
                    .Select(type =>
                        new Card<DefaultCardDescription>(
                            new((CardSuit)suit, (CardType)type))))
            .SelectMany(x => x)
            .ToList();

    public static IReadOnlyList<Card<DefaultCardDescription>> Default52Set { get; } =
        Enumerable.Range((int)FIRST_SUIT, SUIT_COUNT)
            .Select(suit =>
                Enumerable.Range((int)FIRST_DEFAULT52_CARD, DEFAULT52_PACK_COUNT)
                    .Select(type =>
                        new Card<DefaultCardDescription>(
                            new((CardSuit)suit, (CardType)type))))
            .SelectMany(x => x)
            .Concat(Default36Set)
            .ToList();
}

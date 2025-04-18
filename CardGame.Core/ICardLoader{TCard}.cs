﻿using Centuriin.CardGame.Core.Cards;

namespace Centuriin.CardGame.Core;
public interface ICardLoader<TCard>
    where TCard : ICard
{
    public IReadOnlyList<TCard> LoadDeck();
}

using Centuriin.CardGame.Core.Cards;

namespace Centuriin.CardGame.Core.State;

public interface ICardHolder
{
    Task AddToPlayerAsync(PlayerId playerId, IEnumerable<ICard> cards, CancellationToken token);
    Task AddToSharedSpaceAsync(IEnumerable<ICard> cards, CancellationToken token);
    Task AddToSpaceAsync(CardHolderSpaceId spaceId, IEnumerable<ICard> cards, CancellationToken token);
    Task<CardHolderSpaceId> CreateNewCardSpaceAsync(CancellationToken token);
    Task RemoveFromPlayerAsync(PlayerId playerId, IEnumerable<ICard> cards, CancellationToken token);
    Task RemoveFromSharedSpaceAsync(IEnumerable<ICard> cards, CancellationToken token);
}
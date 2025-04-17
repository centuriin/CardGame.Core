using Centuriin.CardGame.Core.Cards;

namespace Centuriin.CardGame.Core.State;
public interface ICardHolder
{
    Task AddToPlayer(PlayerId playerId, IEnumerable<ICard> cards, CancellationToken token);
    Task AddToSharedSpace(IEnumerable<ICard> cards, CancellationToken token);
    Task AddToSpace(CardHolderSpaceId spaceId, IEnumerable<ICard> cards, CancellationToken token);
    Task<CardHolderSpaceId> CreateNewCardSpace(CancellationToken token);
    Task RemoveFromPlayer(PlayerId playerId, IEnumerable<ICard> cards, CancellationToken token);
    Task RemoveFromSharedSpace(IEnumerable<ICard> cards, CancellationToken token);
}
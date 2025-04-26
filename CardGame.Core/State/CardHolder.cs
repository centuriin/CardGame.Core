using Centuriin.CardGame.Core.Cards;

namespace Centuriin.CardGame.Core.State;

public sealed class CardHolder : ICardHolder
{
    private List<ICard> SharedCards { get; } = [];

    private Dictionary<PlayerId, List<ICard>> PlayerCards { get; } = [];

    private Dictionary<CardHolderSpaceId, List<ICard>> CardSpaces { get; } = [];

    public Task AddToSharedSpaceAsync(IEnumerable<ICard> cards, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(cards);

        SharedCards.AddRange(cards);

        return Task.CompletedTask;
    }

    public Task RemoveFromSharedSpaceAsync(IEnumerable<ICard> cards, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(cards);

        foreach (var card in cards)
        {
            SharedCards.Remove(card);
        }

        return Task.CompletedTask;
    }

    public Task<CardHolderSpaceId> CreateNewCardSpaceAsync(CancellationToken token) =>
        Task.FromResult(new CardHolderSpaceId(CardSpaces.Count + 1));

    public Task AddToSpaceAsync(CardHolderSpaceId spaceId, IEnumerable<ICard> cards, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(cards);

        if (!CardSpaces.TryGetValue(spaceId, out var list))
        {
            throw new InvalidOperationException(
                $"Space with id {spaceId.Value} is not exists.");
        }

        list.AddRange(cards);
        return Task.CompletedTask;
    }

    public Task AddToPlayerAsync(PlayerId playerId, IEnumerable<ICard> cards, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(cards);

        if (PlayerCards.TryGetValue(playerId, out var list))
        {
            list.AddRange(cards);
        }
        else
        {
            PlayerCards.Add(playerId, [.. cards]);
        }

        return Task.CompletedTask;
    }

    public Task RemoveFromPlayerAsync(PlayerId playerId, IEnumerable<ICard> cards, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(cards);

        if (!PlayerCards.TryGetValue(playerId, out var list))
        {
            throw new InvalidOperationException(
                $"Player with id {playerId.Value} is not exists.");
        }

        foreach (var card in cards)
        {
            list.Remove(card);
        }

        return Task.CompletedTask;
    }
}

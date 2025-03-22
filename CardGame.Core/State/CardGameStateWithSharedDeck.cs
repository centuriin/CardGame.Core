using Centuriin.CardGame.Core.Cards;

namespace CardGame.Core.State;

/// <summary>
/// Состояние игры с одной общей колодой карт.
/// </summary>
public sealed class CardGameStateWithSharedDeck : CardGameStateBase
{
    private readonly List<ICard> _deck;

    public CardGameStateWithSharedDeck(IEnumerable<ICard> deck)
    {
        ArgumentNullException.ThrowIfNull(deck);
        _deck = new(deck);
    }

    public void Join(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);

        if (CurrentGameState is not GameState.None)
            throw new InvalidOperationException("Can't join player if game is already created.");

        _players[player.Id] = player;
        _playerCards[player.Id] = [];
        _desktopPlayerCards[player.Id] = [];
    }

    public void AddCardsToDesktopFromDeck(IEnumerable<ICard> cards)
    {
        ArgumentNullException.ThrowIfNull(cards);

        AddCardsToDesktop(cards);

        RemoveFromCollection(_deck, cards);
    }

    public void RemoveFromDeck(IEnumerable<ICard> cards)
    {
        ArgumentNullException.ThrowIfNull(cards);

        RemoveFromCollection(_deck, cards);
    }
}

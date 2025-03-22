using Centuriin.CardGame.Core.Cards;

namespace CardGame.Core.State;

/// <summary>
/// Состояние игры с индивидуальными колодами карт для каждого игрока.
/// </summary>
public sealed class CardGameStateWithDeckByPlayer : CardGameStateBase
{
    private readonly Dictionary<Id<Player>, List<ICard>> _deckByPlayer = [];

    public void Join(Player player, IEnumerable<ICard> playerDeck)
    {
        ArgumentNullException.ThrowIfNull(player);
        ArgumentNullException.ThrowIfNull(playerDeck);

        if (CurrentGameState is not GameState.None)
            throw new InvalidOperationException("Can't join player if game is already created.");

        _deckByPlayer[player.Id] = new List<ICard>(playerDeck);
        _playerCards[player.Id] = [];
        _desktopPlayerCards[player.Id] = [];
    }

    public void AddCardsToDesktopFromDeck(Id<Player> playerId, IEnumerable<ICard> cards)
    {
        ArgumentNullException.ThrowIfNull(cards);

        RemoveFromDeck(playerId, cards);

        AddCardsToDesktop(cards);
    }

    public void RemoveFromDeck(Id<Player> playerId, IEnumerable<ICard> cards)
    {
        ArgumentNullException.ThrowIfNull(cards);

        if (!_deckByPlayer.TryGetValue(playerId, out var playerDeck))
            throw new InvalidOperationException($"Player with id: {playerId.Value} is not in the game.");

        RemoveFromCollection(playerDeck, cards);
    }
}

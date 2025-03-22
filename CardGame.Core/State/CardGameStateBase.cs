using System.Runtime.CompilerServices;

using Centuriin.CardGame.Core.Cards;

namespace CardGame.Core.State;

/// <summary>
/// Базовый класс, содержащий состояние игры.
/// </summary>
public abstract class CardGameStateBase
{
    protected readonly Dictionary<Id<Player>, Player> _players = [];
    protected readonly Dictionary<Id<Player>, List<ICard>> _playerCards = [];
    protected readonly Dictionary<Id<Player>, List<ICard>> _desktopPlayerCards = [];
    protected readonly List<ICard> _desktopCards = [];

    public int PlayersCount => _players.Count;

    public IReadOnlyDictionary<Id<Player>, Player> Players => _players;

    public GameState CurrentGameState { get; private set; } = GameState.None;

    protected static void RemoveFromCollection(
        ICollection<ICard> collection,
        IEnumerable<ICard> cards,
        [CallerArgumentExpression(nameof(collection))] string collectionName = null!)
    {
        if (!cards.All(collection.Contains))
            throw new InvalidOperationException($"{collectionName} don't have this cards");

        foreach (var card in cards)
            collection.Remove(card);
    }

    public void StartGame()
    {
        if (CurrentGameState is not GameState.None)
            throw new InvalidOperationException("Game is already initialized.");

        CurrentGameState = GameState.Started;
    }

    public void StopGame() => CurrentGameState = GameState.Ended;

    public void AddCardsToPlayer(Id<Player> playerId, IEnumerable<ICard> cards)
    {
        ArgumentNullException.ThrowIfNull(cards);

        if (!_playerCards.TryGetValue(playerId, out var playerCards))
            throw new InvalidOperationException($"Player with id: {playerId.Value} is not in the game.");

        playerCards.AddRange(cards);
    }

    public void AddCardsToDesktopFromPlayer(Id<Player> playerId, IEnumerable<ICard> cards)
    {
        ArgumentNullException.ThrowIfNull(cards);

        RemoveFromPlayer(playerId, cards);

        _desktopPlayerCards[playerId].AddRange(cards);
    }

    public void AddCardsToDesktop(IEnumerable<ICard> cards)
    {
        ArgumentNullException.ThrowIfNull(cards);

        _desktopCards.AddRange(cards);
    }

    public void RemoveFromPlayer(Id<Player> playerId, IEnumerable<ICard> cards)
    {
        ArgumentNullException.ThrowIfNull(cards);

        if (!_playerCards.TryGetValue(playerId, out var playerCards))
            throw new InvalidOperationException($"Player with id: {playerId.Value} is not in the game.");

        RemoveFromCollection(playerCards, cards);
    }
}

using CardGame.Core.Events;

using Centuriin.CardGame.Core;
using Centuriin.CardGame.Core.Engine;

namespace CardGame.Core.Engine;

public sealed class PlayerTurnAutomat : IPlayerTurnAutomat
{
    private IEnumerator<IPlayer> _enumerator;

    private IEventDispatcher<IGameEvent>? Dispatcher { get; set; }
    private LinkedList<IPlayer> Players { get; } = [];
    private LinkedListNode<IPlayer>? Current { get; set; }

    public IPlayer? PlayerTurn => Current?.ValueRef;

    private PlayerTurnAutomat()
    {
        _enumerator = GetEnumerator();
    }

    private IEnumerator<IPlayer> GetEnumerator()
    {
        Current = Players.First!;

        do
        {
            yield return Current.ValueRef;

            Current = Current.Next ?? Players.First;
        }
        while (Current is not null);
    }

    /// <inheritdoc/>
    public Task MoveNext()
    {
        _ = _enumerator.MoveNext();
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task MoveToPlayer(IPlayer player)
    {
        ArgumentNullException.ThrowIfNull(player);

        Current = Players.Find(player)
            ?? throw new InvalidOperationException("Player not found.");

        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task MoveToFirstPlayer()
    {
        Current = Players.First;
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task MoveToLastPlayer()
    {
        Current = Players.Last;
        return Task.CompletedTask;
    }

    public sealed class AutomatBuilder : IPlayerTurnAutomatBuilder
    {
        private const int MIN_PLAYERS_COUNT = 2;
        private bool _isBuilt = false;

        private readonly HashSet<IPlayer> _players = new();

        private PlayerTurnAutomat Automat { get; } = new();

        private void ThrowIfBuilt()
        {
            if (_isBuilt)
            {
                throw new InvalidOperationException(
                    "Automat is already built, use 'Reset()'.");
            }
        }

        public IPlayerTurnAutomat Build()
        {
            if (Automat.Players.Count < MIN_PLAYERS_COUNT)
            {
                throw new InvalidOperationException(
                    $"Players count must be great or equal {MIN_PLAYERS_COUNT}.");
            }

            _isBuilt = true;
            return Automat;
        }

        public IPlayerTurnAutomatBuilder Reset() => new AutomatBuilder();

        public IPlayerTurnAutomatBuilder AddPlayers(IReadOnlyCollection<IPlayer> players)
        {
            ArgumentNullException.ThrowIfNull(players);

            ThrowIfBuilt();

            foreach (var p in players)
            {
                if (_players.Add(p))
                {
                    Automat.Players.AddLast(p);
                }
            }

            return this;
        }

        public IPlayerTurnAutomatBuilder UseDispatcher(IEventDispatcher<IGameEvent> dispatcher)
        {
            ArgumentNullException.ThrowIfNull(dispatcher);

            ThrowIfBuilt();

            Automat.Dispatcher = dispatcher;

            return this;
        }

        public IPlayerTurnAutomatBuilder Register<TEvent>(
            Func<TEvent, IPlayerTurnAutomat, CancellationToken, Task> action)
            where TEvent : IGameEvent
        {
            ArgumentNullException.ThrowIfNull(action);

            ThrowIfBuilt();

            if (Automat.Dispatcher is null)
            {
                throw new InvalidOperationException("Dispatcher must be settled.");
            }

            Automat.Dispatcher.Register<TEvent>((e, t) => 
                action.Invoke(e, Automat, t));

            return this;
        }
    }
}

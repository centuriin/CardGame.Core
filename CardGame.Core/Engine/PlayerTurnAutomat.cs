using System.Collections;

using CardGame.Core.Events;

using Centuriin.CardGame.Core;
using Centuriin.CardGame.Core.Engine;

namespace CardGame.Core.Engine;

public sealed class PlayerTurnAutomat : IPlayerTurnAutomat
{
    private List<Action> UnregisterActions { get; } = [];
    private IEventDispatcher<IGameEvent>? Dispatcher { get; set; }
    private LinkedList<IPlayer> Players { get; } = [];
    private LinkedListNode<IPlayer>? Current { get; set; }

    private bool _isDisposed = false;

    public static IPlayerTurnAutomatBuilder Builder { get; } = new AutomatBuilder();

    private PlayerTurnAutomat() { }

    public IEnumerator<IPlayer> GetEnumerator()
    {
        ObjectDisposedException.ThrowIf(_isDisposed, this);

        Current = Players.First!;

        do
        {
            yield return Current.ValueRef;

            Current = Current.Next ?? Players.First;
        }
        while(Current is not null);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <inheritdoc/>
    public void Dispose()
    {
        if (Dispatcher is not null)
        {
            foreach (var action in UnregisterActions)
            {
                action.Invoke();
            }

            UnregisterActions.Clear();
        }

        _isDisposed = true;
    }

    public sealed class AutomatBuilder : IPlayerTurnAutomatBuilder
    {
        private const int MIN_PLAYERS_COUNT = 2;
        private bool _isBuilt = false;

        private PlayerTurnAutomat Automat { get; set; } = new();

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
            _isBuilt = true;
            return Automat;
        }

        public IPlayerTurnAutomatBuilder Reset() => new AutomatBuilder();

        public IPlayerTurnAutomatBuilder AddPlayers(IReadOnlyCollection<IPlayer> players)
        {
            ArgumentNullException.ThrowIfNull(players);

            ThrowIfBuilt();

            if (players.Count < MIN_PLAYERS_COUNT)
            {
                throw new InvalidOperationException(
                    $"Players count must be great or equal {MIN_PLAYERS_COUNT}.");
            }

            foreach (var p in players)
            {
                Automat.Players.AddLast(p);
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

        public IPlayerTurnAutomatBuilder Register<TEvent>(Func<TEvent, CancellationToken, Task> action)
            where TEvent : IGameEvent
        {
            ArgumentNullException.ThrowIfNull(action);

            ThrowIfBuilt();

            if (Automat.Dispatcher is null)
            {
                throw new InvalidOperationException("Dispatcher must be settled.");
            }

            Automat.Dispatcher.Register(action);

            Automat.UnregisterActions.Add(() => Automat.Dispatcher.Unregister(action));

            return this;
        }

        public Task NextMovePlayer(IPlayer player)
        {
            ArgumentNullException.ThrowIfNull(player);

            Automat.Current = Automat.Players.Find(player)
                ?? throw new InvalidOperationException("Player not found.");

            return Task.CompletedTask;
        }

        public Task NextMoveFirstPlayer()
        {
            Automat.Current = Automat.Players.First;
            return Task.CompletedTask;
        }

        public Task NextMoveLastPlayer()
        {
            Automat.Current = Automat.Players.Last;
            return Task.CompletedTask;
        }
    }
}

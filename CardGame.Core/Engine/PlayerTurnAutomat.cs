using System.Collections;
using System.Diagnostics.CodeAnalysis;

using CardGame.Core.Events;

using Centuriin.CardGame.Core;
using Centuriin.CardGame.Core.Engine;

namespace CardGame.Core.Engine;

public sealed class PlayerTurnAutomat : IPlayerTurnAutomat
{
    private const int MIN_TURN_COUNT = 1;

    private readonly Dictionary<Type, Action<AutomatContext>> _eventsActions = [];

    public static AutomatBuilder Builder { get; } = new();

    public int CurrentLoop { get; private set; } = 1;

    public Id<Player> Current => CurrentNode.ValueRef.PlayerId;

    private LinkedList<PlayerIdAndTurnCount> Players { get; } = new();

    private LinkedListNode<PlayerIdAndTurnCount> CurrentNode { get; set; } = null!;

    private PlayerTurnAutomat() { }

    public Id<Player> MoveNext()
    {
        if (CurrentNode.Value.CurrentTurnCount > 1)
        {
            CurrentNode.ValueRef.CurrentTurnCount--;

            return CurrentNode.Value.PlayerId;
        }

        CurrentNode.ValueRef.CurrentTurnCount = CurrentNode.ValueRef.InitialTurnCount;

        if (CurrentNode == Players.Last)
        {
            CurrentNode = Players.First!;
            CurrentLoop++;
        }
        else
            CurrentNode = CurrentNode.Next!;

        return CurrentNode.Value.PlayerId;
    }

    public Id<Player> MoveNext(IGameEvent gameEvent)
    {
        ArgumentNullException.ThrowIfNull(gameEvent);

        if (_eventsActions.TryGetValue(gameEvent.GetType(), out var action))
        {
            action.Invoke(new(this));

            return Current;
        }
        else
            throw new InvalidOperationException("This game event can't support.");
    }

    public void Remove(Id<Player> id) => Players.Remove(new PlayerIdAndTurnCount(id));

    private void Register<T>(Action<AutomatContext> action)
        where T : IGameEvent
    {
        if (_eventsActions.ContainsKey(typeof(T)))
        {
            throw new InvalidOperationException("On this game event is already registered action.");
        }

        _eventsActions[typeof(T)] = action;
    }

    public IEnumerator<IPlayer> GetEnumerator() => throw new NotImplementedException();
    IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();

    public sealed class AutomatBuilder
    {
        private PlayerTurnAutomat _automat = new();

        public AutomatBuilder StartWith(Id<Player> id, int turnCount = 1)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(turnCount, MIN_TURN_COUNT);

            ThrowIfAlreadyExist(id);

            _automat.CurrentNode = _automat.Players.AddFirst(new PlayerIdAndTurnCount(id, turnCount));
            
            return this;
        }

        public AutomatBuilder SetNext(Id<Player> id, int turnCount = 1)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(turnCount, MIN_TURN_COUNT);

            ThrowIfAlreadyExist(id);

            _ = _automat.Players.AddLast(new PlayerIdAndTurnCount(id, turnCount));
            
            return this;
        }

        public AutomatBuilder AddReactionOn<T>(Action<AutomatContext> action)
            where T : IGameEvent
        {
            ArgumentNullException.ThrowIfNull(action);

            _automat.Register<T>(action);
            
            return this;
        }

        public PlayerTurnAutomat Build() => _automat;

        public void Reset() => _automat = new();

        private void ThrowIfAlreadyExist(Id<Player> id)
        {
            if (_automat.Players.Find(new(id)) is not null)
            {
                throw new InvalidOperationException("Player is already added.");
            }
        }
    }

    public struct PlayerIdAndTurnCount
    {
        private int _currentTurnCount;

        public Id<Player> PlayerId { get; }
        public int InitialTurnCount { get; }
        public int CurrentTurnCount
        {
            get => _currentTurnCount;
            set
            {
                ArgumentOutOfRangeException.ThrowIfLessThan(value, MIN_TURN_COUNT);

                _currentTurnCount = value;
            }
        }

        internal PlayerIdAndTurnCount(Id<Player> id, int initialTurnCount = 1)
        {
            PlayerId = id;
            InitialTurnCount = initialTurnCount;
            _currentTurnCount = initialTurnCount;
        }

        public override bool Equals([NotNullWhen(true)] object? obj) =>
            obj is PlayerIdAndTurnCount p && PlayerId == p.PlayerId;

        public override int GetHashCode() => PlayerId.GetHashCode();
    }

    public struct AutomatContext
    {
        public LinkedListNode<PlayerIdAndTurnCount> Current { get; }
        public PlayerTurnAutomat Automat { get; }

        internal AutomatContext(PlayerTurnAutomat automat)
        {
            Automat = automat;
            Current = automat.CurrentNode;
        }
    }

    private struct PlayerEnumerator : IEnumerator<IPlayer>
    {
        public IPlayer Current => throw new NotImplementedException();

        object IEnumerator.Current => throw new NotImplementedException();

        public void Dispose() => throw new NotImplementedException();
        public bool MoveNext() => throw new NotImplementedException();
        public void Reset() => throw new NotImplementedException();
    }
}

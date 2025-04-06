using System.Collections;

using CardGame.Core.Events;

using Centuriin.CardGame.Core;
using Centuriin.CardGame.Core.Engine;

namespace CardGame.Core.Engine;

public sealed class PlayerTurnAutomat : IPlayerTurnAutomat
{
    private Dictionary<Type, Func<Task>> Actions { get; } = [];
    private LinkedList<IPlayer> Players { get; } = [];
    private LinkedListNode<IPlayer>? Current { get; set; }

    private PlayerTurnAutomat() { }

    public sealed class Builder : IPlayerTurnAutomatBuilder
    {
        private const int MIN_PLAYERS_COUNT = 2;
        
        private PlayerTurnAutomat Automat { get; set; } = new();

        public IPlayerTurnAutomat Build() => Automat;

        public Builder Reset()
        {
            Automat = new();

            return this;
        }

        public Builder AddPlayers(IReadOnlyCollection<IPlayer> players)
        {
            ArgumentNullException.ThrowIfNull(players);

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

        public Builder Register<TEvent>(Func<Task> action)
            where TEvent : IGameEvent
        {


            return this;
        }

        public Task MoveToPlayer(IPlayer player)
        {
            ArgumentNullException.ThrowIfNull(player);

            Automat.Current = Automat.Players.Find(player)
                ?? throw new InvalidOperationException("Player not found.");

            return Task.CompletedTask;
        }

        public Task MoveToFirstPlayer()
        {
            Automat.Current = Automat.Players.First;
            return Task.CompletedTask;
        }

        public Task MoveToLastPlayer()
        {
            Automat.Current = Automat.Players.Last;
            return Task.CompletedTask;
        }
    }

    public IEnumerator<IPlayer> GetEnumerator()
    {
        Current = Players.First!;

        do
        {
            yield return Current.ValueRef;

            Current = Current.Next ?? Players.First;
        }
        while(Current is not null);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

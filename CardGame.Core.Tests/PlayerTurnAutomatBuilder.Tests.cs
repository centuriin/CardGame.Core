using CardGame.Core;
using CardGame.Core.Engine;
using CardGame.Core.Events;

using Centuriin.CardGame.Core.Engine;

using FluentAssertions;

using Moq;

using Xunit;

namespace Centuriin.CardGame.Core.Tests;

public sealed class PlayerTurnAutomatBuilderTests
{
    [Fact]
    public void Test()
    {
        Func<FakeEvent, CancellationToken, Task> func = null!;
        var dispatcher = new Mock<IEventDispatcher<IGameEvent>>(MockBehavior.Strict);
        dispatcher.Setup(x =>
                x.Register(It.IsAny<Func<FakeEvent, CancellationToken, Task>>()))
            .Callback((Func<FakeEvent, CancellationToken, Task> act) => func = act);

        var player1 = new FakePlayer { Id = 1 };
        var player2 = new FakePlayer { Id = 2 };
        var player3 = new FakePlayer { Id = 3 };

        var builder = new PlayerTurnAutomat.AutomatBuilder();

        using var automat = builder
            .AddPlayers([player1, player2, player3])
            .UseDispatcher(dispatcher.Object)
            .Register<FakeEvent>((_, _) => builder.NextMoveLastPlayer())
            .Build();

        _ = builder.Reset();

        var enumerator = automat.GetEnumerator();

        enumerator.MoveNext().Should().BeTrue();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.MoveNext().Should().BeTrue();
        enumerator.MoveNext().Should().BeTrue();

        enumerator.Current.Should().Be(player1);

        func.Invoke(null!, CancellationToken.None);

        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().Be(player3);
    }

    public sealed class FakePlayer : IPlayer
    {
        public int Id { get; set; }

        public override bool Equals(object? obj) => Equals(obj as FakePlayer);
        public bool Equals(IPlayer? other) => other is FakePlayer p && Id == p.Id;

        public override int GetHashCode() => Id.GetHashCode();
    }

    public sealed class FakeEvent : IGameEvent
    {
        public GameId GameId => throw new NotImplementedException();
    }
}

using CardGame.Core;
using CardGame.Core.Engine;
using CardGame.Core.Events;

using Centuriin.CardGame.Core.Engine;

using FluentAssertions;

using Moq;

using Xunit;

namespace Centuriin.CardGame.Core.Tests.Engine;

public sealed class PlayerTurnAutomatTests
{
    [Fact]
    public void CanNotAddPlayersAfterBuild()
    {
        // Arrange
        var player1 = new PlayerId(1);
        var player2 = new PlayerId(2);

        var builder = new PlayerTurnAutomat
            .AutomatBuilder()
            .AddPlayers([player1, player2]);

        _ = builder.Build();

        // Act
        var exception = Record.Exception(() => builder.AddPlayers([player1]));

        // Assert
        exception.Should().BeOfType<InvalidOperationException>();
    }

    [Fact]
    public void CanNotBuildWithOnePlayer()
    {
        // Arrange
        var player1 = new PlayerId(1);

        var builder = new PlayerTurnAutomat
            .AutomatBuilder()
            .AddPlayers([player1]);

        // Act
        var exception = Record.Exception(() => builder.Build());

        // Assert
        exception.Should().BeOfType<InvalidOperationException>();
    }

    [Fact]
    public void CanDistinctPlayers()
    {
        // Arrange
        var player1 = new PlayerId(1);
        var player2 = new PlayerId(2);

        var automat = new PlayerTurnAutomat
            .AutomatBuilder()
            .AddPlayers([player1, player1, player2])
            .Build();

        // Act
        automat.MoveNext(); //p1
        automat.MoveNext(); //p2

        // Assert
        automat.PlayerTurn.Should().BeEquivalentTo(player2);
    }

    [Fact]
    public void CanNotUseDispatcherAfterBuild()
    {
        // Arrange
        var player1 = new PlayerId(1);
        var player2 = new PlayerId(2);
        var builder = new PlayerTurnAutomat.AutomatBuilder();

        _ = builder.AddPlayers([player1, player2]).Build();

        // Act
        var exception = Record.Exception(() => 
            builder.UseDispatcher(Mock.Of<IEventDispatcher<IGameEvent>>()));

        // Assert
        exception.Should().BeOfType<InvalidOperationException>();
    }

    [Fact]
    public void CanNotRegisterWithoutDispatcher()
    {
        // Arrange
        var player1 = new PlayerId(1);
        var player2 = new PlayerId(2);
        var builder = new PlayerTurnAutomat.AutomatBuilder();

        _ = builder.AddPlayers([player1, player2]).Build();

        // Act
        var exception = Record.Exception(() =>
            builder.Register<FakeEvent>((_, _, _) => Task.CompletedTask));

        // Assert
        exception.Should().BeOfType<InvalidOperationException>();
    }

    [Fact]
    public void CanNotRegisterAfterBuild()
    {
        // Arrange
        var player1 = new PlayerId(1);
        var player2 = new PlayerId(2);
        var builder = new PlayerTurnAutomat.AutomatBuilder();

        _ = builder.AddPlayers([player1, player2])
            .UseDispatcher(Mock.Of<IEventDispatcher<IGameEvent>>())
            .Build();

        // Act
        var exception = Record.Exception(() =>
            builder.Register<FakeEvent>((_, _, _) => Task.CompletedTask));

        // Assert
        exception.Should().BeOfType<InvalidOperationException>();
    }

    [Fact]
    public void PlayerTurnAfterBuildShouldBeNull()
    {
        // Arrange
        var player1 = new PlayerId(1);
        var player2 = new PlayerId(2);

        var builder = new PlayerTurnAutomat.AutomatBuilder();

        // Act
        var automat = builder
            .AddPlayers([player1, player2])
            .Build();

        // Assert
        automat.PlayerTurn.Should().BeNull();
    }

    [Fact]
    public void CanMoveNext()
    {
        // Arrange
        var player1 = new PlayerId(1);
        var player2 = new PlayerId(2);

        var builder = new PlayerTurnAutomat.AutomatBuilder();

        // Act
        var automat = builder
            .AddPlayers([player1, player2])
            .Build();

        automat.MoveNext();

        // Assert
        automat.PlayerTurn.Should().BeEquivalentTo(player1);
    }

    [Fact]
    public void CanBuildCyclePlayerTurns()
    {
        // Arrange
        var player1 = new PlayerId(1);
        var player2 = new PlayerId(2);

        var builder = new PlayerTurnAutomat.AutomatBuilder();

        // Act
        var automat = builder
            .AddPlayers([player1, player2])
            .Build();

        automat.MoveNext(); //p1
        automat.MoveNext(); //p2
        automat.MoveNext(); //p1

        // Assert
        automat.PlayerTurn.Should().BeEquivalentTo(player1);
    }

    [Fact]
    public void CanReactOnEvents()
    {
        // Arrange
        Func<FakeEvent, CancellationToken, Task> func = null!;
        var dispatcher = new Mock<IEventDispatcher<IGameEvent>>(MockBehavior.Strict);
        dispatcher
            .Setup(x =>
                x.Register(It.IsAny<Func<FakeEvent, CancellationToken, Task>>()))
            .Callback((Func<FakeEvent, CancellationToken, Task> act) => func = act);

        var player1 = new PlayerId(1);
        var player2 = new PlayerId(2);
        var player3 = new PlayerId(3);

        var builder = new PlayerTurnAutomat.AutomatBuilder();

        var automat = builder
            .AddPlayers([player1, player2, player3])
            .UseDispatcher(dispatcher.Object)
            .Register<FakeEvent>((_, automat, _) => automat.MoveToLastPlayer())
            .Build();

        // Assert
        func.Invoke(null!, CancellationToken.None);

        automat.PlayerTurn.Should().Be(player3);
    }

    public sealed class FakeEvent : IGameEvent
    {
        public GameId GameId => throw new NotImplementedException();
    }
}

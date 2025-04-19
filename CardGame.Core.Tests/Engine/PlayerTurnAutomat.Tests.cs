using Centuriin.CardGame.Core.Engine;
using Centuriin.CardGame.Core.Engine.Turn;

using FluentAssertions;

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

        var builder = new PlayerTurnAutomat<FakeMarker>
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

        var builder = new PlayerTurnAutomat<FakeMarker>
            .AutomatBuilder()
            .AddPlayers([player1]);

        // Act
        var exception = Record.Exception(() => builder.Build());

        // Assert
        exception.Should().BeOfType<InvalidOperationException>();
    }

    [Fact]
    public async Task CanDistinctPlayersAsync()
    {
        // Arrange
        var player1 = new PlayerId(1);
        var player2 = new PlayerId(2);

        var automat = new PlayerTurnAutomat<FakeMarker>
            .AutomatBuilder()
            .AddPlayers([player1, player1, player2])
            .Build();

        // Act
        await automat.MoveNextAsync(CancellationToken.None); //p1
        await automat.MoveNextAsync(CancellationToken.None); //p2

        // Assert
        automat.PlayerTurn.Should().BeEquivalentTo(player2);
    }

    [Fact]
    public void PlayerTurnAfterBuildShouldBeNull()
    {
        // Arrange
        var player1 = new PlayerId(1);
        var player2 = new PlayerId(2);

        var builder = new PlayerTurnAutomat<FakeMarker>.AutomatBuilder();

        // Act
        var automat = builder
            .AddPlayers([player1, player2])
            .Build();

        // Assert
        automat.PlayerTurn.Should().BeNull();
    }

    [Fact]
    public async Task CanMoveNextAsync()
    {
        // Arrange
        var player1 = new PlayerId(1);
        var player2 = new PlayerId(2);

        var builder = new PlayerTurnAutomat<FakeMarker>.AutomatBuilder();

        // Act
        var automat = builder
            .AddPlayers([player1, player2])
            .Build();

        await automat.MoveNextAsync(CancellationToken.None);

        // Assert
        automat.PlayerTurn.Should().BeEquivalentTo(player1);
    }

    [Fact]
    public async Task CanBuildCyclePlayerTurnsAsync()
    {
        // Arrange
        var player1 = new PlayerId(1);
        var player2 = new PlayerId(2);

        var builder = new PlayerTurnAutomat<FakeMarker>.AutomatBuilder();

        // Act
        var automat = builder
            .AddPlayers([player1, player2])
            .Build();

        await automat.MoveNextAsync(CancellationToken.None); //p1
        await automat.MoveNextAsync(CancellationToken.None); //p2
        await automat.MoveNextAsync(CancellationToken.None); //p1

        // Assert
        automat.PlayerTurn.Should().BeEquivalentTo(player1);
    }

    public sealed class FakeMarker : IGameMarker;
}

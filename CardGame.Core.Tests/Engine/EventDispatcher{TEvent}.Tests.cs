using Centuriin.CardGame.Core.Engine;

using FluentAssertions;

using Xunit;

namespace Centuriin.CardGame.Core.Tests.Engine;

public sealed class EventDispatherTests
{
    [Fact]
    public async Task CanRegisterAndPublishAsync()
    {
        // Arrange
        var action1Calls = 0;
        var action1 = (FakeEvent _, CancellationToken _) =>
        {
            action1Calls++;
            return Task.CompletedTask;
        };

        var action2Calls = 0;
        var action2 = (FakeDerivedEvent _, CancellationToken _) =>
        {
            action2Calls++;
            return Task.CompletedTask;
        };

        var dispatcher = new EventDispatcher<FakeEvent>();

        dispatcher.Register(action1);
        dispatcher.Register(action2);

        // Act
        await dispatcher.PublishAsync(new FakeDerivedEvent(), CancellationToken.None);

        // Assert
        action1Calls.Should().Be(1);
        action2Calls.Should().Be(1);
    }

    public class FakeEvent;
    public sealed class FakeDerivedEvent : FakeEvent;
}

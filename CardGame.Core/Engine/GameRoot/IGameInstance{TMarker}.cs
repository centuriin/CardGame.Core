using CardGame.Core.Events;

using Centuriin.CardGame.Core.Events;
using Centuriin.CardGame.Core.State;

namespace Centuriin.CardGame.Core.Engine.GameRoot;

public interface IGameInstance<TMarker>
    where TMarker : IGameMarker
{
    public IEventDispatcher<IGameEvent> Dispatcher { get; }

    public IGameState<TMarker> State { get; }

    public Task StartGameAsync(CancellationToken token);

    public Task DerailGameAsync(DerailedReason reason, CancellationToken token);

    public Task StopGameAsync(CancellationToken token);
}

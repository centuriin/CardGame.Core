using Centuriin.CardGame.Core.Events;
using Centuriin.CardGame.Core.State;

namespace Centuriin.CardGame.Core.Engine;

public interface IGameEngine
{
    public ICardGameState State { get; }

    public Task StartGameAsync(CancellationToken cancellationToken);

    public Task DerailGameAsync(DerailedReason reason, CancellationToken cancellationToken);

    public Task StopGameAsync(CancellationToken cancellationToken);
}

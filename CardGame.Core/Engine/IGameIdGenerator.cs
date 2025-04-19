using CardGame.Core;

namespace Centuriin.CardGame.Core.Engine;

public interface IGameIdGenerator
{
    public Task<GameId> NextGameIdAsync(CancellationToken token);
}

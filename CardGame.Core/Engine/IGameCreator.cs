using CardGame.Core;

namespace Centuriin.CardGame.Core.Engine;

public interface IGameCreator
{
    public Task<GameId> CreateGameAsync(CancellationToken token);
}

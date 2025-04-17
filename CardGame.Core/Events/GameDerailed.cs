using CardGame.Core;
using CardGame.Core.Events;

namespace Centuriin.CardGame.Core.Events;

public sealed class GameDerailed : IGameEvent
{
    public GameId GameId { get; }

    public DerailedReason Reason { get; }

    public GameDerailed(GameId gameId, DerailedReason reason)
    {
        GameId = gameId;

        ArgumentNullException.ThrowIfNull(reason);
        Reason = reason;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Core.Events;

public sealed class GameStartedEvent : IGameEvent
{
    public GameId GameId { get; }

    public GameStartedEvent(GameId gameId)
    {
        GameId = gameId;
    }
}

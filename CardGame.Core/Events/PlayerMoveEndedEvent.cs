using CardGame.Core;
using CardGame.Core.Events;

namespace Centuriin.CardGame.Core.Events;

public sealed record class PlayerMoveEndedEvent(GameId GameId, PlayerId PlayerId) : IGameEvent;

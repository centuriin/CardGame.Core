using CardGame.Core;
using CardGame.Core.Events;

namespace Centuriin.CardGame.Core.Events;

public sealed record class PlayerMoveEnded(GameId GameId, PlayerId PlayerId) : IGameEvent;

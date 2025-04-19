using Centuriin.CardGame.Core.Engine;
using Centuriin.CardGame.Core.Engine.Turn;

namespace Centuriin.CardGame.Core.State;
public interface IGameState<TMarker>
    where TMarker : IGameMarker
{
    ICardHolder CardHolder { get; }
    IPlayerTurnAutomat<TMarker> TurnAutomat { get; }
}
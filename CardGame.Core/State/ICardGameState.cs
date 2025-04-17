using Centuriin.CardGame.Core.Engine;

namespace Centuriin.CardGame.Core.State;
public interface ICardGameState
{
    ICardHolder CardHolder { get; }
    IPlayerTurnAutomat TurnAutomat { get; }
}
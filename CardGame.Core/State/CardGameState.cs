using Centuriin.CardGame.Core.Engine;

namespace Centuriin.CardGame.Core.State;

public sealed class CardGameState : ICardGameState
{
    public ICardHolder CardHolder { get; }

    public IPlayerTurnAutomat TurnAutomat { get; }

    public CardGameState(
        ICardHolder cardHolder,
        IPlayerTurnAutomat turnAutomat)
    {
        ArgumentNullException.ThrowIfNull(cardHolder);
        CardHolder = cardHolder;

        ArgumentNullException.ThrowIfNull(turnAutomat);
        TurnAutomat = turnAutomat;
    }
}

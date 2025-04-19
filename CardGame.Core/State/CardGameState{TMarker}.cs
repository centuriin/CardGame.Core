using Centuriin.CardGame.Core.Engine;
using Centuriin.CardGame.Core.Engine.Turn;

namespace Centuriin.CardGame.Core.State;

public sealed class CardGameState<TMarker> : IGameState<TMarker>
    where TMarker : IGameMarker
{
    public ICardHolder CardHolder { get; }

    public IPlayerTurnAutomat<TMarker> TurnAutomat { get; }

    public CardGameState(
        ICardHolder cardHolder,
        IPlayerTurnAutomat<TMarker> turnAutomat)
    {
        ArgumentNullException.ThrowIfNull(cardHolder);
        CardHolder = cardHolder;

        ArgumentNullException.ThrowIfNull(turnAutomat);
        TurnAutomat = turnAutomat;
    }
}

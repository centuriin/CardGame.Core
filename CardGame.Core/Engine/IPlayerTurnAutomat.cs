using CardGame.Core.Engine;

namespace Centuriin.CardGame.Core.Engine;

public interface IPlayerTurnAutomat : IEnumerable<IPlayer>, IDisposable
{
    public static abstract IPlayerTurnAutomatBuilder Builder { get; }
}

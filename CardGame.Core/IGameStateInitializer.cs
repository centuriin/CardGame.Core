using CardGame.Core.State;

namespace CardGame.Core;

public interface IGameStateInitializer<TGameState>
    where TGameState : CardGameStateBase
{
    public void Initialize(TGameState state);
}
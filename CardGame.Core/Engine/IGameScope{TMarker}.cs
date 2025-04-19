namespace Centuriin.CardGame.Core.Engine;

public interface IGameScope<TMarker> : IDisposable
    where TMarker : IGameMarker
{
    public IGameInstance<TMarker> GameInstance { get; }
}

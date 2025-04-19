namespace Centuriin.CardGame.Core.Engine;

public interface IGameRegistrator<TMarker> : IRegistrator
    where TMarker : IGameMarker;

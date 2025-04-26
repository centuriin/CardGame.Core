using Centuriin.CardGame.Core.Cards;

namespace Centuriin.CardGame.Core.Engine;

public interface ICardLoader<TMarker>
{
    public Task<IReadOnlyCollection<ICard>> LoadAsync(CancellationToken token);
}

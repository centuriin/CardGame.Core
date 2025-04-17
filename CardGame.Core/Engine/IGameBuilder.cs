namespace Centuriin.CardGame.Core.Engine;

public interface IGameBuilder
{
    public Task BuildAsync(CancellationToken cancellationToken);
}

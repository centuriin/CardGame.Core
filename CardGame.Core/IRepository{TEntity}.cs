namespace CardGame.Core;

public interface IRepository<TEntity>
{
    public Task AddAsync(TEntity entity, CancellationToken token);

    public Task SaveAsync(CancellationToken token);
}
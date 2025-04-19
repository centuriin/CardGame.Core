using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CardGame.Core.Events;

using Centuriin.CardGame.Core.Engine;
using Centuriin.CardGame.Core.Events;

namespace Centuriin.CardGame.Drunkard;
public sealed class GameBuilder : IGameBuilder
{
    private readonly IEventDispatcher<IGameEvent> _dispatcher;
    private readonly IGameInstance _engine;

    public GameBuilder(
        IEventDispatcher<IGameEvent> dispatcher,
        IGameInstance engine)
    {
        ArgumentNullException.ThrowIfNull(dispatcher);
        _dispatcher = dispatcher;

        ArgumentNullException.ThrowIfNull(engine);
        _engine = engine;
    }

    public Task BuildAsync(CancellationToken token)
    {
        _dispatcher.Register<PlayerMoveEnded>(OnPlayerMoveEndedAsync);

        return Task.CompletedTask;
    }

    private async Task OnPlayerMoveEndedAsync(PlayerMoveEnded moveEnded, CancellationToken token) => 
        await _engine.State.TurnAutomat.MoveNextAsync(token);
}

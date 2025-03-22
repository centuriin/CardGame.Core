using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CardGame.Core.Events;

using Centuriin.CardGame.Core.Cards;

namespace CardGame.Core;

public interface IGameEngine<TCard>
    where TCard : ICard
{
    public Task<bool> TryHandleEventAsync(IGameEvent gameEvent, CancellationToken token);
}

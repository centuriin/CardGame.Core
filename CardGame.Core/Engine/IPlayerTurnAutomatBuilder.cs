﻿using Centuriin.CardGame.Core;
using Centuriin.CardGame.Core.Engine;

namespace CardGame.Core.Engine;

public interface IPlayerTurnAutomatBuilder
{
    public IPlayerTurnAutomat Build();

    public IPlayerTurnAutomatBuilder AddPlayers(IReadOnlyCollection<PlayerId> players);

    public IPlayerTurnAutomatBuilder Reset();
}
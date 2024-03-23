using CardGame.Core.State;

using Xunit;

namespace CardGame.Core.Tests;

public class CardGameStateTests
{
    [Fact(DisplayName = "Can add card to desktop from player")]
    [Trait("Category", "Unit")]
    public void CanAddCardsToDesktopFromPlayer()
    {
        var state = new CardGameStateWithGlobalDeck(CardSets.Default36Set);

        var player = new Player(new(1), new("name"));

        state.Join(player);

        state.AddCardsToPlayer(player.Id, CardSets.Default36Set.Take(2));
        state.AddCardsToPlayer(player.Id, CardSets.Default36Set.Take(2));

        state.AddCardsToDesktopFromPlayer(player.Id, CardSets.Default36Set.Take(2));


    }
}

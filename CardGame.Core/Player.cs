using Centuriin.CardGame.Core;

namespace CardGame.Core;

public sealed class Player
{
    public Id<Player> Id { get; }

    public Name<Player> Name { get; }

    public Player(Id<Player> id, Name<Player> name)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
}

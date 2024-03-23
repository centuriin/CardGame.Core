namespace CardGame.Core;

public sealed class Card<TDescription> : ICard 
    where TDescription : ICardDescription
{
    public TDescription Descirption { get; }

    public Card(TDescription description)
    {
        Descirption = description ?? throw new ArgumentNullException(nameof(description));
    }

    public bool Equals(ICard? other) => 
        other is Card<TDescription> card &&
        card.Descirption.Equals(Descirption);

    public override bool Equals(object? obj) => 
        obj is ICard card && 
        Equals(card);

    public override int GetHashCode() => Descirption.GetHashCode();
}

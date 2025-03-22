namespace Centuriin.CardGame.Core;

public sealed record class Name<TEntity>
{
    public string Value { get; }

    public Name(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Name can't be null, empty or has only whitespaces.", nameof(value));

        Value = value;
    }
}

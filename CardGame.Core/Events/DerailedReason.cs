namespace Centuriin.CardGame.Core.Events;

public sealed class DerailedReason
{
    public string Message { get; }

    public DerailedReason(string message)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(message);
        Message = message;
    }
}

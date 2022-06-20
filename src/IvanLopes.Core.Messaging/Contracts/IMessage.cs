namespace IvanLopes.Core.Messaging.Contracts;

/// <summary>IMessage is the base contract for commands and integration events.</summary>
public interface IMessage
{
    /// <summary>The ID of the message.</summary>
    public Guid MessageId { get; }

    /// <summary>Unique identifier that allows reference to anything in the system that sent the message.</summary>
    public string? CorrelationId { get; }

    /// <summary>The system that sent the message.</summary>
    public string SourceApp { get; }

    /// <summary>The User who created the message.</summary>
    public string? CreatedBy { get; }

    /// <summary>The create date and time.</summary>
    public DateTimeOffset CreatedAt { get; }

    /// <summary>The version of the message.</summary>
    public DateTimeOffset Version { get; }

    /// <summary>Full contract type name including namespace used for</summary>
    public string ContentType { get; }

    /// <summary>The content of the message serialized as JSON.</summary>
    public string ContentJson { get; }
}

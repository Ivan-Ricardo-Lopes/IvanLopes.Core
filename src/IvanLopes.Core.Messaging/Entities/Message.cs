using IvanLopes.Core.Messaging.Contracts;
using System.Text.Json;

namespace IvanLopes.Core.Messaging.Entities
{
    /// <inheritdoc />
    public class Message : IMessage
    {
        internal Message() { }
        public Guid MessageId { get; internal set; }
        public string? CorrelationId { get; internal set; }
        public string SourceApp { get; internal set; }
        public string? CreatedBy { get; internal set; }
        public DateTimeOffset CreatedAt { get; internal set; }
        public DateTimeOffset Version { get; internal set; }
        public string ContentType { get; internal set; }
        public string ContentJson { get; internal set; }

        public T GetContentObject<T>()
        {
            if (typeof(T).FullName != ContentType)
                throw new ArgumentException(nameof(T), $"The type should be {ContentType}, but is {typeof(T).FullName}");

            return JsonSerializer.Deserialize<T>(ContentJson);
        }
    }
}

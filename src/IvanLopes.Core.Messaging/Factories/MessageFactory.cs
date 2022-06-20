using IvanLopes.Core.Messaging.Contracts;
using IvanLopes.Core.Messaging.Entities;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace IvanLopes.Core.Messaging.Factories
{
    /// <inheritdoc />
    public class MessageFactory : IMessageFactory
    {
        public IMessage CreateMessage(object content, string sourceApp, string? correlationId = null, string? createdBy = null)
        {
            if (content is null)
                throw new ArgumentNullException(nameof(content));
            if (content.GetType().IsGenericType)
                throw new ArgumentException(nameof(content), "The content can't be generic type.");

            return new Message()
            {
                MessageId = Guid.NewGuid(),
                CorrelationId = correlationId,
                SourceApp = sourceApp,
                CreatedBy = createdBy,
                CreatedAt = DateTimeOffset.UtcNow,
                Version = DateTimeOffset.UtcNow,
                ContentType = content.GetType().FullName,
                ContentJson = JsonSerializer.Serialize(content)
            };
        }
    }
}

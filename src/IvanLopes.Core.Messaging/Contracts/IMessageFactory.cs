namespace IvanLopes.Core.Messaging.Contracts
{
    /// <summary>Factory to create Message</summary>
    public interface IMessageFactory
    {
        /// <summary>Creates a new Message</summary>
        /// <param name="content">The object content of the message.</param>
        /// <param name="sourceApp"><inheritdoc cref="IMessage.SourceApp" path="/summary"/></param>
        /// <param name="correlationId"><inheritdoc cref="IMessage.CorrelationId" path="/summary"/></param>
        /// <param name="createdBy"><inheritdoc cref="IMessage.CreatedBy" path="/summary"/></param>
        /// <returns>Returns an instance of IMessage.</returns>
        public IMessage CreateMessage(object content, string sourceApp, string? correlationId = null, string? createdBy = null);
    }
}

using IvanLopes.Core.Messaging.Contracts;
using IvanLopes.Core.Messaging.Factories;
using System.Text.Json;

namespace IvanLopes.Core.Messaging.Tests
{
    public class MessageFactoryTests
    {
        IMessageFactory _messageFactory;
        const string SOURCE_APP = "MessagingTests";
        const string USER_IDENTIFIER = "MessagingTestsUser";

        public MessageFactoryTests()
        {
            this._messageFactory = new MessageFactory();
        }

        [Fact]
        public void Should_Create_Message_Instance()
        {
            /// Arrange
            var content = new ContentClassTest() { Id = Guid.NewGuid() };
            string correlationId = Guid.NewGuid().ToString();

            /// Act
            IMessage message = _messageFactory.CreateMessage(content, SOURCE_APP, correlationId, USER_IDENTIFIER);

            /// Asserts
            Assert.NotNull(message);
            Assert.Equal(correlationId, message.CorrelationId);
            Assert.True(Guid.TryParse(message.MessageId.ToString(), out Guid guidId));
            Assert.Equal(content.GetType().FullName, message.ContentType);

            var deserializedMessageContent = JsonSerializer.Deserialize<ContentClassTest>(message.ContentJson);
            Assert.NotNull(deserializedMessageContent);
            Assert.Equal(content.Id, deserializedMessageContent?.Id);

            Assert.Equal(SOURCE_APP, message.SourceApp);
            Assert.Equal(USER_IDENTIFIER, message.CreatedBy);
        }

        [Fact]
        public void Should_throw_exception_content_Is_null()
        {
            /// Act, Assert
            Assert.Throws<ArgumentNullException>(() => _messageFactory.CreateMessage(null, SOURCE_APP, null, null));
        }

        [Fact]
        public void Should_throw_exception_content_Is_generic_type()
        {
            /// Arrange
            var content = new List<string>();

            /// Act, Assert
            Assert.Throws<ArgumentException>(() => _messageFactory.CreateMessage(content, SOURCE_APP, null, null));
        }

        internal class ContentClassTest
        {
            public Guid Id { get; set; }
        }
    }
}
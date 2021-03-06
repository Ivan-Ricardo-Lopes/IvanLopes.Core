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
        public void Should_Throw_Exception_Content_Is_Null()
        {
            /// Act, Assert
            Assert.Throws<ArgumentNullException>(() => _messageFactory.CreateMessage(null, SOURCE_APP, null, null));
        }

        [Fact]
        public void Should_Throw_Exception_Content_Is_Generic_Type()
        {
            /// Arrange
            var content = new List<string>();

            /// Act, Assert
            Assert.Throws<ArgumentException>(() => _messageFactory.CreateMessage(content, SOURCE_APP, null, null));
        }

        [Fact]
        public void Should_Message_Deserialize_Content_Properly()
        {
            /// Arrange
            var content = new ContentClassTest() { Id = Guid.NewGuid() };
            string correlationId = Guid.NewGuid().ToString();
            IMessage message = _messageFactory.CreateMessage(content, SOURCE_APP, correlationId, USER_IDENTIFIER);

            /// Act
            var deserializedContent = message.GetContentObject<ContentClassTest>();

            /// Asserts
            Assert.True(deserializedContent is ContentClassTest);
            Assert.Equal(content.Id, deserializedContent.Id);
        }

        [Fact]
        public void Should_Throw_Exception_Deserialize_Type_Param_Is_Different_Then_ContentType()
        {
            /// Arrange
            var content = new ContentClassTest() { Id = Guid.NewGuid() };
            string correlationId = Guid.NewGuid().ToString();
            IMessage message = _messageFactory.CreateMessage(content, SOURCE_APP, correlationId, USER_IDENTIFIER);

            /// Act, Assert            
            Assert.Throws<ArgumentException>(() => message.GetContentObject<Type>());
        }


        internal class ContentClassTest
        {
            public Guid Id { get; set; }
        }
    }
}
using Atlas_API.Entities;
using Atlas_API.Factories;
using Atlas_API.Services;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;

namespace Atlas_API.Tests
{
    public class UserStoryCollectionRetrieverTests
    {
        private readonly IMongoClientFactory _client;

        public UserStoryCollectionRetrieverTests()
        {
            Mock<IMongoCollection<UserStory>> mockCollection = new Mock<IMongoCollection<UserStory>>();

            Mock<IMongoDatabase> mockDatabase = new Mock<IMongoDatabase>();
            mockDatabase
                .Setup(x => x.GetCollection<UserStory>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>()))
                .Returns(mockCollection.Object);


            Mock<MongoClient> mockClient = new Mock<MongoClient>();
            mockClient
                .Setup(x => x.GetDatabase(It.IsAny<string>(), It.IsAny<MongoDatabaseSettings>()))
                .Returns(mockDatabase.Object);

            Mock<IMongoClientFactory> mockFactory = new Mock<IMongoClientFactory>();
            mockFactory
                .Setup(x => x.CreateClient())
                .ReturnsAsync(new MongoClient());

            _client = mockFactory.Object;
        }

        [Test]
        public void GetCollection_UserStory_Returns_MongoCollection()
        {
            // Arrange
            ICollectionRetriever<UserStory> retriever = new UserStoryCollectionRetriever(_client);

            // Act
            var result = retriever.GetCollection("fake database", "fake collection");

            // Assert
            Assert.IsInstanceOf<IMongoCollection<UserStory>>(result);
        }
    }
}
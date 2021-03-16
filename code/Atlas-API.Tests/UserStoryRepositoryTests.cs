using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Atlas_API.Entities;
using Atlas_API.Repositories;
using Atlas_API.Services;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;

namespace Atlas_API.Tests
{
    public class UserStoryRepositoryTests
    {
        private Mock<IMongoCollection<UserStory>> _mockCollection;
        private Mock<IMongoDBContext> _mockContext;


        [Test]
        public async Task GetAll_Returns_All_UserStories()
        {
            // Arrange
            var list = new List<UserStory>()
            {
                new UserStory ()
                {
                    Id = new ObjectId(),
                    Title = "My title1",
                },
                new UserStory()
                {
                    Id = new ObjectId(),
                    Title = "My title2",
                }
            };

            Mock<IAsyncCursor<UserStory>> _userStoryCursor = new Mock<IAsyncCursor<UserStory>>();
            _userStoryCursor.Setup(_ => _.Current).Returns(list);
            _userStoryCursor
                .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);


            _mockCollection = new Mock<IMongoCollection<UserStory>>();
            _mockCollection
                .Setup(x => x.FindAsync(It.IsAny<FilterDefinition<UserStory>>(), It.IsAny<FindOptions<UserStory, UserStory>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_userStoryCursor.Object);

            _mockContext = new Mock<IMongoDBContext>();
            _mockContext
                .Setup(x => x.GetCollection<UserStory>(It.IsAny<string>()))
                .Returns(_mockCollection.Object);

            var repo = new UserStoryRepository(_mockContext.Object);

            // Act
            var result = await repo.Get();
            foreach (var story in result)
            {
                Console.WriteLine(story.Title);
            }

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
        }

    }
}
using System;
using Atlas_API.Entities;
using Atlas_API.Factories;
using Atlas_API.Services;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;

namespace Atlas_API.Tests
{
    public class MongoDBContextTests
    {
        private readonly Mock<IMongoClientFactory> _mockFactory;
        private readonly IMongoClientFactory _factory;
        public MongoDBContextTests()
        {
            _mockFactory = new Mock<IMongoClientFactory>();
            _mockFactory
                .Setup(x => x.CreateClient())
                .ReturnsAsync(new MongoClient("mongodb://test1"));
            _factory = _mockFactory.Object;
        }

        [Test]
        public void MongoDBContext_Constructs_Successfully()
        {
            // Act
            var context = new MongoDBContext(_factory);

            // Assert
            Assert.NotNull(context);
        }

        [Test]
        public void GetCollection_Throws_Exception_If_name_null()
        {
            // Arrange
            var context = new MongoDBContext(_factory);

            // Act + Assert
            Assert.Throws<Exception>(() => context.GetCollection<UserStory>(""));
        }

        [Test]
        public void GetCollection_Throws_Exception_If_Name_Whitespace()
        {
            // Arrange
            var context = new MongoDBContext(_factory);

            // Act + Assert
            Assert.Throws<Exception>(() => context.GetCollection<UserStory>("        "));
        }

        [Test]
        public void GetCollection_Returns_Collection()
        {
            // Arrange
            var context = new MongoDBContext(_factory);

            // Act
            var result = context.GetCollection<UserStory>("Atlas");

            // Assert
            Assert.IsInstanceOf<IMongoCollection<UserStory>>(result);
        }
    }
}
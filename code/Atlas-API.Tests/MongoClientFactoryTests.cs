using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Atlas_API.Factories;
using Atlas_API.Services;
using Azure.Security.KeyVault.Secrets;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;

namespace Atlas_API.Tests
{
    public class MongoClientFactoryTests
    {

        private readonly string _connectionStringSecret = "mongodb+srv://<username>:<password>@cluster.mongodb.net/<database>";
        private MongoClientFactory _factory;

        [OneTimeSetUp]
        public void SetUp()
        {
            Mock<IAzureKeyVaultAccess> mock = new Mock<IAzureKeyVaultAccess>();
            mock
                .Setup(x => x.GetSecrets(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new List<KeyVaultSecret>() { new KeyVaultSecret("secret0", _connectionStringSecret), new KeyVaultSecret("secret1", "value1"), new KeyVaultSecret("secret2", "value2"), new KeyVaultSecret("secret3", "value3") });

            var _vaultAccess = mock.Object;

            _factory = new MongoClientFactory(_vaultAccess);
        }

        [Test]
        public async Task CreateClient_Returns_MongoClient()
        {
            // Arrange


            // Act
            var result = await _factory.CreateClient();

            // Assert
            Assert.IsInstanceOf<MongoClient>(result);
        }

        [Test]
        public async Task Calling_CreateClient_Twice_Returns_Same_Object()
        {
            // Arrange

            // Act
            var firstResult = await _factory.CreateClient();
            var secondResult = await _factory.CreateClient();

            // Assert
            Assert.AreEqual(firstResult, secondResult);
        }
    }
}
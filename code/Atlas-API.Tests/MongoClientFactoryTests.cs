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

        private readonly IAzureKeyVaultAccess _vaultAccess;

        private readonly string _connectionStringSecret = "mongodb+srv://<username>:<password>@cluster.mongodb.net/<database>";
        public MongoClientFactoryTests()
        {
            Mock<IAzureKeyVaultAccess> mock = new Mock<IAzureKeyVaultAccess>();
            mock
                .Setup(x => x.GetSecrets(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new List<KeyVaultSecret>() { new KeyVaultSecret("secret0", _connectionStringSecret), new KeyVaultSecret("secret1", "value1"), new KeyVaultSecret("secret2", "value2"), new KeyVaultSecret("secret3", "value3") });

            _vaultAccess = mock.Object;
        }

        [Test]
        public async Task CreateClient_Returns_MongoClient()
        {
            // Arrange
            var factory = new MongoClientFactory(_vaultAccess);

            // Act
            var result = await factory.CreateClient();

            // Assert
            Assert.IsInstanceOf<MongoClient>(result);
        }
    }
}
using Moq;
using Azure;
using System.Threading;
using System.Threading.Tasks;
using Azure.Security.KeyVault.Secrets;
using NUnit.Framework;
using Atlas_API.Services;

namespace Atlas_API.Tests
{
    public class AzureKeyVaultAccessTests
    {
        private SecretClient client;

        private string key = "myFakeSecretKey";

        private string value = "myFakeSecretValue";

        public AzureKeyVaultAccessTests()
        {
            KeyVaultSecret secret = SecretModelFactory.KeyVaultSecret(new SecretProperties(key), value);
            Response<KeyVaultSecret> response = Response.FromValue(secret, Mock.Of<Response>());

            Mock<SecretClient> mockClient = new Mock<SecretClient>();
            mockClient
                .Setup(x => x.GetSecretAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(response));

            client = mockClient.Object;
        }

        [Test]
        public async Task GetSecrets_Returns_Correct_Secret_Value()
        {
            // Arrange 
            IAzureKeyVaultAccess vaultAccess = new AzureKeyVaultAccess(client);

            // Act
            var result = await vaultAccess.GetSecrets(key);

            // Assert
            Assert.AreEqual(result[0].Value, value);
        }

        [Test]
        public async Task GetSecrets_Returns_The_Same_Number_Of_Values_As_Keys()
        {
            // Arrange
            IAzureKeyVaultAccess vaultAccess = new AzureKeyVaultAccess(client);

            // Act
            var result = await vaultAccess.GetSecrets(key, key, key, key);

            // Assert
            Assert.That(result, Has.Count.EqualTo(4));
        }
    }
}
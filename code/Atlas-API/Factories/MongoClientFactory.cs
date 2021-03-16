using System.Threading.Tasks;
using Atlas_API.Services;
using MongoDB.Driver;
using System;

namespace Atlas_API.Factories
{
    public class MongoClientFactory : IMongoClientFactory
    {
        private readonly IAzureKeyVaultAccess _vaultAccess;
        public MongoClientFactory(IAzureKeyVaultAccess vaultAccess)
        {
            _vaultAccess = vaultAccess;
        }
        public async Task<MongoClient> CreateClient()
        {
            var secrets = await _vaultAccess.GetSecrets("MongoDBConnectionString", "MongoDBUserName", "MongoDBPassword", "MongoDBName");
            foreach (var secret in secrets)
            {
                Console.WriteLine(secret.Name);
                Console.WriteLine(secret.Value);
            }

            string connectionString = secrets[0].Value;

            connectionString = connectionString.Replace("<username>", secrets[1].Value);
            connectionString = connectionString.Replace("<password>", secrets[2].Value);
            connectionString = connectionString.Replace("<database>", secrets[3].Value);

            return new MongoClient(connectionString);
        }
    }
}
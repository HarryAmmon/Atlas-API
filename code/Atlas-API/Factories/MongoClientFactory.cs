using System.Threading.Tasks;
using Atlas_API.Services;
using MongoDB.Driver;
using System;

namespace Atlas_API.Factories
{
    public class MongoClientFactory : IMongoClientFactory
    {
        private readonly IAzureKeyVaultAccess _vaultAccess;
        private MongoClient _client;
        public MongoClientFactory(IAzureKeyVaultAccess vaultAccess)
        {
            _vaultAccess = vaultAccess;
        }
        public async Task<MongoClient> CreateClient()
        {
            if (_client == null)
            {

                var secrets = await _vaultAccess.GetSecrets("MongoDBConnectionString", "MongoDBUserName", "MongoDBPassword", "MongoDBName");


                string connectionString = secrets[0].Value;

                connectionString = connectionString.Replace("<username>", secrets[1].Value);
                connectionString = connectionString.Replace("<password>", secrets[2].Value);
                connectionString = connectionString.Replace("<database>", secrets[3].Value);

                _client = new MongoClient(connectionString);
                return _client;
            }
            else
            {
                return _client;
            }
        }
    }
}
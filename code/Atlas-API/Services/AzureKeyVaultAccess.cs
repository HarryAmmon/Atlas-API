using System.Collections.Generic;
using System.Threading.Tasks;
using Azure;
using Azure.Security.KeyVault.Secrets;

namespace Atlas_API.Services
{
    public class AzureKeyVaultAccess : IAzureKeyVaultAccess
    {
        private readonly SecretClient _client;

        public AzureKeyVaultAccess(SecretClient client)
        {
            _client = client;
        }

        public async Task<IList<KeyVaultSecret>> GetSecrets(params string[] secretNames)
        {
            List<Task<Response<KeyVaultSecret>>> tasks = new List<Task<Response<KeyVaultSecret>>>();

            foreach (string secret in secretNames)
            {
                tasks.Add(_client.GetSecretAsync(secret));
            }

            var completedTasks = await Task.WhenAll(tasks);

            List<KeyVaultSecret> secrets = new List<KeyVaultSecret>();
            foreach (var task in completedTasks)
            {
                secrets.Add(task);
            }

            return secrets;
        }
    }
}
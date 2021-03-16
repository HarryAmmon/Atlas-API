using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Security.KeyVault.Secrets;

namespace Atlas_API.Services
{
    public interface IAzureKeyVaultAccess
    {
        Task<IList<KeyVaultSecret>> GetSecrets(params string[] secretNames);
    }
}
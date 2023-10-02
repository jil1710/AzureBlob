using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.Extensions.Configuration.AzureKeyVault;

namespace AzureBlob
{
    public class PrefixKeyVaultManager : KeyVaultSecretManager
    {
        private readonly string _keyPrefix;

        public PrefixKeyVaultManager(string keyPrefix)
        {
            _keyPrefix = $"{keyPrefix}-";
        }

        public override bool Load(Azure.Security.KeyVault.Secrets.SecretProperties secret)
        {
            var result = secret.Name.StartsWith(_keyPrefix);
            var j = 8;
            return result;
        }

        public override string GetKey(KeyVaultSecret secret)
        {
            var result = secret.Name.Substring(_keyPrefix.Length).Replace("--", ConfigurationPath.KeyDelimiter);
            var j = 8;
            return result;
        }

        
        
    }
}

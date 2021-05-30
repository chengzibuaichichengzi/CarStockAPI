using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarStockAPI.Auth
{
    public class DealerManager : IDealerManager
    {
        // Hardcode two dealers's credentials for testing
        private Dictionary<string, string> credentials = new Dictionary<string, string>()
        {
            { "dealerClientId", "dealerSecretKey" },
            { "anotherDealerClientId", "anotherKey" }
        };

        private readonly ICustomTokenManager customTokenManager;

        public DealerManager(ICustomTokenManager customTokenManager)
        {
            this.customTokenManager = customTokenManager;
        }

        public string Authenticate(string clientId, string secretKey)
        {
            //validate the credentials              
            if (!string.IsNullOrWhiteSpace(clientId) && credentials[clientId] != secretKey) return string.Empty;

            //generate token using dealer's client Id
            return customTokenManager.CreateToken(clientId);
        }
    }
}

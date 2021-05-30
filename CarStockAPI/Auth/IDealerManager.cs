using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarStockAPI.Auth
{
    public interface IDealerManager
    {
        string Authenticate(string clientId, string secretKey);
    }
}

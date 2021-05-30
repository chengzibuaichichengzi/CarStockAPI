using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarStockAPI.Auth
{
    public interface ICustomTokenManager
    {
        string CreateToken(string clientId);
        string GetUserInfoByToken(string token);
        bool VerifyToken(string token);
    }
}

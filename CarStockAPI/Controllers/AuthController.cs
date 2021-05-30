using CarStockAPI.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarStockAPI.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IDealerManager _dealerManager;
        private readonly ICustomTokenManager _customTokenManager;

        public AuthController(IDealerManager dealerManager, ICustomTokenManager customTokenManager)
        {
            _customTokenManager = customTokenManager;
            _dealerManager = dealerManager;
        }

        [HttpPost]
        [Route("/authenticate")]
        public Task<string> Authenticate(UserCredential userCredential)
        {

            return Task.FromResult(_dealerManager.Authenticate(userCredential.clientId, userCredential.clientSecret));
        }
    }

    public class UserCredential
    {
        public string clientId { get; set; }
        public string clientSecret { get; set; }
    }

    public class Token
    {
        public string token { get; set; }
    }
}

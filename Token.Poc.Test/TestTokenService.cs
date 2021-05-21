using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Token.Poc.Api.Services;

namespace Token.Poc.Test
{
    public class TestTokenService : ITokenService
    {
        public async Task<decimal> GetBalance(string erc20Address, string tokenHolderAddress)
        {
            return (decimal)1.0;
        }
    }
}

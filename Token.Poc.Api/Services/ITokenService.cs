using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Token.Poc.Api.Services
{
    public interface ITokenService
    {
        Task<decimal> GetBalance(string erc20Address, string tokenHolderAddress);
    }
}

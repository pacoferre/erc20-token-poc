using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Token.Poc.Api.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Token.Poc.Api.Controllers
{
    public class GetBalanceDto
    {
        public string Balance { get; set; }
    }

    [ApiController]
    public partial class TokenInfoController : ControllerBase
    {
        private ITokenService _service;

        public TokenInfoController(ITokenService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("/api/{erc20Address}/{tokenHolderAddress}")]
        public async Task<GetBalanceDto> GetBalance(string erc20Address, string tokenHolderAddress)
        {
            var balance = (await _service.GetBalance(erc20Address, tokenHolderAddress)).ToString().Replace(",", ".");
            var parts = balance.Split('.');

            if (parts.Length > 1)
            {
                balance = Int64.Parse(parts[0]).ToString("#,##0") + '.' + parts[1];
            }

            return new GetBalanceDto
            {
                Balance = balance
            };
        }
    }
}

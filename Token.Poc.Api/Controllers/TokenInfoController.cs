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
        public async Task<GetBalanceDto> Get(string erc20Address, string tokenHolderAddress)
        {
            return new GetBalanceDto
            {
                Balance = (await _service.GetBalance(erc20Address, tokenHolderAddress)).ToString().Replace(",", ".")
            };
        }
    }
}

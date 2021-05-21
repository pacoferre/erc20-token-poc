using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Token.Poc.Api.Services
{
    public class TokenService : ITokenService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _cache;

        public TokenService(IConfiguration configuration, HttpClient httpClient, IMemoryCache cache)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _cache = cache;
        }

        public async Task<decimal> GetBalance(string erc20Address, string tokenHolderAddress)
        {
            var cacheEntry = await
                _cache.GetOrCreateAsync(erc20Address + "_" + tokenHolderAddress, async entry =>
                {
                    entry.SlidingExpiration = TimeSpan.FromSeconds(10);

                    try
                    {
                        string url = $"https://api.etherscan.io/api?module=account&action=tokenbalance&" +
                            $"contractaddress={erc20Address}&address={tokenHolderAddress}&tag=latest&apikey={_configuration["ETHERSCAN_API"]}";

                        var responseString = await _httpClient.GetStringAsync(url);
                        var response = JsonSerializer
                            .Deserialize<EtherScanResponse>(responseString, new JsonSerializerOptions()
                            {
                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                            });
                        var value = decimal.Parse(response.Result.Substring(0, response.Result.Length - 6)) / 1000000000000;

                        return value;
                    }
                    catch
                    {
                    }

                    return 0;
                });

            return cacheEntry;
        }
    }


    public class EtherScanResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string Result { get; set; }
    }

}


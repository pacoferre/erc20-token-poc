using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using Token.Poc.Api.Services;

namespace Token.Poc.Test
{
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.Lifetime == ServiceLifetime.Scoped && d.ServiceType.FullName ==
                        typeof(ITokenService).FullName);

                services.Remove(descriptor);

                services.AddScoped<ITokenService, TestTokenService>();
            });
        }
    }
}

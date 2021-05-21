using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Token.Poc.Api.Controllers;
using Xunit;
using FluentAssertions;

namespace Token.Poc.Test
{
    [CollectionDefinition("Tests")]
    public class UnitTest : IClassFixture<CustomWebApplicationFactory<Api.Startup>>
    {
        private readonly CustomWebApplicationFactory<Api.Startup> _factory;

        public UnitTest(CustomWebApplicationFactory<Api.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Simple_Test()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/aaa/bbb");
            var stringResponse = await response.Content
                .ReadAsStringAsync();
            var responseDto = JsonSerializer
                .Deserialize<GetBalanceDto>(stringResponse, new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            responseDto.Balance.Should().Be("1");
        }
    }
}

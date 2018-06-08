using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
using FluentAssertions;
using System.Net.Http.Headers;
using System.Text;
using System;
using WebApplication1;
using POC.NetCore.Model;

namespace XUnitTestProject1
{
    public class PessoasControllerIntegrationTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public PessoasControllerIntegrationTests()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                                     .UseStartup<WebApplication1.Startup>());
            _client = _server.CreateClient();
        }
        // ... 

        [Fact]
        public async Task Pessoas_Put()
        {
            // Arrange
            var pessoa = new Pessoa
            {
                Id = 16,
                PrimeiroNome = "John",
                LastName = "Doe",
                Idade = 50,
                Titulo = "FooBar",
                Telefone = "001 123 1234567",
                Email = "john.doe@foo.bar"
            };
            var content = JsonConvert.SerializeObject(pessoa);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync("/api/Pessoas/16", stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            responseString.Should().Be(String.Empty);
        }

        [Fact]
        public async Task Pessoa_Invalida()
        {
            // Arrange
            var pessoa = new Pessoa { PrimeiroNome = "John" };
            var content = JsonConvert.SerializeObject(pessoa);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync("/api/Pessoas/16", stringContent);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            var responseString = await response.Content.ReadAsStringAsync();
            responseString.Should().Contain("The Email field is required");
     
        }

        [Fact]
        public async Task Pessoa_Delete_Specific()
        {
            // Arrange

            // Act
            var response = await _client.DeleteAsync("/api/Pessoas/16");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            responseString.Should().Be(String.Empty);
        }
    }
}

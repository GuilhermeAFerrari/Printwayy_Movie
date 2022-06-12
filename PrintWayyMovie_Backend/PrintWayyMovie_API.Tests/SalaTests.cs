using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;

namespace PrintWayyMovie_API.Tests
{
    public class SalaTests
    {
        [Fact]
        public async Task RetornarTodasSalas()
        {
            // Preparo
            var application = new WebHostBuilder().UseStartup<Program>();
            var server = new TestServer(application);
            var client = server.CreateClient();

            // Acao
            var result = await client.GetFromJsonAsync<Sala>(requestUri: "/salas");

            // Resultado
            Assert.NotNull(result);
        }

        [Fact]
        public async Task RetornarUmaSala()
        {
            // Preparo
            var application = new WebHostBuilder().UseStartup<Program>();
            var server = new TestServer(application);
            var client = server.CreateClient();

            // Acao
            var result = await client.GetFromJsonAsync<Sala>(requestUri: "/salas/1");

            // Resultado
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CriarSala()
        {
            // Preparo
            var application = new WebHostBuilder().UseStartup<Program>();
            var server = new TestServer(application);
            var client = server.CreateClient();

            var dadosSala = new Sala()
            {
                Cl_Quantidade = 100,
                Cl_Nome = "Sala-Teste"
            };

            // Acao
            var result = await client.PostAsJsonAsync(requestUri: "/create-sala", dadosSala);

            // Resultado
            Assert.Equal(StatusCodes.Status201Created, (double)result.StatusCode);
        }
    }
}
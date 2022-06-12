using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;

namespace PrintWayyMovie_API.Tests
{
    public class SessaoTests
    {
        [Fact]
        public async Task RetornarTodasSessoes()
        {
            // Preparo
            var application = new WebHostBuilder().UseStartup<Program>();
            var server = new TestServer(application);
            var client = server.CreateClient();

            // Acao
            var result = await client.GetFromJsonAsync<Sessao>(requestUri: "/sessoes");

            // Resultado
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CriarSessao()
        {
            // Preparo
            var application = new WebHostBuilder().UseStartup<Program>();
            var server = new TestServer(application);
            var client = server.CreateClient();

            var dadosSessao = new Sessao()
            {
                Cl_Data = "2022-06-12",
                Cl_HoraInicio = "10:00",
                Cl_Valor = 19.99,
                Cl_Animacao = "2D",
                Cl_Audio = "Original",
                Fk_IdFilme = 1,
                Fk_IdSala = 1
            };

            // Acao
            var result = await client.PutAsJsonAsync(requestUri: "/create-sessao", dadosSessao);

            // Resultado
            Assert.Equal(StatusCodes.Status201Created, (double)result.StatusCode);
        }

        [Fact]
        public async Task RemoverSessao()
        {
            // Preparo
            var application = new WebHostBuilder().UseStartup<Program>();
            var server = new TestServer(application);
            var client = server.CreateClient();

            // Acao
            var result = await client.DeleteAsync("/delete-sessao/1");

            // Resultado
            Assert.Equal(StatusCodes.Status404NotFound, (double)result.StatusCode);
        }
    }
}

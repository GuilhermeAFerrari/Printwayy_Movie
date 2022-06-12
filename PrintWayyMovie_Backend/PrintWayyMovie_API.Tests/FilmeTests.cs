using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;

namespace PrintWayyMovie_API.Tests
{
    public class FilmeTests
    {
        [Fact]
        public async Task RetornarTodosFilmes()
        {
            // Preparo
            var application = new WebHostBuilder().UseStartup<Program>();
            var server = new TestServer(application);
            var client = server.CreateClient();

            // Acao
            var result = await client.GetFromJsonAsync<Filme>(requestUri: "/filmes");

            // Resultado
            Assert.NotNull(result);
        }

        [Fact]
        public async Task RetornarUmFilme()
        {
            // Preparo
            var application = new WebHostBuilder().UseStartup<Program>();
            var server = new TestServer(application);
            var client = server.CreateClient();

            // Acao
            var result = await client.GetFromJsonAsync<Filme>(requestUri: "/filmes/1");

            // Resultado
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CriarFilme()
        {
            // Preparo
            var application = new WebHostBuilder().UseStartup<Program>();
            var server = new TestServer(application);
            var client = server.CreateClient();

            var dadosFilme = new Filme()
            {
                Cl_Imagem = "Imagem Teste",
                Cl_Titulo = "Teste",
                Cl_Descricao = "Descrição teste",
                Cl_Duracao = "120"
            };

            // Acao
            var result = await client.PostAsJsonAsync(requestUri: "/create-filme", dadosFilme);

            // Resultado
            Assert.Equal(StatusCodes.Status201Created, (double)result.StatusCode);
        }

        [Fact]
        public async Task AtualizarFilme()
        {
            // Preparo
            var application = new WebHostBuilder().UseStartup<Program>();
            var server = new TestServer(application);
            var client = server.CreateClient();

            var dadosFilme = new Filme()
            {
                Cl_Imagem = "Imagem Teste",
                Cl_Titulo = "Teste",
                Cl_Descricao = "Descrição teste",
                Cl_Duracao = "120"
            };

            // Acao
            var result = await client.PutAsJsonAsync(requestUri: "/update-filme/1", dadosFilme);

            // Resultado
            Assert.Equal(StatusCodes.Status204NoContent, (double)result.StatusCode);
        }

        [Fact]
        public async Task RemoverFilme()
        {
            // Preparo
            var application = new WebHostBuilder().UseStartup<Program>();
            var server = new TestServer(application);
            var client = server.CreateClient();

            // Acao
            var result = await client.DeleteAsync("/delete-filme/1");

            // Resultado
            Assert.Equal(StatusCodes.Status404NotFound, (double)result.StatusCode);
        }
    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;

namespace PrintWayyMovie_API.Tests
{
    public class UsuarioTests
    {
        [Fact]
        public async Task RetornarTodosUsuarios()
        {
            // Preparo
            var application = new WebHostBuilder().UseStartup<Program>();
            var server = new TestServer(application);
            var client = server.CreateClient();

            // Acao
            var result = await client.GetFromJsonAsync<Usuario>(requestUri: "/usuarios");

            // Resultado
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CriarUsuario()
        {
            // Preparo
            var application = new WebHostBuilder().UseStartup<Program>();
            var server = new TestServer(application);
            var client = server.CreateClient();

            var dadosUsuario = new Usuario()
            {
                Cl_Email = "teste@printwayy.com",
                Cl_Senha = "1234"
            };

            // Acao
            var result = await client.PostAsJsonAsync(requestUri: "/create-usuario", dadosUsuario);

            // Resultado
            Assert.Equal(StatusCodes.Status201Created, (double)result.StatusCode);
        }
    }
}
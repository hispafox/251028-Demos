using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Api.Services;
using TodoApp.Api;


using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Program = TodoApp.Api.Program;


namespace TodoApp.IntegrationTests
{
    public class IntegrationTestBase : IDisposable
    {

        protected readonly WebApplicationFactory<Program> Factory;
        protected readonly HttpClient Client;

        /*
        public IntegrationTestBase()
        {
            Factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseEnvironment("Testing");
                });
            Client = Factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
                HandleCookies = true,
                BaseAddress = new Uri("https://localhost:5001")
            });
        }*/

        public IntegrationTestBase()
        {

//            var projectDir = Path.GetFullPath(
//    Path.Combine(AppContext.BaseDirectory,
//                 "..", "..", "..", "..", // ajusta según tu estructura
//                 "TodoApp")             // carpeta de tu proyecto de la aplicación
//);

            // Configuramos un host web de prueba con servicios limpios para cada prueba
            Factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        // Usamos un servicio limpio para cada prueba
                        services.AddSingleton<ITodoService, TodoService>();
                    });
                    //builder.UseContentRoot(projectDir);
                });

            Client = Factory.CreateClient();
        }

        public void Dispose()
        {
            Client.Dispose();
            Factory.Dispose();
        }

    }
}

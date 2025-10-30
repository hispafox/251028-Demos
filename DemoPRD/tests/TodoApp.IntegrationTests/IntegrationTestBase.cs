using Microsoft.AspNetCore.Mvc.Testing;
using TodoApp.Api.Services;

namespace TodoApp.IntegrationTests;

/// <summary>
/// Clase base para pruebas de integración.
/// Proporciona WebApplicationFactory configurado.
/// </summary>
public class IntegrationTestBase : IDisposable
{
    protected readonly WebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;

    public IntegrationTestBase()
    {
   Factory = new WebApplicationFactory<Program>()
   .WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
  {
          // Asegurar servicios limpios para cada prueba
        // El servicio Singleton se recrea con cada Factory
       });
            });

        Client = Factory.CreateClient();
    }

    public void Dispose()
    {
        Client.Dispose();
        Factory.Dispose();
        GC.SuppressFinalize(this);
    }
}

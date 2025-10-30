using Microsoft.AspNetCore.Mvc.Testing;

namespace TodoApp.E2ETests;

/// <summary>
/// Clase base para pruebas end-to-end.
/// </summary>
public class E2ETestBase : IDisposable
{
    protected readonly WebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;

    public E2ETestBase()
    {
      Factory = new WebApplicationFactory<Program>();
        Client = Factory.CreateClient();
    }

    public void Dispose()
    {
        Client.Dispose();
        Factory.Dispose();
        GC.SuppressFinalize(this);
  }
}

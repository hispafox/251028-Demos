using GestionUsuarios.Api.Models;
using GestionUsuarios.Api.Repositories;
using GestionUsuarios.Api.Services;
using Moq;

namespace GestionUsuarios.Tests;

public class UsuarioServiceTests
{

    private readonly List<Usuario> _usuarios;
    private readonly Mock<IUsuarioRepository> _mockRepository;
    // SUT -> System Under Test
    private readonly IUsuarioService _service;

    public UsuarioServiceTests()
    {




    // Configuración común para todas las pruebas
    _usuarios = new List<Usuario>
            {
                new Usuario { Id = 1, Nombre = "Juan Pérez", Email = "juan@ejemplo.com", Activo = true },
                new Usuario { Id = 2, Nombre = "María López", Email = "maria@ejemplo.com", Activo = true }
            };

        _mockRepository = new Mock<IUsuarioRepository>();
        _service = new UsuarioService(_mockRepository.Object);
    }


    //// Método de prueba para ObtenerTodosAsync
    //[Fact]
    //public async Task ObtenerTodosAsync_DeberiaDevolverListaDeUsuarios()
    //{
    //    // Arrange
    //    var repositorio = new UsuarioRepository();

    //    var service = new UsuarioService(repositorio);
    //    // Act
    //    var resultado = await service.ObtenerTodosAsync();
    //    // Assert
    //    Assert.NotNull(resultado);
    //    Assert.Equal(2, resultado.Count());
    //}


    // Método de prueba para ObtenerTodosAsync
    [Fact]
    public async Task ObtenerTodosAsync_DeberiaDevolverListaDeUsuarios()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.ObtenerTodosAsync()).ReturnsAsync(_usuarios);

        // Act
        var resultado = await _service.ObtenerTodosAsync();

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(2, resultado.Count());
        Assert.Equal(_usuarios.Count, resultado.Count());
        _mockRepository.Verify(repo => repo.ObtenerTodosAsync(), Times.Once);

        // Verificaciones adicionales de _mockRepository si es necesario, comprobar que no llama al metodo ObtenerPorIdAsync
        _mockRepository.Verify(repo => repo.ObtenerPorIdAsync(It.IsAny<int>()), Times.Never);



    }


    [Fact]
    public async Task ObtenerPorIdAsync_ConIdValido_DebeRetornarUsuario()
    {
        // Arrange
        var usuario = _usuarios.First();
        _mockRepository.Setup(repo => repo.ObtenerPorIdAsync(usuario.Id))
            .ReturnsAsync(usuario);
        _mockRepository.Setup(repo => repo.ObtenerPorIdAsync(2))
            .ReturnsAsync(_usuarios[1]);
        //_mockRepository.Setup(repo => repo.ObtenerPorIdAsync(2))
        //    .ReturnsAsync(usuario);

        // Act
        var resultado = await _service.ObtenerPorIdAsync(usuario.Id);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(usuario.Id, resultado.Id);
        Assert.Equal(usuario.Nombre, resultado.Nombre);
        _mockRepository.Verify(repo => repo.ObtenerPorIdAsync(usuario.Id), Times.Once);
    }


}
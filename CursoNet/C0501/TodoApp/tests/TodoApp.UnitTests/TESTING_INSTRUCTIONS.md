# Instrucciones para Pruebas Unitarias - TodoApp

## ?? Descripción del Proyecto

Este proyecto contiene pruebas unitarias para la aplicación TodoApp, utilizando **xUnit** y **Moq** para el aislamiento de dependencias.

---

## ??? Convenciones de Nomenclatura

### Nombres de Métodos de Prueba

```
[Método]_[Condición]_[ResultadoEsperado]
```

**Ejemplos:**
- `Add_ConItemValido_DevuelveItemConId`
- `Add_ConTituloVacio_LanzaArgumentException`
- `GetById_ConIdExistente_DevuelveItem`
- `Delete_ConIdExistente_EliminaYDevuelveTrue`

### Nombres de Clases de Prueba

```
[ClaseObjetivo]Tests
```

**Ejemplos:**
- `TodoServiceTests` - Pruebas para `TodoService`
- `TodosControllerTests` - Pruebas para `TodosController`

---

## ?? Estructura AAA (Arrange-Act-Assert)

Todas las pruebas deben seguir el patrón AAA con comentarios explícitos:

```csharp
[Fact]
public void NombreDelTest()
{
    // Arrange - Preparar los datos y dependencias de prueba
    var mockService = new Mock<ITodoService>();
    var item = new TodoItem { Title = "Test" };

    // Act - Ejecutar la acción a probar
    var resultado = _service.Add(item);

    // Assert - Verificar el resultado
  Assert.NotNull(resultado);
 Assert.Equal(item.Title, resultado.Title);
}
```

---

## ?? Sistema Bajo Prueba (SUT)

```csharp
/// <summary>
/// _todoService es el SUT (System Under Test)
/// El componente que se está probando
/// </summary>
private readonly TodoService _todoService;

public TodoServiceTests()
{
    // Arrange - Preparar el SUT para todas las pruebas
    _todoService = new TodoService();
}
```

---

## ?? Tipos de Pruebas Unitarias

### 1. Pruebas de Servicios

**Objetivo:** Validar la lógica de negocio en los servicios.

**Características:**
- Usar instancias reales del servicio cuando no tiene dependencias
- Usar **Moq** para simular dependencias externas
- Validar comportamiento y excepciones

**Ejemplo:**

```csharp
public class TodoServiceTests
{
    private readonly TodoService _todoService;

    public TodoServiceTests()
 {
        _todoService = new TodoService();
    }

    [Fact]
    public void Add_ConItemValido_DevuelveItemConId()
    {
        // Arrange
        var item = new TodoItem { Title = "Test Todo" };

  // Act
    var result = _todoService.Add(item);

        // Assert
   Assert.NotEqual(0, result.Id);
        Assert.Equal(item.Title, result.Title);
    }

    [Fact]
    public void Add_ConTituloVacio_LanzaArgumentException()
    {
        // Arrange
    var item = new TodoItem { Title = "" };

      // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => _todoService.Add(item));
        Assert.Contains("título no puede estar vacío", exception.Message);
    }
}
```

### 2. Pruebas de Controladores

**Objetivo:** Validar la lógica del controlador aislando dependencias.

**Características:**
- Usar **Moq** para simular servicios
- Validar tipos de retorno (`ActionResult`)
- Validar códigos de estado HTTP
- No realizar llamadas HTTP reales

**Ejemplo:**

```csharp
public class TodosControllerTests
{
    private readonly Mock<ITodoService> _mockService;
    private readonly TodosController _controller;

    public TodosControllerTests()
    {
 // Arrange - Crear mocks de dependencias
        _mockService = new Mock<ITodoService>();
_controller = new TodosController(_mockService.Object);
    }

    [Fact]
    public void GetAll_CuandoHayItems_DevuelveOkConLista()
    {
        // Arrange
        var items = new List<TodoItem>
        {
          new TodoItem { Id = 1, Title = "Test 1" },
       new TodoItem { Id = 2, Title = "Test 2" }
        };
        _mockService.Setup(s => s.GetAll()).Returns(items);

        // Act
      var result = _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedItems = Assert.IsAssignableFrom<IEnumerable<TodoItem>>(okResult.Value);
        Assert.Equal(2, returnedItems.Count());
    }

    [Fact]
  public void GetById_ConIdInexistente_DevuelveNotFound()
    {
        // Arrange
        _mockService.Setup(s => s.GetById(It.IsAny<int>())).Returns((TodoItem)null);

        // Act
        var result = _controller.GetById(999);

        // Assert
   Assert.IsType<NotFoundResult>(result);
    }
}
```

---

## ?? Plan de Generación de Tests Completos

### Paso 1: Identificar Componentes a Probar

- **Servicios**: `TodoService`, `ITodoService`
- **Controladores**: `TodosController`, `WeatherForecastController`
- **Modelos**: Validaciones en `TodoItem` (si existen)

### Paso 2: Diseñar Casos de Prueba

#### 2.1 Casos Básicos (Happy Path)
- Operaciones exitosas con datos válidos
- Retorno de colecciones con datos
- Creación, lectura, actualización y eliminación exitosas

#### 2.2 Casos de Validación
- Títulos vacíos o nulos
- IDs inexistentes
- Datos inválidos

#### 2.3 Casos de Límites
- Colecciones vacías
- Primer y último elemento
- IDs mínimos y máximos

#### 2.4 Casos Excepcionales
- Excepciones de validación
- Operaciones sobre elementos inexistentes
- Conflictos de estado

### Paso 3: Implementar Tests con `[Fact]` y `[Theory]`

#### Estructura con `[Fact]` (Un caso específico)

```csharp
[Fact]
public void GetAll_CuandoNoHayItems_DevuelveColeccionVacia()
{
    // Arrange
    _mockService.Setup(s => s.GetAll()).Returns(new List<TodoItem>());

    // Act
    var result = _controller.GetAll();

    // Assert
    var okResult = Assert.IsType<OkObjectResult>(result);
    var items = Assert.IsAssignableFrom<IEnumerable<TodoItem>>(okResult.Value);
    Assert.Empty(items);
}
```

#### Estructura con `[Theory]` (Múltiples casos)

```csharp
[Theory]
[InlineData("")]
[InlineData(null)]
[InlineData("   ")]
public void Add_ConTituloInvalido_LanzaArgumentException(string titulo)
{
    // Arrange
    var item = new TodoItem { Title = titulo };

    // Act & Assert
    Assert.Throws<ArgumentException>(() => _todoService.Add(item));
}
```

### Paso 4: Uso de Moq para Dependencias

#### Setup de Comportamiento

```csharp
// Retornar valor específico
_mockService.Setup(s => s.GetById(1)).Returns(new TodoItem { Id = 1, Title = "Test" });

// Retornar null
_mockService.Setup(s => s.GetById(999)).Returns((TodoItem)null);

// Lanzar excepción
_mockService.Setup(s => s.Add(It.IsAny<TodoItem>())).Throws<ArgumentException>();

// Verificar que un método fue llamado
_mockService.Verify(s => s.Add(It.IsAny<TodoItem>()), Times.Once);

// Verificar con parámetros específicos
_mockService.Verify(s => s.GetById(1), Times.Once);
```

### Paso 5: Ejecutar y Validar

1. Abrir **Test Explorer** en Visual Studio: `Ctrl+E, T`
2. Ejecutar todos los tests: `Ctrl+R, A`
3. Verificar que todos pasen ?
4. Revisar cobertura de código (opcional):
   - Usar **Coverlet** (ya incluido en el proyecto)
   - Objetivo: >80% de cobertura

---

## ? Checklist de Cobertura

### TodoService
- [x] `GetAll` - Estado inicial vacío
- [x] `Add` - Item válido con ID asignado
- [x] `Add` - Título vacío lanza excepción
- [x] `GetById` - ID existente devuelve item
- [x] `GetById` - ID inexistente devuelve null
- [x] `Delete` - ID existente elimina y devuelve true
- [x] `Delete` - ID inexistente devuelve false
- [ ] `Update` - Pendiente si existe el método

### TodosController
- [ ] `GetAll` - Devuelve OK con colección vacía
- [ ] `GetAll` - Devuelve OK con lista de items
- [ ] `GetById` - ID existente devuelve OK con item
- [ ] `GetById` - ID inexistente devuelve NotFound
- [ ] `Create` - Datos válidos devuelve Created
- [ ] `Create` - Datos inválidos devuelve BadRequest
- [ ] `Update` - ID existente actualiza y devuelve NoContent
- [ ] `Update` - ID inexistente devuelve NotFound
- [ ] `Delete` - ID existente elimina y devuelve NoContent
- [ ] `Delete` - ID inexistente devuelve NotFound

---

## ?? Plantilla para Nuevos Tests

### Test de Servicio

```csharp
[Fact]
public void Metodo_Condicion_ResultadoEsperado()
{
    // Arrange - Preparar datos de prueba
    var item = new TodoItem { Title = "Test" };

    // Act - Ejecutar método del servicio
    var resultado = _todoService.Metodo(item);

    // Assert - Verificar resultado
    Assert.NotNull(resultado);
    // Más aserciones...
}
```

### Test de Controlador con Mock

```csharp
[Fact]
public void Metodo_Condicion_ResultadoEsperado()
{
    // Arrange - Configurar mock
    _mockService.Setup(s => s.Metodo(It.IsAny<int>())).Returns(expectedValue);

    // Act - Ejecutar método del controlador
    var result = _controller.Metodo(1);

    // Assert - Verificar tipo de resultado y valor
    var okResult = Assert.IsType<OkObjectResult>(result);
    Assert.Equal(expectedValue, okResult.Value);
}
```

---

## ??? Herramientas y Comandos

### Ejecutar Tests
- **Todos los tests**: `Ctrl+R, A`
- **Tests del archivo actual**: `Ctrl+R, T`
- **Test bajo el cursor**: `Ctrl+R, Ctrl+T`
- **Ejecutar tests fallidos**: `Ctrl+R, F`

### Ver Resultados
- Abrir **Test Explorer**: `Ctrl+E, T`
- Ver detalles de fallos en **Output** ? **Tests**

### Depurar Tests
- Poner breakpoint en el test
- Click derecho en el test ? **Debug Test**
- O usar: `Ctrl+R, Ctrl+D`

---

## ?? Recursos Adicionales

- [xUnit Documentation](https://xunit.net/)
- [Moq Quickstart](https://github.com/moq/moq4/wiki/Quickstart)
- [Unit Testing Best Practices - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices)
- [Testing in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/test/overview)

---

## ?? Estándares del Proyecto

- **Framework de pruebas**: xUnit 2.5.3
- **Framework de mocking**: Moq 4.20.72
- **Target Framework**: .NET 8.0
- **Lenguaje**: C# 12.0
- **Convención de nombres**: PascalCase con guiones bajos para separar secciones
- **Cobertura objetivo**: >80%
- **Patrón obligatorio**: AAA (Arrange-Act-Assert) con comentarios

---

## ?? Notas Importantes

1. **Aislamiento**: Las pruebas unitarias NO deben:
   - Acceder a bases de datos
   - Realizar llamadas HTTP reales
   - Escribir en el sistema de archivos
   - Depender de otras pruebas

2. **Uso de Mocks**: Usar `Moq` para simular todas las dependencias externas

3. **Independencia**: Cada test debe poder ejecutarse de forma independiente

4. **Limpieza**: No es necesario limpiar el estado entre tests si se usa el patrón AAA correctamente

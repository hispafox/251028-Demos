# ?? Guía General de Testing - TodoApp

## ?? Descripción del Workspace

Este workspace contiene una aplicación TodoApp con tres tipos de pruebas organizadas en proyectos separados:

```
TodoApp/
??? src/
?   ??? TodoApp.Api/    # Aplicación principal (API)
??? tests/
  ??? TodoApp.UnitTests/         # Pruebas unitarias
??? TodoApp.IntegrationTests/  # Pruebas de integración
    ??? TodoApp.E2ETests/          # Pruebas End-to-End
```

---

## ?? Tipos de Pruebas y Cuándo Usarlas

### 1. ?? Pruebas Unitarias (`TodoApp.UnitTests`)

**¿Qué son?**
Pruebas que validan componentes individuales (clases, métodos) en completo aislamiento.

**¿Cuándo usarlas?**
- Probar lógica de negocio en servicios
- Validar comportamiento de métodos individuales
- Verificar manejo de excepciones
- Probar cálculos y transformaciones de datos

**Características:**
- ? Muy rápidas (< 1ms por test)
- ?? Completamente aisladas (usan Moq para dependencias)
- ?? Alta cobertura (objetivo >80%)
- ? Fáciles de mantener

**Framework:** xUnit + Moq

?? **Ver:** [`tests/TodoApp.UnitTests/TESTING_INSTRUCTIONS.md`](tests/TodoApp.UnitTests/TESTING_INSTRUCTIONS.md)

---

### 2. ?? Pruebas de Integración (`TodoApp.IntegrationTests`)

**¿Qué son?**
Pruebas que validan la integración entre múltiples componentes trabajando juntos.

**¿Cuándo usarlas?**
- Probar endpoints HTTP completos
- Validar serialización/deserialización JSON
- Verificar códigos de estado HTTP
- Probar middleware y filtros
- Validar integración con base de datos (si aplica)

**Características:**
- ?? Moderadamente rápidas (10-100ms por test)
- ?? Usan servidor de pruebas real (WebApplicationFactory)
- ?? Componentes reales (controladores, servicios, etc.)
- ?? Cobertura de integración (objetivo >70%)

**Framework:** xUnit + Microsoft.AspNetCore.Mvc.Testing

?? **Ver:** [`tests/TodoApp.IntegrationTests/TESTING_INSTRUCTIONS.md`](tests/TodoApp.IntegrationTests/TESTING_INSTRUCTIONS.md)

---

### 3. ?? Pruebas End-to-End (`TodoApp.E2ETests`)

**¿Qué son?**
Pruebas que validan flujos completos de usuario desde el inicio hasta el fin.

**¿Cuándo usarlas?**
- Probar escenarios de usuario completos
- Validar flujos críticos de negocio
- Verificar que todo el sistema funciona junto
- Probar UI con navegador real (con Selenium)

**Características:**
- ?? Más lentas (100ms-varios segundos por test)
- ?? Simulan usuarios reales
- ?? Pruebas de flujos completos (CRUD completo)
- ?? Cobertura de escenarios críticos (5-10 escenarios principales)

**Framework:** xUnit + Microsoft.AspNetCore.Mvc.Testing + Selenium (opcional)

?? **Ver:** [`tests/TodoApp.E2ETests/TESTING_INSTRUCTIONS.md`](tests/TodoApp.E2ETests/TESTING_INSTRUCTIONS.md)

---

## ??? Pirámide de Testing

```
        /\
       /  \  E2E Tests
      / ?? \  (Pocos, escenarios críticos)
   /______\
/   \
   / Integration \  Integration Tests
  /    ??       \  (Moderados, endpoints y flujos)
 /______________\
/        \
/   Unit Tests    \  Unit Tests
/      ??        \  (Muchos, alta cobertura)
/__________________\
```

**Proporciones Recomendadas:**
- **70%** Pruebas Unitarias (rápidas, muchas)
- **20%** Pruebas de Integración (moderadas)
- **10%** Pruebas E2E (lentas, pocas pero críticas)

---

## ?? Comparación Rápida

| Aspecto | Unit Tests | Integration Tests | E2E Tests |
|---------|------------|-------------------|-----------|
| **Velocidad** | ??? Muy rápidas | ?? Moderadas | ? Lentas |
| **Aislamiento** | ?? Total (con mocks) | ?? Parcial (componentes reales) | ?? Ninguno (sistema completo) |
| **Alcance** | ?? Métodos individuales | ?? Endpoints/Controladores | ?? Flujos completos |
| **Mantenimiento** | ? Fácil | ?? Moderado | ?? Complejo |
| **Cantidad** | ?? Muchas (100+) | ?? Moderadas (20-50) | ?? Pocas (5-15) |
| **Cobertura** | >80% del código | >70% de endpoints | Escenarios críticos |
| **Fallos** | ?? Específicos | ?? Módulo específico | ?? En flujo completo |

---

## ?? Comandos Esenciales

### Ejecutar Todas las Pruebas

```bash
# Desde el directorio raíz de TodoApp
dotnet test

# Ejecutar solo un proyecto de pruebas
dotnet test tests/TodoApp.UnitTests
dotnet test tests/TodoApp.IntegrationTests
dotnet test tests/TodoApp.E2ETests

# Ejecutar con detalles
dotnet test --logger "console;verbosity=detailed"

# Generar reporte de cobertura (con Coverlet)
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```

### Visual Studio

- **Todos los tests**: `Ctrl+R, A`
- **Tests del archivo actual**: `Ctrl+R, T`
- **Test bajo el cursor**: `Ctrl+R, Ctrl+T`
- **Tests fallidos**: `Ctrl+R, F`
- **Abrir Test Explorer**: `Ctrl+E, T`
- **Depurar test**: `Ctrl+R, Ctrl+D`

---

## ? Workflow de Testing Recomendado

### 1. Durante el Desarrollo (TDD)

```
1. Escribir test unitario que falla ??
2. Implementar código mínimo para pasar el test ??
3. Refactorizar y mejorar ??
4. Repetir
```

### 2. Antes de Commit

```bash
# Ejecutar tests unitarios (rápidas)
dotnet test tests/TodoApp.UnitTests

# Si pasan, ejecutar integración
dotnet test tests/TodoApp.IntegrationTests

# Build final
dotnet build
```

### 3. Antes de Merge/Deploy

```bash
# Ejecutar TODAS las pruebas
dotnet test

# Verificar cobertura
dotnet test /p:CollectCoverage=true

# Build de release
dotnet build -c Release
```

### 4. En CI/CD Pipeline

```yaml
# Ejemplo para GitHub Actions
- name: Run Unit Tests
  run: dotnet test tests/TodoApp.UnitTests --no-build

- name: Run Integration Tests
  run: dotnet test tests/TodoApp.IntegrationTests --no-build

- name: Run E2E Tests
  run: dotnet test tests/TodoApp.E2ETests --no-build
```

---

## ?? Convenciones de Nomenclatura

### Archivos y Clases

```csharp
// Unit Tests
TodoServiceTests.cs           // Pruebas para TodoService
TodosControllerTests.cs       // Pruebas para TodosController

// Integration Tests
TodosControllerTests.cs       // Pruebas de integración del controlador
IntegrationTestBase.cs        // Clase base con WebApplicationFactory

// E2E Tests
TodoE2ETests.cs              // Pruebas de escenarios completos
```

### Métodos de Prueba

```csharp
// Unit & Integration Tests
[Metodo]_[Condicion]_[ResultadoEsperado]

// Ejemplos
Add_ConItemValido_DevuelveItemConId
GetById_ConIdInexistente_DevuelveNotFound
Create_ConDatosValidos_DevuelveCreatedYNuevoItem

// E2E Tests
EscenarioCompleto_[DescripcionDelFlujo]

// Ejemplos
EscenarioCompleto_GestionDeTareas
EscenarioCompleto_UsuarioCreaTareasYLasCompleta
```

---

## ?? Patrón AAA (Arrange-Act-Assert)

**Obligatorio en todos los tipos de pruebas:**

```csharp
[Fact]
public void/async Task NombreDelTest()
{
    // Arrange - Preparar datos y dependencias
    var input = "test data";
    var expected = "expected result";

    // Act - Ejecutar la acción a probar
    var result = SystemUnderTest.Method(input);

    // Assert - Verificar el resultado
    Assert.Equal(expected, result);
}
```

---

## ?? Configuración de Proyectos

### TodoApp.UnitTests

```xml
<PackageReference Include="xunit" Version="2.5.3" />
<PackageReference Include="Moq" Version="4.20.72" />
<PackageReference Include="coverlet.collector" Version="6.0.0" />
```

**Dependencias:**
- Proyecto: `TodoApp.Api`
- Usa: Moq para simular dependencias

### TodoApp.IntegrationTests

```xml
<PackageReference Include="xunit" Version="2.5.3" />
<PackageReference Include="Microsoft.AspNetCore.Mvc.Testing" Version="8.0.16" />
<PackageReference Include="coverlet.collector" Version="6.0.0" />
```

**Dependencias:**
- Proyecto: `TodoApp.Api`
- Usa: WebApplicationFactory para servidor de pruebas

### TodoApp.E2ETests

```xml
<PackageReference Include="xunit" Version="2.5.3" />
<PackageReference Include="Microsoft.AspNetCore.Mvc.Testing" Version="8.0.16" />
<PackageReference Include="Selenium.WebDriver" Version="4.32.0" />
<PackageReference Include="Selenium.Support" Version="4.32.0" />
```

**Dependencias:**
- Proyecto: `TodoApp.Api`
- Usa: WebApplicationFactory + Selenium (opcional)

---

## ?? Decisión: ¿Qué tipo de test escribir?

### Flowchart de Decisión

```
¿Qué estoy probando?
?
?? Un método o clase individual
?  ?? ?? UNIT TEST
?
?? Un endpoint HTTP o integración entre componentes
?  ?? ?? INTEGRATION TEST
?
?? Un flujo completo de usuario
   ?? ?? E2E TEST
```

### Ejemplos Prácticos

#### Escenario 1: Validación de Título

```csharp
// ? UNIT TEST - Probar lógica de validación
[Fact]
public void Add_ConTituloVacio_LanzaArgumentException()
{
    var item = new TodoItem { Title = "" };
    Assert.Throws<ArgumentException>(() => _todoService.Add(item));
}
```

#### Escenario 2: Endpoint de Creación

```csharp
// ? INTEGRATION TEST - Probar endpoint HTTP
[Fact]
public async Task Create_ConDatosValidos_DevuelveCreatedYNuevoItem()
{
    var newItem = new TodoItem { Title = "Test" };
    var response = await Client.PostAsJsonAsync("/api/todos", newItem);
    Assert.Equal(HttpStatusCode.Created, response.StatusCode);
}
```

#### Escenario 3: Flujo de Usuario Completo

```csharp
// ? E2E TEST - Probar flujo completo
[Fact]
public async Task EscenarioCompleto_UsuarioCreaActualizaYEliminaTarea()
{
    // PASO 1: Crear tarea
    // PASO 2: Actualizar tarea
    // PASO 3: Verificar actualización
    // PASO 4: Eliminar tarea
    // PASO 5: Verificar eliminación
}
```

---

## ?? Errores Comunes y Soluciones

### Error 1: Tests Dependientes

**? Mal:**
```csharp
// Test 1 crea datos que Test 2 usa
[Fact] public void Test1() { /* crea ID=1 */ }
[Fact] public void Test2() { /* usa ID=1 */ }  // ?? Falla si Test1 no ejecuta
```

**? Bien:**
```csharp
// Cada test es independiente
[Fact] public void Test1() { /* crea y limpia */ }
[Fact] public void Test2() { /* crea y limpia */ }
```

### Error 2: No Limpiar Datos

**? Mal:**
```csharp
[Fact]
public async Task Test()
{
    var item = await CreateItem();
    // No se elimina - contamina otros tests
}
```

**? Bien:**
```csharp
[Fact]
public async Task Test()
{
    var item = await CreateItem();
    try { /* pruebas */ }
    finally { await DeleteItem(item.Id); }  // ? Cleanup garantizado
}
```

### Error 3: Tests Lentos (Integration/E2E)

**? Mal:**
```csharp
[Fact] public async Task Test1() { var factory = new WebApplicationFactory<Program>(); }
[Fact] public async Task Test2() { var factory = new WebApplicationFactory<Program>(); }
// Crea servidor en cada test ??
```

**? Bien:**
```csharp
public class Tests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    public Tests(WebApplicationFactory<Program> factory) 
    { 
        _client = factory.CreateClient(); // ? Reutiliza servidor
 }
}
```

---

## ?? Métricas de Calidad

### Objetivos por Tipo de Test

| Métrica | Unit Tests | Integration Tests | E2E Tests |
|---------|-----------|-------------------|-----------|
| **Cobertura de Código** | >80% | >70% | N/A (cobertura de flujos) |
| **Tiempo de Ejecución** | <500ms total | <5s total | <30s total |
| **Cantidad** | 100+ tests | 20-50 tests | 5-15 scenarios |
| **Tasa de Éxito** | >99% | >95% | >90% |

### Herramientas de Cobertura

```bash
# Coverlet (incluido en el proyecto)
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

# Fine Code Coverage (extensión de Visual Studio)
# Instalar desde: Extensions ? Manage Extensions ? Fine Code Coverage
```

---

## ?? Checklist de Testing Completo

### Antes de Considerar una Feature "Completa"

- [ ] **Unit Tests**
  - [ ] Todos los métodos públicos tienen al menos 1 test
  - [ ] Casos happy path cubiertos
  - [ ] Casos de error y excepciones cubiertos
  - [ ] Cobertura >80%

- [ ] **Integration Tests**
  - [ ] Todos los endpoints HTTP probados
  - [ ] Códigos de estado HTTP validados (200, 201, 400, 404, etc.)
  - [ ] Serialización/deserialización JSON funciona
  - [ ] Validaciones de entrada probadas

- [ ] **E2E Tests**
  - [ ] Al menos 1 escenario de usuario completo
  - [ ] Flujo happy path funcional
  - [ ] Cleanup de datos implementado

- [ ] **General**
  - [ ] Todos los tests pasan: `dotnet test`
  - [ ] Build exitoso: `dotnet build`
  - [ ] Sin warnings de compilación
  - [ ] Documentación actualizada

---

## ?? Recursos Adicionales

### Documentación Oficial

- [Unit Testing Best Practices - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices)
- [Integration Tests in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests)
- [xUnit Documentation](https://xunit.net/)
- [Moq Quickstart](https://github.com/moq/moq4/wiki/Quickstart)

### Proyectos de Ejemplo

- TodoApp.UnitTests - Ejemplos de pruebas unitarias con Moq
- TodoApp.IntegrationTests - Ejemplos de pruebas con WebApplicationFactory
- TodoApp.E2ETests - Ejemplos de escenarios completos

---

## ?? Mejores Prácticas Generales

### 1. Tests Independientes

? Cada test debe poder ejecutarse solo sin depender de otros

### 2. Nombres Descriptivos

? El nombre del test debe describir claramente qué se está probando

### 3. Un Assert Principal por Test

? Enfocarse en una verificación principal (pero pueden haber asserts secundarios)

### 4. Usar Theory para Casos Múltiples

? `[Theory]` con `[InlineData]` para probar múltiples escenarios similares

### 5. Comentarios AAA

? Siempre incluir comentarios `// Arrange`, `// Act`, `// Assert`

### 6. Cleanup de Datos

? Limpiar datos de prueba al final (especialmente en Integration y E2E)

### 7. Tests Rápidos

? Mantener tests rápidos - si son lentos, mover a Integration o E2E

### 8. Mensajes de Error Claros

? Usar mensajes descriptivos en assertions: `Assert.Equal(expected, actual, "El título debería coincidir")`

---

## ?? Soporte

Para más información sobre cada tipo de test, consulta las guías específicas:

- ?? [Unit Tests Guide](tests/TodoApp.UnitTests/TESTING_INSTRUCTIONS.md)
- ?? [Integration Tests Guide](tests/TodoApp.IntegrationTests/TESTING_INSTRUCTIONS.md)
- ?? [E2E Tests Guide](tests/TodoApp.E2ETests/TESTING_INSTRUCTIONS.md)
- ??? [API Development Guide](src/TodoApp.Api/DEVELOPMENT_GUIDE.md)

---

**Versión:** 1.0  
**Última actualización:** 2024  
**Target Framework:** .NET 8.0  
**Testing Framework:** xUnit 2.5.3

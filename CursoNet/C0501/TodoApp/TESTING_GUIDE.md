# ?? Gu�a General de Testing - TodoApp

## ?? Descripci�n del Workspace

Este workspace contiene una aplicaci�n TodoApp con tres tipos de pruebas organizadas en proyectos separados:

```
TodoApp/
??? src/
?   ??? TodoApp.Api/    # Aplicaci�n principal (API)
??? tests/
  ??? TodoApp.UnitTests/         # Pruebas unitarias
??? TodoApp.IntegrationTests/  # Pruebas de integraci�n
    ??? TodoApp.E2ETests/          # Pruebas End-to-End
```

---

## ?? Tipos de Pruebas y Cu�ndo Usarlas

### 1. ?? Pruebas Unitarias (`TodoApp.UnitTests`)

**�Qu� son?**
Pruebas que validan componentes individuales (clases, m�todos) en completo aislamiento.

**�Cu�ndo usarlas?**
- Probar l�gica de negocio en servicios
- Validar comportamiento de m�todos individuales
- Verificar manejo de excepciones
- Probar c�lculos y transformaciones de datos

**Caracter�sticas:**
- ? Muy r�pidas (< 1ms por test)
- ?? Completamente aisladas (usan Moq para dependencias)
- ?? Alta cobertura (objetivo >80%)
- ? F�ciles de mantener

**Framework:** xUnit + Moq

?? **Ver:** [`tests/TodoApp.UnitTests/TESTING_INSTRUCTIONS.md`](tests/TodoApp.UnitTests/TESTING_INSTRUCTIONS.md)

---

### 2. ?? Pruebas de Integraci�n (`TodoApp.IntegrationTests`)

**�Qu� son?**
Pruebas que validan la integraci�n entre m�ltiples componentes trabajando juntos.

**�Cu�ndo usarlas?**
- Probar endpoints HTTP completos
- Validar serializaci�n/deserializaci�n JSON
- Verificar c�digos de estado HTTP
- Probar middleware y filtros
- Validar integraci�n con base de datos (si aplica)

**Caracter�sticas:**
- ?? Moderadamente r�pidas (10-100ms por test)
- ?? Usan servidor de pruebas real (WebApplicationFactory)
- ?? Componentes reales (controladores, servicios, etc.)
- ?? Cobertura de integraci�n (objetivo >70%)

**Framework:** xUnit + Microsoft.AspNetCore.Mvc.Testing

?? **Ver:** [`tests/TodoApp.IntegrationTests/TESTING_INSTRUCTIONS.md`](tests/TodoApp.IntegrationTests/TESTING_INSTRUCTIONS.md)

---

### 3. ?? Pruebas End-to-End (`TodoApp.E2ETests`)

**�Qu� son?**
Pruebas que validan flujos completos de usuario desde el inicio hasta el fin.

**�Cu�ndo usarlas?**
- Probar escenarios de usuario completos
- Validar flujos cr�ticos de negocio
- Verificar que todo el sistema funciona junto
- Probar UI con navegador real (con Selenium)

**Caracter�sticas:**
- ?? M�s lentas (100ms-varios segundos por test)
- ?? Simulan usuarios reales
- ?? Pruebas de flujos completos (CRUD completo)
- ?? Cobertura de escenarios cr�ticos (5-10 escenarios principales)

**Framework:** xUnit + Microsoft.AspNetCore.Mvc.Testing + Selenium (opcional)

?? **Ver:** [`tests/TodoApp.E2ETests/TESTING_INSTRUCTIONS.md`](tests/TodoApp.E2ETests/TESTING_INSTRUCTIONS.md)

---

## ??? Pir�mide de Testing

```
        /\
       /  \  E2E Tests
      / ?? \  (Pocos, escenarios cr�ticos)
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
- **70%** Pruebas Unitarias (r�pidas, muchas)
- **20%** Pruebas de Integraci�n (moderadas)
- **10%** Pruebas E2E (lentas, pocas pero cr�ticas)

---

## ?? Comparaci�n R�pida

| Aspecto | Unit Tests | Integration Tests | E2E Tests |
|---------|------------|-------------------|-----------|
| **Velocidad** | ??? Muy r�pidas | ?? Moderadas | ? Lentas |
| **Aislamiento** | ?? Total (con mocks) | ?? Parcial (componentes reales) | ?? Ninguno (sistema completo) |
| **Alcance** | ?? M�todos individuales | ?? Endpoints/Controladores | ?? Flujos completos |
| **Mantenimiento** | ? F�cil | ?? Moderado | ?? Complejo |
| **Cantidad** | ?? Muchas (100+) | ?? Moderadas (20-50) | ?? Pocas (5-15) |
| **Cobertura** | >80% del c�digo | >70% de endpoints | Escenarios cr�ticos |
| **Fallos** | ?? Espec�ficos | ?? M�dulo espec�fico | ?? En flujo completo |

---

## ?? Comandos Esenciales

### Ejecutar Todas las Pruebas

```bash
# Desde el directorio ra�z de TodoApp
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
2. Implementar c�digo m�nimo para pasar el test ??
3. Refactorizar y mejorar ??
4. Repetir
```

### 2. Antes de Commit

```bash
# Ejecutar tests unitarios (r�pidas)
dotnet test tests/TodoApp.UnitTests

# Si pasan, ejecutar integraci�n
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
TodosControllerTests.cs       // Pruebas de integraci�n del controlador
IntegrationTestBase.cs        // Clase base con WebApplicationFactory

// E2E Tests
TodoE2ETests.cs              // Pruebas de escenarios completos
```

### M�todos de Prueba

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

## ?? Patr�n AAA (Arrange-Act-Assert)

**Obligatorio en todos los tipos de pruebas:**

```csharp
[Fact]
public void/async Task NombreDelTest()
{
    // Arrange - Preparar datos y dependencias
    var input = "test data";
    var expected = "expected result";

    // Act - Ejecutar la acci�n a probar
    var result = SystemUnderTest.Method(input);

    // Assert - Verificar el resultado
    Assert.Equal(expected, result);
}
```

---

## ?? Configuraci�n de Proyectos

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

## ?? Decisi�n: �Qu� tipo de test escribir?

### Flowchart de Decisi�n

```
�Qu� estoy probando?
?
?? Un m�todo o clase individual
?  ?? ?? UNIT TEST
?
?? Un endpoint HTTP o integraci�n entre componentes
?  ?? ?? INTEGRATION TEST
?
?? Un flujo completo de usuario
   ?? ?? E2E TEST
```

### Ejemplos Pr�cticos

#### Escenario 1: Validaci�n de T�tulo

```csharp
// ? UNIT TEST - Probar l�gica de validaci�n
[Fact]
public void Add_ConTituloVacio_LanzaArgumentException()
{
    var item = new TodoItem { Title = "" };
    Assert.Throws<ArgumentException>(() => _todoService.Add(item));
}
```

#### Escenario 2: Endpoint de Creaci�n

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
    // PASO 3: Verificar actualizaci�n
    // PASO 4: Eliminar tarea
    // PASO 5: Verificar eliminaci�n
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

## ?? M�tricas de Calidad

### Objetivos por Tipo de Test

| M�trica | Unit Tests | Integration Tests | E2E Tests |
|---------|-----------|-------------------|-----------|
| **Cobertura de C�digo** | >80% | >70% | N/A (cobertura de flujos) |
| **Tiempo de Ejecuci�n** | <500ms total | <5s total | <30s total |
| **Cantidad** | 100+ tests | 20-50 tests | 5-15 scenarios |
| **Tasa de �xito** | >99% | >95% | >90% |

### Herramientas de Cobertura

```bash
# Coverlet (incluido en el proyecto)
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

# Fine Code Coverage (extensi�n de Visual Studio)
# Instalar desde: Extensions ? Manage Extensions ? Fine Code Coverage
```

---

## ?? Checklist de Testing Completo

### Antes de Considerar una Feature "Completa"

- [ ] **Unit Tests**
  - [ ] Todos los m�todos p�blicos tienen al menos 1 test
  - [ ] Casos happy path cubiertos
  - [ ] Casos de error y excepciones cubiertos
  - [ ] Cobertura >80%

- [ ] **Integration Tests**
  - [ ] Todos los endpoints HTTP probados
  - [ ] C�digos de estado HTTP validados (200, 201, 400, 404, etc.)
  - [ ] Serializaci�n/deserializaci�n JSON funciona
  - [ ] Validaciones de entrada probadas

- [ ] **E2E Tests**
  - [ ] Al menos 1 escenario de usuario completo
  - [ ] Flujo happy path funcional
  - [ ] Cleanup de datos implementado

- [ ] **General**
  - [ ] Todos los tests pasan: `dotnet test`
  - [ ] Build exitoso: `dotnet build`
  - [ ] Sin warnings de compilaci�n
  - [ ] Documentaci�n actualizada

---

## ?? Recursos Adicionales

### Documentaci�n Oficial

- [Unit Testing Best Practices - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices)
- [Integration Tests in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests)
- [xUnit Documentation](https://xunit.net/)
- [Moq Quickstart](https://github.com/moq/moq4/wiki/Quickstart)

### Proyectos de Ejemplo

- TodoApp.UnitTests - Ejemplos de pruebas unitarias con Moq
- TodoApp.IntegrationTests - Ejemplos de pruebas con WebApplicationFactory
- TodoApp.E2ETests - Ejemplos de escenarios completos

---

## ?? Mejores Pr�cticas Generales

### 1. Tests Independientes

? Cada test debe poder ejecutarse solo sin depender de otros

### 2. Nombres Descriptivos

? El nombre del test debe describir claramente qu� se est� probando

### 3. Un Assert Principal por Test

? Enfocarse en una verificaci�n principal (pero pueden haber asserts secundarios)

### 4. Usar Theory para Casos M�ltiples

? `[Theory]` con `[InlineData]` para probar m�ltiples escenarios similares

### 5. Comentarios AAA

? Siempre incluir comentarios `// Arrange`, `// Act`, `// Assert`

### 6. Cleanup de Datos

? Limpiar datos de prueba al final (especialmente en Integration y E2E)

### 7. Tests R�pidos

? Mantener tests r�pidos - si son lentos, mover a Integration o E2E

### 8. Mensajes de Error Claros

? Usar mensajes descriptivos en assertions: `Assert.Equal(expected, actual, "El t�tulo deber�a coincidir")`

---

## ?? Soporte

Para m�s informaci�n sobre cada tipo de test, consulta las gu�as espec�ficas:

- ?? [Unit Tests Guide](tests/TodoApp.UnitTests/TESTING_INSTRUCTIONS.md)
- ?? [Integration Tests Guide](tests/TodoApp.IntegrationTests/TESTING_INSTRUCTIONS.md)
- ?? [E2E Tests Guide](tests/TodoApp.E2ETests/TESTING_INSTRUCTIONS.md)
- ??? [API Development Guide](src/TodoApp.Api/DEVELOPMENT_GUIDE.md)

---

**Versi�n:** 1.0  
**�ltima actualizaci�n:** 2024  
**Target Framework:** .NET 8.0  
**Testing Framework:** xUnit 2.5.3

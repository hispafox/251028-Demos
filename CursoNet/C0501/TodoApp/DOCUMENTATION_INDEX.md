# ?? �ndice de Documentaci�n - TodoApp

## ?? Gu�a R�pida de Navegaci�n

Esta es una gu�a de referencia r�pida para localizar toda la documentaci�n del proyecto TodoApp.

---

## ?? Documentaci�n Principal

### 1. [README.md](README.md) - Visi�n General del Proyecto
**Lee esto primero** ?

Contiene:
- Descripci�n general del proyecto
- Inicio r�pido y prerrequisitos
- Estructura del proyecto
- API endpoints y ejemplos
- Tecnolog�as utilizadas
- Roadmap y contribuci�n

**Cu�ndo leer:** Al empezar con el proyecto

---

### 2. [TESTING_GUIDE.md](TESTING_GUIDE.md) - Gu�a General de Testing
**La gu�a maestra de testing** ??

Contiene:
- Los 3 tipos de pruebas (Unit, Integration, E2E)
- Cu�ndo usar cada tipo
- Pir�mide de testing
- Comandos esenciales
- Workflow de testing
- Comparaci�n entre tipos de test

**Cu�ndo leer:** Antes de escribir cualquier test

---

## ?? Documentaci�n de Desarrollo

### 3. [src/TodoApp.Api/DEVELOPMENT_GUIDE.md](src/TodoApp.Api/DEVELOPMENT_GUIDE.md) - Gu�a de Desarrollo de la API
**Para desarrollar en el proyecto API** ???

Contiene:
- Arquitectura del proyecto
- Convenciones de c�digo y nomenclatura
- Patrones de dise�o implementados
- C�mo agregar nuevos endpoints
- Mejores pr�cticas
- Configuraci�n y middleware
- Manejo de errores
- Comandos �tiles

**Cu�ndo leer:** 
- Al agregar nuevos endpoints
- Al modificar servicios o controladores
- Al configurar el proyecto

---

## ?? Documentaci�n de Testing

### 4. [tests/TodoApp.UnitTests/TESTING_INSTRUCTIONS.md](tests/TodoApp.UnitTests/TESTING_INSTRUCTIONS.md) - Pruebas Unitarias
**Para probar componentes aislados** ??

Contiene:
- Uso de **Moq** para mocks
- Pruebas de servicios y controladores
- Estructura AAA (Arrange-Act-Assert)
- Plantillas de tests unitarios
- Checklist de cobertura
- Objetivo: >80% cobertura

**Cu�ndo leer:**
- Al escribir pruebas para servicios
- Al testear l�gica de negocio
- Al usar Moq para simular dependencias

---

### 5. [tests/TodoApp.IntegrationTests/TESTING_INSTRUCTIONS.md](tests/TodoApp.IntegrationTests/TESTING_INSTRUCTIONS.md) - Pruebas de Integraci�n
**Para probar endpoints HTTP** ??

Contiene:
- Uso de **WebApplicationFactory**
- Pruebas de endpoints completos
- Validaci�n de c�digos HTTP
- Flujos CRUD completos
- Configuraci�n de testhost.deps.json
- Objetivo: >70% endpoints

**Cu�ndo leer:**
- Al probar endpoints de la API
- Al validar integraci�n entre componentes
- Al testear serializaci�n JSON

---

### 6. [tests/TodoApp.E2ETests/TESTING_INSTRUCTIONS.md](tests/TodoApp.E2ETests/TESTING_INSTRUCTIONS.md) - Pruebas End-to-End
**Para probar flujos de usuario completos** ??

Contiene:
- Escenarios completos de usuario
- Estructura de pasos numerados
- Uso de **Selenium** (opcional)
- Pruebas de flujos cr�ticos
- Manejo de cleanup
- Pruebas de carga

**Cu�ndo leer:**
- Al escribir escenarios de usuario
- Al probar flujos cr�ticos completos
- Al usar Selenium para UI testing

---

## ??? Mapa de Decisi�n: �Qu� Documento Leer?

```
???????????????????????????????????????
? �Qu� quieres hacer?       ?
???????????????????????????????????????
         ?
         ??? Empezar con el proyecto
         ?   ??? ?? README.md
       ?
         ??? Entender el testing en general
    ?   ??? ?? TESTING_GUIDE.md
         ?
      ??? Desarrollar en la API
  ?   ??? Agregar nuevo endpoint
         ?   ?   ??? ??? DEVELOPMENT_GUIDE.md
         ?   ??? Modificar servicio
         ?   ?   ??? ??? DEVELOPMENT_GUIDE.md
         ?   ??? Configurar middleware
         ?       ??? ??? DEVELOPMENT_GUIDE.md
?
         ??? Escribir tests
       ??? Probar m�todo de servicio
        ?   ??? ?? Unit Tests Guide
           ??? Probar endpoint HTTP
        ?   ??? ?? Integration Tests Guide
  ??? Probar flujo completo de usuario
       ??? ?? E2E Tests Guide
```

---

## ?? Por Rol

### Desarrollador Backend

**Orden de lectura:**
1. [README.md](README.md) - Visi�n general
2. [DEVELOPMENT_GUIDE.md](src/TodoApp.Api/DEVELOPMENT_GUIDE.md) - Desarrollo de API
3. [Unit Tests Guide](tests/TodoApp.UnitTests/TESTING_INSTRUCTIONS.md) - Tests unitarios
4. [Integration Tests Guide](tests/TodoApp.IntegrationTests/TESTING_INSTRUCTIONS.md) - Tests de integraci�n

### QA / Tester

**Orden de lectura:**
1. [README.md](README.md) - Visi�n general
2. [TESTING_GUIDE.md](TESTING_GUIDE.md) - Gu�a general de testing
3. [Integration Tests Guide](tests/TodoApp.IntegrationTests/TESTING_INSTRUCTIONS.md) - Tests de integraci�n
4. [E2E Tests Guide](tests/TodoApp.E2ETests/TESTING_INSTRUCTIONS.md) - Tests E2E

### Tech Lead / Arquitecto

**Orden de lectura:**
1. [README.md](README.md) - Visi�n general
2. [DEVELOPMENT_GUIDE.md](src/TodoApp.Api/DEVELOPMENT_GUIDE.md) - Arquitectura
3. [TESTING_GUIDE.md](TESTING_GUIDE.md) - Estrategia de testing
4. Todos los guides espec�ficos para revisi�n

### Nuevo en el Equipo

**Orden de lectura:**
1. [README.md](README.md) - ? Empezar aqu�
2. [TESTING_GUIDE.md](TESTING_GUIDE.md) - Entender testing
3. [DEVELOPMENT_GUIDE.md](src/TodoApp.Api/DEVELOPMENT_GUIDE.md) - Aprender el desarrollo
4. Guides espec�ficos seg�n necesidad

---

## ?? Por Tarea

### "Necesito agregar un nuevo endpoint"

1. [DEVELOPMENT_GUIDE.md](src/TodoApp.Api/DEVELOPMENT_GUIDE.md) ? Secci�n "Agregar un Nuevo Endpoint"
2. [Unit Tests Guide](tests/TodoApp.UnitTests/TESTING_INSTRUCTIONS.md) ? Escribir tests para el servicio
3. [Integration Tests Guide](tests/TodoApp.IntegrationTests/TESTING_INSTRUCTIONS.md) ? Escribir tests para el endpoint

### "Necesito escribir tests para c�digo existente"

1. [TESTING_GUIDE.md](TESTING_GUIDE.md) ? Entender qu� tipo de test necesitas
2. Elegir gu�a espec�fica:
   - �M�todo individual? ? [Unit Tests Guide](tests/TodoApp.UnitTests/TESTING_INSTRUCTIONS.md)
   - �Endpoint HTTP? ? [Integration Tests Guide](tests/TodoApp.IntegrationTests/TESTING_INSTRUCTIONS.md)
   - �Flujo completo? ? [E2E Tests Guide](tests/TodoApp.E2ETests/TESTING_INSTRUCTIONS.md)

### "Necesito entender c�mo funciona el proyecto"

1. [README.md](README.md) ? Visi�n general
2. [DEVELOPMENT_GUIDE.md](src/TodoApp.Api/DEVELOPMENT_GUIDE.md) ? Arquitectura y patrones
3. Explorar el c�digo con la gu�a como referencia

### "Necesito mejorar la cobertura de tests"

1. [TESTING_GUIDE.md](TESTING_GUIDE.md) ? M�tricas y objetivos
2. [Unit Tests Guide](tests/TodoApp.UnitTests/TESTING_INSTRUCTIONS.md) ? Checklist de cobertura
3. [Integration Tests Guide](tests/TodoApp.IntegrationTests/TESTING_INSTRUCTIONS.md) ? Checklist de endpoints

### "Necesito configurar el entorno de desarrollo"

1. [README.md](README.md) ? Secci�n "Inicio R�pido"
2. [DEVELOPMENT_GUIDE.md](src/TodoApp.Api/DEVELOPMENT_GUIDE.md) ? Secci�n "Comandos Esenciales"

---

## ?? B�squeda R�pida por Tema

### Arquitectura y Patrones
? [DEVELOPMENT_GUIDE.md](src/TodoApp.Api/DEVELOPMENT_GUIDE.md)
- Arquitectura en capas
- Dependency Injection
- Repository Pattern
- Principios SOLID

### Convenciones de C�digo
? [DEVELOPMENT_GUIDE.md](src/TodoApp.Api/DEVELOPMENT_GUIDE.md)
- Nomenclatura de archivos
- Rutas REST
- C�digos de estado HTTP
- Manejo de errores

### Testing - General
? [TESTING_GUIDE.md](TESTING_GUIDE.md)
- Pir�mide de testing
- Tipos de pruebas
- Cu�ndo usar cada tipo
- Comandos de testing

### Testing - Unitario
? [Unit Tests Guide](tests/TodoApp.UnitTests/TESTING_INSTRUCTIONS.md)
- Uso de Moq
- Patr�n AAA
- Tests de servicios
- Tests de controladores (con mocks)

### Testing - Integraci�n
? [Integration Tests Guide](tests/TodoApp.IntegrationTests/TESTING_INSTRUCTIONS.md)
- WebApplicationFactory
- Tests de endpoints
- Validaci�n HTTP
- Flujos CRUD

### Testing - E2E
? [E2E Tests Guide](tests/TodoApp.E2ETests/TESTING_INSTRUCTIONS.md)
- Escenarios completos
- Selenium (UI)
- Pruebas de flujos
- Pruebas de carga

### API Endpoints y Ejemplos
? [README.md](README.md)
- Lista de endpoints
- Ejemplos de uso con curl
- Estructura de respuestas

### Comandos y CLI
? [README.md](README.md) + [DEVELOPMENT_GUIDE.md](src/TodoApp.Api/DEVELOPMENT_GUIDE.md)
- Comandos dotnet
- Comandos de testing
- Gesti�n de paquetes

---

## ?? Matriz de Contenido

| Tema | README | TESTING_GUIDE | DEVELOPMENT_GUIDE | Unit Tests | Integration Tests | E2E Tests |
|------|--------|---------------|-------------------|------------|-------------------|-----------|
| **Visi�n General** | ??? | ? | ? | ? | ? | ? |
| **Inicio R�pido** | ??? | ? | ? | ? | ? | ? |
| **Arquitectura** | ? | ? | ??? | ? | ? | ? |
| **Convenciones** | ? | ? | ??? | ? | ? | ? |
| **Testing General** | ? | ??? | ? | ? | ? | ? |
| **Unit Testing** | ? | ? | ? | ??? | ? | ? |
| **Integration Testing** | ? | ? | ? | ? | ??? | ? |
| **E2E Testing** | ? | ? | ? | ? | ? | ??? |
| **Desarrollo API** | ? | ? | ??? | ? | ? | ? |
| **Ejemplos de C�digo** | ? | ? | ??? | ??? | ??? | ??? |
| **Comandos** | ?? | ?? | ?? | ? | ? | ? |

**Leyenda:**
- ??? = Contenido principal y detallado
- ?? = Contenido secundario importante
- ? = Menci�n o referencia
- ? = No aplica o no cubierto

---

## ?? Glosario de T�rminos

### AAA Pattern
**Arrange-Act-Assert** - Patr�n de estructura de tests
? Ver todos los Testing Guides

### API
**Application Programming Interface** - Interfaz de programaci�n
? Ver [DEVELOPMENT_GUIDE.md](src/TodoApp.Api/DEVELOPMENT_GUIDE.md)

### CRUD
**Create, Read, Update, Delete** - Operaciones b�sicas
? Ver [README.md](README.md)

### DI
**Dependency Injection** - Inyecci�n de dependencias
? Ver [DEVELOPMENT_GUIDE.md](src/TodoApp.Api/DEVELOPMENT_GUIDE.md)

### E2E
**End-to-End** - Pruebas de extremo a extremo
? Ver [E2E Tests Guide](tests/TodoApp.E2ETests/TESTING_INSTRUCTIONS.md)

### Moq
Framework de mocking para .NET
? Ver [Unit Tests Guide](tests/TodoApp.UnitTests/TESTING_INSTRUCTIONS.md)

### REST
**Representational State Transfer** - Arquitectura de API
? Ver [DEVELOPMENT_GUIDE.md](src/TodoApp.Api/DEVELOPMENT_GUIDE.md)

### SUT
**System Under Test** - Sistema bajo prueba
? Ver todos los Testing Guides

### WebApplicationFactory
Clase para testing de integraci�n en ASP.NET Core
? Ver [Integration Tests Guide](tests/TodoApp.IntegrationTests/TESTING_INSTRUCTIONS.md)

### xUnit
Framework de testing para .NET
? Ver [TESTING_GUIDE.md](TESTING_GUIDE.md)

---

## ?? Comandos R�pidos de Referencia

```bash
# Ejecutar la API
dotnet run --project src/TodoApp.Api

# Ejecutar todos los tests
dotnet test

# Ejecutar tests espec�ficos
dotnet test tests/TodoApp.UnitTests
dotnet test tests/TodoApp.IntegrationTests
dotnet test tests/TodoApp.E2ETests

# Build del proyecto
dotnet build

# Restaurar dependencias
dotnet restore

# Ver Swagger UI
# ? https://localhost:5001/swagger (despu�s de ejecutar la API)
```

---

## ?? Necesitas Ayuda?

### Por Tema

- **API Development** ? [DEVELOPMENT_GUIDE.md](src/TodoApp.Api/DEVELOPMENT_GUIDE.md) ? Secci�n "Problemas Comunes"
- **Testing** ? [TESTING_GUIDE.md](TESTING_GUIDE.md) ? Secci�n "Errores Comunes"
- **Getting Started** ? [README.md](README.md) ? Secci�n "Inicio R�pido"

### Recursos Externos

- [ASP.NET Core Docs](https://learn.microsoft.com/en-us/aspnet/core/)
- [xUnit Docs](https://xunit.net/)
- [Moq Docs](https://github.com/moq/moq4/wiki/Quickstart)

---

**?? Tip:** Guarda este archivo como favorito para acceso r�pido a toda la documentaci�n!

**Versi�n:** 1.0  
**�ltima actualizaci�n:** 2024  
**Proyecto:** TodoApp - .NET 8.0

# ?? Índice de Documentación - TodoApp

## ?? Guía Rápida de Navegación

Esta es una guía de referencia rápida para localizar toda la documentación del proyecto TodoApp.

---

## ?? Documentación Principal

### 1. [README.md](README.md) - Visión General del Proyecto
**Lee esto primero** ?

Contiene:
- Descripción general del proyecto
- Inicio rápido y prerrequisitos
- Estructura del proyecto
- API endpoints y ejemplos
- Tecnologías utilizadas
- Roadmap y contribución

**Cuándo leer:** Al empezar con el proyecto

---

### 2. [TESTING_GUIDE.md](TESTING_GUIDE.md) - Guía General de Testing
**La guía maestra de testing** ??

Contiene:
- Los 3 tipos de pruebas (Unit, Integration, E2E)
- Cuándo usar cada tipo
- Pirámide de testing
- Comandos esenciales
- Workflow de testing
- Comparación entre tipos de test

**Cuándo leer:** Antes de escribir cualquier test

---

## ?? Documentación de Desarrollo

### 3. [src/TodoApp.Api/DEVELOPMENT_GUIDE.md](src/TodoApp.Api/DEVELOPMENT_GUIDE.md) - Guía de Desarrollo de la API
**Para desarrollar en el proyecto API** ???

Contiene:
- Arquitectura del proyecto
- Convenciones de código y nomenclatura
- Patrones de diseño implementados
- Cómo agregar nuevos endpoints
- Mejores prácticas
- Configuración y middleware
- Manejo de errores
- Comandos útiles

**Cuándo leer:** 
- Al agregar nuevos endpoints
- Al modificar servicios o controladores
- Al configurar el proyecto

---

## ?? Documentación de Testing

### 4. [tests/TodoApp.UnitTests/TESTING_INSTRUCTIONS.md](tests/TodoApp.UnitTests/TESTING_INSTRUCTIONS.md) - Pruebas Unitarias
**Para probar componentes aislados** ??

Contiene:
- Uso de **Moq** para mocks
- Pruebas de servicios y controladores
- Estructura AAA (Arrange-Act-Assert)
- Plantillas de tests unitarios
- Checklist de cobertura
- Objetivo: >80% cobertura

**Cuándo leer:**
- Al escribir pruebas para servicios
- Al testear lógica de negocio
- Al usar Moq para simular dependencias

---

### 5. [tests/TodoApp.IntegrationTests/TESTING_INSTRUCTIONS.md](tests/TodoApp.IntegrationTests/TESTING_INSTRUCTIONS.md) - Pruebas de Integración
**Para probar endpoints HTTP** ??

Contiene:
- Uso de **WebApplicationFactory**
- Pruebas de endpoints completos
- Validación de códigos HTTP
- Flujos CRUD completos
- Configuración de testhost.deps.json
- Objetivo: >70% endpoints

**Cuándo leer:**
- Al probar endpoints de la API
- Al validar integración entre componentes
- Al testear serialización JSON

---

### 6. [tests/TodoApp.E2ETests/TESTING_INSTRUCTIONS.md](tests/TodoApp.E2ETests/TESTING_INSTRUCTIONS.md) - Pruebas End-to-End
**Para probar flujos de usuario completos** ??

Contiene:
- Escenarios completos de usuario
- Estructura de pasos numerados
- Uso de **Selenium** (opcional)
- Pruebas de flujos críticos
- Manejo de cleanup
- Pruebas de carga

**Cuándo leer:**
- Al escribir escenarios de usuario
- Al probar flujos críticos completos
- Al usar Selenium para UI testing

---

## ??? Mapa de Decisión: ¿Qué Documento Leer?

```
???????????????????????????????????????
? ¿Qué quieres hacer?       ?
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
       ??? Probar método de servicio
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
1. [README.md](README.md) - Visión general
2. [DEVELOPMENT_GUIDE.md](src/TodoApp.Api/DEVELOPMENT_GUIDE.md) - Desarrollo de API
3. [Unit Tests Guide](tests/TodoApp.UnitTests/TESTING_INSTRUCTIONS.md) - Tests unitarios
4. [Integration Tests Guide](tests/TodoApp.IntegrationTests/TESTING_INSTRUCTIONS.md) - Tests de integración

### QA / Tester

**Orden de lectura:**
1. [README.md](README.md) - Visión general
2. [TESTING_GUIDE.md](TESTING_GUIDE.md) - Guía general de testing
3. [Integration Tests Guide](tests/TodoApp.IntegrationTests/TESTING_INSTRUCTIONS.md) - Tests de integración
4. [E2E Tests Guide](tests/TodoApp.E2ETests/TESTING_INSTRUCTIONS.md) - Tests E2E

### Tech Lead / Arquitecto

**Orden de lectura:**
1. [README.md](README.md) - Visión general
2. [DEVELOPMENT_GUIDE.md](src/TodoApp.Api/DEVELOPMENT_GUIDE.md) - Arquitectura
3. [TESTING_GUIDE.md](TESTING_GUIDE.md) - Estrategia de testing
4. Todos los guides específicos para revisión

### Nuevo en el Equipo

**Orden de lectura:**
1. [README.md](README.md) - ? Empezar aquí
2. [TESTING_GUIDE.md](TESTING_GUIDE.md) - Entender testing
3. [DEVELOPMENT_GUIDE.md](src/TodoApp.Api/DEVELOPMENT_GUIDE.md) - Aprender el desarrollo
4. Guides específicos según necesidad

---

## ?? Por Tarea

### "Necesito agregar un nuevo endpoint"

1. [DEVELOPMENT_GUIDE.md](src/TodoApp.Api/DEVELOPMENT_GUIDE.md) ? Sección "Agregar un Nuevo Endpoint"
2. [Unit Tests Guide](tests/TodoApp.UnitTests/TESTING_INSTRUCTIONS.md) ? Escribir tests para el servicio
3. [Integration Tests Guide](tests/TodoApp.IntegrationTests/TESTING_INSTRUCTIONS.md) ? Escribir tests para el endpoint

### "Necesito escribir tests para código existente"

1. [TESTING_GUIDE.md](TESTING_GUIDE.md) ? Entender qué tipo de test necesitas
2. Elegir guía específica:
   - ¿Método individual? ? [Unit Tests Guide](tests/TodoApp.UnitTests/TESTING_INSTRUCTIONS.md)
   - ¿Endpoint HTTP? ? [Integration Tests Guide](tests/TodoApp.IntegrationTests/TESTING_INSTRUCTIONS.md)
   - ¿Flujo completo? ? [E2E Tests Guide](tests/TodoApp.E2ETests/TESTING_INSTRUCTIONS.md)

### "Necesito entender cómo funciona el proyecto"

1. [README.md](README.md) ? Visión general
2. [DEVELOPMENT_GUIDE.md](src/TodoApp.Api/DEVELOPMENT_GUIDE.md) ? Arquitectura y patrones
3. Explorar el código con la guía como referencia

### "Necesito mejorar la cobertura de tests"

1. [TESTING_GUIDE.md](TESTING_GUIDE.md) ? Métricas y objetivos
2. [Unit Tests Guide](tests/TodoApp.UnitTests/TESTING_INSTRUCTIONS.md) ? Checklist de cobertura
3. [Integration Tests Guide](tests/TodoApp.IntegrationTests/TESTING_INSTRUCTIONS.md) ? Checklist de endpoints

### "Necesito configurar el entorno de desarrollo"

1. [README.md](README.md) ? Sección "Inicio Rápido"
2. [DEVELOPMENT_GUIDE.md](src/TodoApp.Api/DEVELOPMENT_GUIDE.md) ? Sección "Comandos Esenciales"

---

## ?? Búsqueda Rápida por Tema

### Arquitectura y Patrones
? [DEVELOPMENT_GUIDE.md](src/TodoApp.Api/DEVELOPMENT_GUIDE.md)
- Arquitectura en capas
- Dependency Injection
- Repository Pattern
- Principios SOLID

### Convenciones de Código
? [DEVELOPMENT_GUIDE.md](src/TodoApp.Api/DEVELOPMENT_GUIDE.md)
- Nomenclatura de archivos
- Rutas REST
- Códigos de estado HTTP
- Manejo de errores

### Testing - General
? [TESTING_GUIDE.md](TESTING_GUIDE.md)
- Pirámide de testing
- Tipos de pruebas
- Cuándo usar cada tipo
- Comandos de testing

### Testing - Unitario
? [Unit Tests Guide](tests/TodoApp.UnitTests/TESTING_INSTRUCTIONS.md)
- Uso de Moq
- Patrón AAA
- Tests de servicios
- Tests de controladores (con mocks)

### Testing - Integración
? [Integration Tests Guide](tests/TodoApp.IntegrationTests/TESTING_INSTRUCTIONS.md)
- WebApplicationFactory
- Tests de endpoints
- Validación HTTP
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
- Gestión de paquetes

---

## ?? Matriz de Contenido

| Tema | README | TESTING_GUIDE | DEVELOPMENT_GUIDE | Unit Tests | Integration Tests | E2E Tests |
|------|--------|---------------|-------------------|------------|-------------------|-----------|
| **Visión General** | ??? | ? | ? | ? | ? | ? |
| **Inicio Rápido** | ??? | ? | ? | ? | ? | ? |
| **Arquitectura** | ? | ? | ??? | ? | ? | ? |
| **Convenciones** | ? | ? | ??? | ? | ? | ? |
| **Testing General** | ? | ??? | ? | ? | ? | ? |
| **Unit Testing** | ? | ? | ? | ??? | ? | ? |
| **Integration Testing** | ? | ? | ? | ? | ??? | ? |
| **E2E Testing** | ? | ? | ? | ? | ? | ??? |
| **Desarrollo API** | ? | ? | ??? | ? | ? | ? |
| **Ejemplos de Código** | ? | ? | ??? | ??? | ??? | ??? |
| **Comandos** | ?? | ?? | ?? | ? | ? | ? |

**Leyenda:**
- ??? = Contenido principal y detallado
- ?? = Contenido secundario importante
- ? = Mención o referencia
- ? = No aplica o no cubierto

---

## ?? Glosario de Términos

### AAA Pattern
**Arrange-Act-Assert** - Patrón de estructura de tests
? Ver todos los Testing Guides

### API
**Application Programming Interface** - Interfaz de programación
? Ver [DEVELOPMENT_GUIDE.md](src/TodoApp.Api/DEVELOPMENT_GUIDE.md)

### CRUD
**Create, Read, Update, Delete** - Operaciones básicas
? Ver [README.md](README.md)

### DI
**Dependency Injection** - Inyección de dependencias
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
Clase para testing de integración en ASP.NET Core
? Ver [Integration Tests Guide](tests/TodoApp.IntegrationTests/TESTING_INSTRUCTIONS.md)

### xUnit
Framework de testing para .NET
? Ver [TESTING_GUIDE.md](TESTING_GUIDE.md)

---

## ?? Comandos Rápidos de Referencia

```bash
# Ejecutar la API
dotnet run --project src/TodoApp.Api

# Ejecutar todos los tests
dotnet test

# Ejecutar tests específicos
dotnet test tests/TodoApp.UnitTests
dotnet test tests/TodoApp.IntegrationTests
dotnet test tests/TodoApp.E2ETests

# Build del proyecto
dotnet build

# Restaurar dependencias
dotnet restore

# Ver Swagger UI
# ? https://localhost:5001/swagger (después de ejecutar la API)
```

---

## ?? Necesitas Ayuda?

### Por Tema

- **API Development** ? [DEVELOPMENT_GUIDE.md](src/TodoApp.Api/DEVELOPMENT_GUIDE.md) ? Sección "Problemas Comunes"
- **Testing** ? [TESTING_GUIDE.md](TESTING_GUIDE.md) ? Sección "Errores Comunes"
- **Getting Started** ? [README.md](README.md) ? Sección "Inicio Rápido"

### Recursos Externos

- [ASP.NET Core Docs](https://learn.microsoft.com/en-us/aspnet/core/)
- [xUnit Docs](https://xunit.net/)
- [Moq Docs](https://github.com/moq/moq4/wiki/Quickstart)

---

**?? Tip:** Guarda este archivo como favorito para acceso rápido a toda la documentación!

**Versión:** 1.0  
**Última actualización:** 2024  
**Proyecto:** TodoApp - .NET 8.0

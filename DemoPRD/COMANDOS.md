# Comandos Útiles - TodoApp

## Compilación y Ejecución

### Restaurar dependencias
```bash
dotnet restore
```

### Compilar la solución
```bash
dotnet build
```

### Ejecutar la API
```bash
cd src/TodoApp.Api
dotnet run
```

### Ejecutar con watch (recarga automática)
```bash
cd src/TodoApp.Api
dotnet watch run
```

## Pruebas

### Ejecutar todas las pruebas
```bash
dotnet test
```

### Ejecutar con cobertura
```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```

### Pruebas por proyecto

#### Pruebas unitarias
```bash
dotnet test tests/TodoApp.UnitTests/TodoApp.UnitTests.csproj
```

#### Pruebas de integración
```bash
dotnet test tests/TodoApp.IntegrationTests/TodoApp.IntegrationTests.csproj
```

#### Pruebas E2E
```bash
dotnet test tests/TodoApp.E2ETests/TodoApp.E2ETests.csproj
```

### Pruebas con salida detallada
```bash
dotnet test --logger "console;verbosity=detailed"
```

## Limpieza

### Limpiar archivos de compilación
```bash
dotnet clean
```

### Limpiar y reconstruir
```bash
dotnet clean && dotnet build
```

## Publicación

### Publicar para producción
```bash
dotnet publish -c Release -o ./publish
```

### Publicar autocontenido (incluye runtime)
```bash
dotnet publish -c Release -r win-x64 --self-contained -o ./publish
```

## Paquetes NuGet

### Agregar paquete
```bash
dotnet add package <NombrePaquete>
```

### Actualizar paquetes
```bash
dotnet restore
```

### Listar paquetes instalados
```bash
dotnet list package
```

## Información del proyecto

### Ver información de la SDK
```bash
dotnet --info
```

### Ver versión
```bash
dotnet --version
```

### Listar SDKs instalados
```bash
dotnet --list-sdks
```

## Debugging

### Ejecutar en modo Debug
```bash
dotnet run --configuration Debug
```

### Ejecutar en modo Release
```bash
dotnet run --configuration Release
```

## URLs de la Aplicación

- **HTTP**: http://localhost:5000
- **HTTPS**: https://localhost:5001
- **Swagger**: https://localhost:5001/swagger
- **Health**: https://localhost:5001/health (si se implementa)

## Variables de Entorno

### Configurar entorno de desarrollo
```bash
set ASPNETCORE_ENVIRONMENT=Development  # Windows
export ASPNETCORE_ENVIRONMENT=Development  # Linux/Mac
```

### Configurar entorno de producción
```bash
set ASPNETCORE_ENVIRONMENT=Production  # Windows
export ASPNETCORE_ENVIRONMENT=Production  # Linux/Mac
```

## Docker (Futuro)

### Construir imagen
```bash
docker build -t todoapp:latest .
```

### Ejecutar contenedor
```bash
docker run -d -p 8080:80 --name todoapp todoapp:latest
```

## Git

### Inicializar repositorio
```bash
git init
git add .
git commit -m "Initial commit - TodoApp solution"
```

### Crear rama de desarrollo
```bash
git checkout -b develop
```

## Análisis de Código

### Formato de código
```bash
dotnet format
```

### Análisis estático (si se configura)
```bash
dotnet build /p:RunAnalyzers=true
```

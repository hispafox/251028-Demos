REM Crear la solución
dotnet new sln -n GestionUsuarios

REM Crear el proyecto de API commo APIs Minimas 
dotnet new webapi -n GestionUsuarios.Api -f net8.0

REM Crear el proyecto de pruebas
dotnet new xunit -n GestionUsuarios.Tests -f net8.0

REM Agregar proyectos a la solución
dotnet sln add GestionUsuarios.Api/GestionUsuarios.Api.csproj
dotnet sln add GestionUsuarios.Tests/GestionUsuarios.Tests.csproj

REM Agregar referencia del proyecto de pruebas al proyecto de API
dotnet add GestionUsuarios.Tests/GestionUsuarios.Tests.csproj reference GestionUsuarios.Api/GestionUsuarios.Api.csproj

REM Agregar paquetes necesarios para las pruebas
dotnet add GestionUsuarios.Tests/GestionUsuarios.Tests.csproj package Moq
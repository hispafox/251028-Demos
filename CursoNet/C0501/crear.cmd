# Crear la estructura de directorios
mkdir TodoApp
cd TodoApp
mkdir src tests

# Crear solución
dotnet new sln -n TodoApp

# Crear el proyecto principal (API con controladores en .NET 8)
cd src
dotnet new webapi -o TodoApp.Api --use-controllers --framework net8.0

# Crear proyectos de test (todos apuntando a .NET 8)
cd ../tests
dotnet new xunit -o TodoApp.UnitTests --framework net8.0
dotnet new xunit -o TodoApp.IntegrationTests --framework net8.0
dotnet new xunit -o TodoApp.E2ETests --framework net8.0

# Volver a la raíz para añadir todos los proyectos a la solución
cd ..
dotnet sln add src/TodoApp.Api/TodoApp.Api.csproj
dotnet sln add tests/TodoApp.UnitTests/TodoApp.UnitTests.csproj
dotnet sln add tests/TodoApp.IntegrationTests/TodoApp.IntegrationTests.csproj
dotnet sln add tests/TodoApp.E2ETests/TodoApp.E2ETests.csproj

# Agregar referencias de tests al proyecto principal
dotnet add tests/TodoApp.UnitTests/TodoApp.UnitTests.csproj reference src/TodoApp.Api/TodoApp.Api.csproj
dotnet add tests/TodoApp.IntegrationTests/TodoApp.IntegrationTests.csproj reference src/TodoApp.Api/TodoApp.Api.csproj
dotnet add tests/TodoApp.E2ETests/TodoApp.E2ETests.csproj reference src/TodoApp.Api/TodoApp.Api.csproj

# Agregar paquetes NuGet necesarios
cd tests/TodoApp.UnitTests
dotnet add package Moq

cd ../TodoApp.IntegrationTests
dotnet add package Microsoft.AspNetCore.Mvc.Testing

cd ../TodoApp.E2ETests
dotnet add package Microsoft.AspNetCore.Mvc.Testing
dotnet add package Selenium.WebDriver
dotnet add package Selenium.Support

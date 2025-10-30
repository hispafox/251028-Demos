using Microsoft.EntityFrameworkCore;
using TodoApp.Api.Data;
using TodoApp.Api.Data.Extensions;
using TodoApp.Api.Data.Repositories;
using TodoApp.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurar DbContext según el entorno
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var dbProvider = builder.Configuration["DatabaseProvider"] ?? "SQLite";

switch (dbProvider)
{
    case "SqlServer":
        builder.Services.AddDbContext<TodoDbContext>(options =>
            options.UseSqlServer(connectionString));
        break;

    case "SQLite":
    default:
        builder.Services.AddDbContext<TodoDbContext>(options =>
            options.UseSqlite(connectionString ?? "Data Source=todos.db"));
        break;
}

// Registrar repositorios
builder.Services.AddScoped<ITodoRepository, TodoRepository>();

// Registrar servicios
builder.Services.AddScoped<ITodoService, TodoService>();

// Configurar AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "TodoApp API",
        Version = "v2.1",
        Description = "API RESTful para gestión de tareas con Entity Framework Core y Bogus - Proyecto educativo .NET 8",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "TodoApp Team",
            Url = new Uri("https://github.com/hispafox/251028-Demos")
        }
    });

    // Incluir comentarios XML para documentación
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

// Aplicar migraciones y seeding en desarrollo
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<TodoDbContext>();
        var logger = services.GetRequiredService<ILogger<Program>>();

        // Aplicar migraciones
        logger.LogInformation("Aplicando migraciones...");
        await context.Database.MigrateAsync();
        logger.LogInformation("? Migraciones aplicadas exitosamente");

        // Aplicar seeding (solo si la configuración lo permite)
        var seedDatabase = builder.Configuration.GetValue<bool>("SeedDatabase", true);
        if (seedDatabase)
        {
            logger.LogInformation("Aplicando seeding de datos con Bogus...");
            await context.SeedDatabaseAsync(clearExisting: false);
            logger.LogInformation("? Seeding aplicado exitosamente - 40 tareas generadas");
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "? Error al aplicar migraciones o seeding");
    }

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoApp API v2.1");
        options.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// Hacer Program parcial para accesibilidad en pruebas
public partial class Program { }

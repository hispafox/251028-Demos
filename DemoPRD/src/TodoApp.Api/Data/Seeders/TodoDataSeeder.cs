using Bogus;
using TodoApp.Api.Data.Entities;

namespace TodoApp.Api.Data.Seeders
{
    /// <summary>
    /// Generador de datos de prueba para TodoEntity usando Bogus
    /// </summary>
  public static class TodoDataSeeder
    {
  /// <summary>
      /// Genera una lista de TodoEntities con datos realistas
 /// </summary>
        /// <param name="count">N�mero de registros a generar</param>
        /// <param name="seed">Semilla para reproducibilidad (opcional)</param>
        /// <returns>Lista de TodoEntity con datos generados</returns>
        public static List<TodoEntity> GenerateTodos(int count = 50, int? seed = null)
  {
          // Configurar semilla para reproducibilidad
            if (seed.HasValue)
            {
       Randomizer.Seed = new Random(seed.Value);
   }
        
          var todoId = 1;
         
  // Configurar el generador con Bogus
     var faker = new Faker<TodoEntity>("es") // Locale espa�ol
       .StrictMode(false)
             .RuleFor(t => t.Id, f => todoId++)
     .RuleFor(t => t.Title, f => GenerateTodoTitle(f))
          .RuleFor(t => t.IsComplete, f => f.Random.Bool(0.3f)) // 30% completadas
 .RuleFor(t => t.CreatedAt, f => f.Date.Between(
        DateTime.UtcNow.AddMonths(-6), 
      DateTime.UtcNow))
           .RuleFor(t => t.UpdatedAt, (f, t) => 
   {
         // Solo tareas completadas tienen UpdatedAt
     if (t.IsComplete)
        {
    return f.Date.Between(t.CreatedAt, DateTime.UtcNow);
    }
return null;
    });
       
     return faker.Generate(count);
        }
    
  /// <summary>
        /// Genera t�tulos de tareas variados y realistas
        /// </summary>
    private static string GenerateTodoTitle(Faker faker)
    {
         var templates = new[]
{
  // Tareas de trabajo (25%)
$"Revisar {faker.Commerce.Product()}",
   $"Preparar informe de {faker.Commerce.Department()}",
    $"Reuni�n con {faker.Name.FullName()}",
      $"Enviar email a {faker.Name.FirstName()}",
   $"Actualizar documentaci�n de {faker.Hacker.Noun()}",
   
      // Tareas t�cnicas (25%)
      $"Implementar {faker.Hacker.Verb()} en {faker.Hacker.Noun()}",
   $"Corregir bug en {faker.System.FileName()}",
      $"Optimizar {faker.Database.Column()}",
     $"Refactorizar m�dulo de {faker.Commerce.ProductName()}",
  $"Configurar {faker.System.CommonFileName()}",
   
// Tareas personales (25%)
      $"Comprar {faker.Commerce.Product()}",
     $"Llamar a {faker.Name.FirstName()}",
        $"Leer {faker.Lorem.Sentence(3)}",
       $"Organizar {faker.Commerce.Department()}",
      $"Planificar {faker.Commerce.ProductName()}",
        
   // Tareas administrativas (25%)
      $"Aprobar {faker.Finance.AccountName()}",
   $"Revisar solicitud de {faker.Name.FullName()}",
 $"Completar formulario de {faker.Commerce.Department()}",
     $"Firmar contrato con {faker.Company.CompanyName()}",
        $"Verificar {faker.Finance.TransactionType()}"
  };
   
   return faker.PickRandom(templates);
    }
        
        /// <summary>
        /// Genera tareas categorizadas por tipo
      /// </summary>
      /// <param name="seed">Semilla para reproducibilidad</param>
        /// <returns>Lista de TodoEntity categorizada</returns>
        public static List<TodoEntity> GenerateCategorizedTodos(int seed = 42)
  {
   Randomizer.Seed = new Random(seed);
      var todos = new List<TodoEntity>();
       var id = 1;
     
    // Tareas de desarrollo (15 items)
 todos.AddRange(GenerateDevelopmentTodos(id, 15));
  id += 15;
      
   // Tareas personales (10 items)
todos.AddRange(GeneratePersonalTodos(id, 10));
        id += 10;
            
// Tareas administrativas (10 items)
     todos.AddRange(GenerateAdministrativeTodos(id, 10));
         id += 10;
            
            // Tareas de reuniones (5 items)
            todos.AddRange(GenerateMeetingTodos(id, 5));
       
            return todos;
        }
        
        #region Generadores Espec�ficos por Categor�a  
     private static List<TodoEntity> GenerateDevelopmentTodos(int startId, int count)
    {
      var currentId = startId;
  var faker = new Faker<TodoEntity>("es")
  .RuleFor(t => t.Id, f => currentId++)
   .RuleFor(t => t.Title, f => f.PickRandom(
     "Implementar autenticaci�n JWT",
  $"Crear endpoint para {f.Hacker.Noun()}",
    $"Corregir bug en {f.System.FileName()}",
   $"Escribir tests para {f.Hacker.Verb()}",
        $"Optimizar consulta de {f.Database.Column()}",
  $"Refactorizar clase {f.Name.LastName()}Service",
  "Actualizar dependencias del proyecto",
  $"Implementar patr�n {f.Hacker.Noun()}",
   "Configurar CI/CD pipeline",
 $"Documentar API endpoint {f.Hacker.Abbreviation()}"
 ))
     .RuleFor(t => t.IsComplete, f => f.Random.Bool(0.4f))
     .RuleFor(t => t.CreatedAt, f => f.Date.Recent(30))
       .RuleFor(t => t.UpdatedAt, (f, t) => t.IsComplete ? f.Date.Recent(7) : null);
    
       return faker.Generate(count);
      }
        
     private static List<TodoEntity> GeneratePersonalTodos(int startId, int count)
        {
    var currentId = startId;
   var faker = new Faker<TodoEntity>("es")
          .RuleFor(t => t.Id, f => currentId++)
         .RuleFor(t => t.Title, f => f.PickRandom(
            $"Comprar {f.Commerce.Product()}",
   "Llamar al m�dico",
       $"Renovar suscripci�n de {f.Commerce.ProductName()}",
  $"Leer libro: {f.Lorem.Sentence(3)}",
  "Hacer ejercicio",
    "Preparar presentaci�n",
       "Organizar escritorio",
    $"Pagar factura de {f.Commerce.Department()}",
        $"Reservar cita con {f.Name.FirstName()}",
  "Planificar vacaciones"
       ))
     .RuleFor(t => t.IsComplete, f => f.Random.Bool(0.2f))
      .RuleFor(t => t.CreatedAt, f => f.Date.Recent(60))
      .RuleFor(t => t.UpdatedAt, (f, t) => t.IsComplete ? f.Date.Recent(30) : null);
      
   return faker.Generate(count);
   }
   
  private static List<TodoEntity> GenerateAdministrativeTodos(int startId, int count)
   {
  var currentId = startId;
     var faker = new Faker<TodoEntity>("es")
   .RuleFor(t => t.Id, f => currentId++)
    .RuleFor(t => t.Title, f => f.PickRandom(
       "Aprobar timesheet del equipo",
        $"Revisar presupuesto de {f.Commerce.Department()}",
$"Completar evaluaci�n de {f.Name.FullName()}",
    $"Firmar contrato con {f.Company.CompanyName()}",
    $"Actualizar pol�tica de {f.Commerce.ProductName()}",
  $"Revisar solicitud de {f.Name.FirstName()}",
$"Archivar documentos de {f.Date.Month()}",
    "Preparar reporte trimestral",
    "Organizar reuni�n de planificaci�n",
   $"Verificar compliance de {f.Commerce.Department()}"
 ))
  .RuleFor(t => t.IsComplete, f => f.Random.Bool(0.5f))
  .RuleFor(t => t.CreatedAt, f => f.Date.Recent(45))
    .RuleFor(t => t.UpdatedAt, (f, t) => t.IsComplete ? f.Date.Recent(15) : null);
     
   return faker.Generate(count);
        }
        
   private static List<TodoEntity> GenerateMeetingTodos(int startId, int count)
   {
 var currentId = startId;
    var faker = new Faker<TodoEntity>("es")
     .RuleFor(t => t.Id, f => currentId++)
     .RuleFor(t => t.Title, f => f.PickRandom(
      "Reuni�n de standup diario",
    $"Sprint planning con {f.Name.FirstName()}",
  "Retrospectiva del sprint",
           "Revisi�n de c�digo con el equipo",
  $"One-on-one con {f.Name.FullName()}",
   "Demo para stakeholders",
       "Sesi�n de arquitectura",
      $"Reuni�n con cliente {f.Company.CompanyName()}",
     $"Training sobre {f.Hacker.Noun()}",
     "Comit� de revisi�n t�cnica"
 ))
    .RuleFor(t => t.IsComplete, f => f.Random.Bool(0.6f))
 .RuleFor(t => t.CreatedAt, f => f.Date.Recent(14))
     .RuleFor(t => t.UpdatedAt, (f, t) => t.IsComplete ? f.Date.Recent(7) : null);
    
 return faker.Generate(count);
  }
     
  #endregion
    }
}

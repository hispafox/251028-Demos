using TodoApp.Api.Data.Entities;

namespace TodoApp.Api.Data.Repositories
{
    /// <summary>
    /// Interfaz espec�fica de repositorio para TodoEntity
    /// </summary>
  public interface ITodoRepository : IRepository<TodoEntity>
    {
  Task<IEnumerable<TodoEntity>> GetCompletedAsync();
    Task<IEnumerable<TodoEntity>> GetPendingAsync();
     Task<int> GetCountAsync();
    }
}

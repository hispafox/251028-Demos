using Microsoft.EntityFrameworkCore;
using TodoApp.Api.Data.Entities;

namespace TodoApp.Api.Data.Repositories
{
    /// <summary>
    /// Implementación específica de repositorio para TodoEntity
    /// </summary>
    public class TodoRepository : Repository<TodoEntity>, ITodoRepository
    {
     public TodoRepository(TodoDbContext context) : base(context)
        {
        }
   
        public async Task<IEnumerable<TodoEntity>> GetCompletedAsync()
   {
       return await _dbSet
         .Where(t => t.IsComplete)
       .OrderByDescending(t => t.UpdatedAt)
        .ToListAsync();
   }
        
      public async Task<IEnumerable<TodoEntity>> GetPendingAsync()
        {
     return await _dbSet
    .Where(t => !t.IsComplete)
    .OrderBy(t => t.CreatedAt)
   .ToListAsync();
        }
        
        public async Task<int> GetCountAsync()
        {
      return await _dbSet.CountAsync();
  }
        
        // Sobrescribir para agregar tracking de UpdatedAt
        public override async Task<TodoEntity> UpdateAsync(TodoEntity entity)
  {
  entity.UpdatedAt = DateTime.UtcNow;
   return await base.UpdateAsync(entity);
      }
    }
}

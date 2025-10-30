namespace TodoApp.Api.Data.Repositories
{
    /// <summary>
    /// Interfaz gen�rica de repositorio
 /// </summary>
    /// <typeparam name="T">Tipo de entidad</typeparam>
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
     Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    }
}

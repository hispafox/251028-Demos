using TodoApp.Api.Models;

namespace TodoApp.Api.Services
{
    public interface ITodoService
    {
        IEnumerable<TodoItem> GetAll();
        TodoItem? GetById(int id);
        TodoItem Add(TodoItem item);
        TodoItem? Update(int id, TodoItem item);
        bool Delete(int id);
    }
}

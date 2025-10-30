using TodoApp.Api.Models;

namespace TodoApp.Api.Services
{
    public class TodoService : ITodoService
    {
        private readonly List<TodoItem> _todos;
        private int _nextId = 1;

        public TodoService()
        {
            _todos = new List<TodoItem>();
        }

        public IEnumerable<TodoItem> GetAll()
        {
            return _todos;
        }

        public TodoItem? GetById(int id)
        {
            return _todos.FirstOrDefault(t => t.Id == id);
        }

        public TodoItem Add(TodoItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (string.IsNullOrWhiteSpace(item.Title))
                throw new ArgumentException("El título no puede estar vacío", nameof(item.Title));

            var todoItem = new TodoItem
            {
                Id = _nextId++,
                Title = item.Title,
                IsComplete = item.IsComplete
            };

            
            _todos.Add(todoItem);
            return todoItem;
        }

        public TodoItem? Update(int id, TodoItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (string.IsNullOrWhiteSpace(item.Title))
                throw new ArgumentException("El título no puede estar vacío", nameof(item.Title));

            var existingItem = GetById(id);
            if (existingItem == null)
                return null;

            existingItem.Title = item.Title;
            existingItem.IsComplete = item.IsComplete;

            return existingItem;
        }

        public bool Delete(int id)
        {
            var item = GetById(id);
            if (item == null)
                return false;

            return _todos.Remove(item);
        }
    }
}

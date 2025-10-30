using AutoMapper;
using TodoApp.Api.Data.Entities;
using TodoApp.Api.Data.Repositories;
using TodoApp.Api.DTOs;

namespace TodoApp.Api.Services;

/// <summary>
/// Implementación del servicio de gestión de tareas con persistencia
/// </summary>
public class TodoService : ITodoService
{
    private readonly ITodoRepository _repository;
    private readonly IMapper _mapper;

    public TodoService(ITodoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TodoItemDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<TodoItemDto>>(entities);
    }

    public async Task<TodoItemDto?> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : _mapper.Map<TodoItemDto>(entity);
    }

    public async Task<TodoItemDto> CreateAsync(CreateTodoItemDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
            throw new ArgumentException("El título no puede estar vacío");

        var entity = _mapper.Map<TodoEntity>(dto);
        entity.CreatedAt = DateTime.UtcNow;

        var created = await _repository.AddAsync(entity);
        return _mapper.Map<TodoItemDto>(created);
    }

    public async Task<TodoItemDto?> UpdateAsync(int id, UpdateTodoItemDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
            throw new ArgumentException("El título no puede estar vacío");

        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            return null;

        entity.Title = dto.Title;
        entity.IsComplete = dto.IsComplete;
        entity.UpdatedAt = DateTime.UtcNow;

        var updated = await _repository.UpdateAsync(entity);
        return _mapper.Map<TodoItemDto>(updated);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<TodoItemDto>> GetCompletedAsync()
    {
        var entities = await _repository.GetCompletedAsync();
        return _mapper.Map<IEnumerable<TodoItemDto>>(entities);
    }

    public async Task<IEnumerable<TodoItemDto>> GetPendingAsync()
    {
        var entities = await _repository.GetPendingAsync();
        return _mapper.Map<IEnumerable<TodoItemDto>>(entities);
    }
}

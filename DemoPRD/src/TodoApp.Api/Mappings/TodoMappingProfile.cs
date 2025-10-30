using AutoMapper;
using TodoApp.Api.Data.Entities;
using TodoApp.Api.DTOs;

namespace TodoApp.Api.Mappings
{
    /// <summary>
    /// Perfil de AutoMapper para mapeo entre entidades y DTOs
    /// </summary>
    public class TodoMappingProfile : Profile
    {
  public TodoMappingProfile()
 {
  // Entity -> DTO (para lectura)
   CreateMap<TodoEntity, TodoItemDto>();
  
// CreateDTO -> Entity (para creación)
       CreateMap<CreateTodoItemDto, TodoEntity>()
           .ForMember(dest => dest.Id, opt => opt.Ignore())
          .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
       .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
            
// UpdateDTO -> Entity (para actualización)
     CreateMap<UpdateTodoItemDto, TodoEntity>()
      .ForMember(dest => dest.Id, opt => opt.Ignore())
  .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
   .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
        }
}
}

using System.ComponentModel.DataAnnotations;

namespace TareasAPI.Models;

/// <summary>
/// Representa un proyecto que puede contener múltiples tareas.
/// </summary>
/// <remarks>
/// Un proyecto es una entidad que agrupa tareas relacionadas y permite organizarlas.
/// Cada proyecto tiene un nombre único, descripción opcional y fechas de auditoría.
/// </remarks>
public class Project
{
    /// <summary>
    /// Obtiene o establece el identificador único del proyecto.
    /// </summary>
    /// <value>El identificador del proyecto.</value>
    public int Id { get; set; }

    /// <summary>
    /// Obtiene o establece el nombre del proyecto.
    /// </summary>
    /// <value>El nombre del proyecto, con una longitud máxima de 200 caracteres.</value>
    /// <remarks>
    /// Este campo es obligatorio y no puede estar vacío.
    /// </remarks>
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Obtiene o establece la descripción del proyecto.
    /// </summary>
    /// <value>La descripción del proyecto, o <c>null</c> si no se proporciona.</value>
    public string? Description { get; set; }

    /// <summary>
    /// Obtiene o establece la fecha y hora de creación del proyecto.
    /// </summary>
    /// <value>La fecha y hora en que se creó el proyecto.</value>
    /// <remarks>
    /// Este campo es obligatorio y se establece automáticamente al crear el proyecto.
    /// </remarks>
    [Required]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Obtiene o establece la fecha y hora de la última actualización del proyecto.
    /// </summary>
    /// <value>La fecha y hora de la última actualización, o <c>null</c> si nunca se ha actualizado.</value>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Obtiene o establece la colección de tareas asociadas al proyecto.
    /// </summary>
    /// <value>Una colección de tareas que pertenecen a este proyecto, o <c>null</c> si no hay tareas cargadas.</value>
    /// <remarks>
    /// Esta propiedad es una propiedad de navegación utilizada por Entity Framework.
    /// </remarks>
    public ICollection<Tarea>? Tareas { get; set; }
}

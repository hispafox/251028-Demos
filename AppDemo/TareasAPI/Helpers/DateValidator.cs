using System;

namespace TareasAPI.Validation
{
    /// <summary>
    /// Proporciona utilidades para validar relaciones entre fechas.
    /// </summary>
    public static class DateValidator
    {
        /// <summary>
        /// Determina si <paramref name="start"/> es estrictamente anterior a <paramref name="end"/>.
        /// </summary>
        /// <param name="start">Fecha de inicio. No puede ser <c>null</c>.</param>
        /// <param name="end">Fecha de fin. No puede ser <c>null</c>.</param>
        /// <returns>
        /// <c>true</c> si <paramref name="start"/> es estrictamente anterior a <paramref name="end"/>; de lo contrario, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">Si <paramref name="start"/> o <paramref name="end"/> son <c>null</c>.</exception>
        /// <example>
        /// // Devuelve true si2025-01-01 es anterior a2025-02-01
        /// DateValidator.IsStartBeforeEnd(new DateTime(2025,1,1), new DateTime(2025,2,1));
        /// </example>
        public static bool IsStartBeforeEnd(DateTime? start, DateTime? end)
        {
            if (start is null) throw new ArgumentNullException(nameof(start));
            if (end is null) throw new ArgumentNullException(nameof(end));

            return start.Value < end.Value;
        }
    }
}

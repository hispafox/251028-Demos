using System;

namespace TareasAPI.Validation
{
    public static class DateValidator
    {
        public static bool IsStartBeforeEnd(DateTime? start, DateTime? end)
        {
            if (start is null) throw new ArgumentNullException(nameof(start));
            if (end is null) throw new ArgumentNullException(nameof(end));

            return start.Value < end.Value;
        }
    }
}

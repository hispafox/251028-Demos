using System;
using System.ComponentModel.DataAnnotations;

namespace TareasAPI.Validation;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class StartBeforeEndAttribute : ValidationAttribute
{
 private readonly string _startProperty;
 private readonly string _endProperty;

 public StartBeforeEndAttribute(string startProperty, string endProperty)
 {
 _startProperty = startProperty;
 _endProperty = endProperty;
 ErrorMessage = "La fecha de inicio debe ser anterior a la fecha límite.";
 }

 protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
 {
 var startProp = validationContext.ObjectType.GetProperty(_startProperty);
 var endProp = validationContext.ObjectType.GetProperty(_endProperty);

 if (startProp is null || endProp is null)
 return new ValidationResult($"Propiedad no encontrada: {_startProperty} o {_endProperty}");

 var startVal = startProp.GetValue(validationContext.ObjectInstance);
 var endVal = endProp.GetValue(validationContext.ObjectInstance);

 // If either value is null, let [Required] handle missing values
 if (startVal is null || endVal is null)
 return ValidationResult.Success;

 DateTime? start = startVal as DateTime?;
 DateTime? end = endVal as DateTime?;

 // Handle boxed DateTime (non-nullable properties)
 if (start is null && startVal is DateTime sd) start = sd;
 if (end is null && endVal is DateTime ed) end = ed;

 if (start is null || end is null)
 return ValidationResult.Success;

 if (!DateValidator.IsStartBeforeEnd(start, end))
 return new ValidationResult(ErrorMessage, new[] { _startProperty, _endProperty });

 return ValidationResult.Success;
 }
}

# Instrucciones para GitHub Copilot

## Contexto del Proyecto

Este proyecto es una solución de calculadora matemática desarrollada en C# que incluye una biblioteca de clases, una aplicación de consola y un proyecto de pruebas unitarias.

## Características del Proyecto

- **Lenguaje**: C# 12.0
- **Framework**: .NET 8
- **Tipo de solución**: Solución con múltiples proyectos
- **Proyectos incluidos**:
  - `BibliotecaCalculadora`: Biblioteca de clases con operaciones matemáticas
  - `CalculadoraConsola`: Aplicación de consola para usar la calculadora
  - `BibliotecaCalculadora.Tests`: Proyecto de pruebas unitarias

## Arquitectura y Estándares

### Estructura de la Solución

```
CalculadoraSolucion/
??? BibliotecaCalculadora/      # Biblioteca principal
?   ??? Calculadora.cs      # Clase con operaciones matemáticas
??? CalculadoraConsola/    # Aplicación de consola
??? BibliotecaCalculadora.Tests/   # Pruebas unitarias
?   ??? LaCalculadoraDeberia.cs    # Tests con xUnit
??? .github/
    ??? agents.md                   # Este archivo
```

### Estilo de Código

1. **Nomenclatura**: 
   - Utilizar PascalCase para nombres de métodos públicos y clases
   - Nombres descriptivos en español para métodos
2. **Manejo de errores**: 
   - Lanzar excepciones específicas para casos de error (ej: `ArgumentException`, `DivideByZeroException`)
   - Incluir mensajes descriptivos en español
3. **Documentación**: Agregar comentarios XML para métodos públicos siguiendo el formato estándar de C#

### Clase Principal: `Calculadora`

Ubicación: `BibliotecaCalculadora\Calculadora.cs`

La clase `Calculadora` proporciona las siguientes operaciones:

- `Sumar(int a, int b)`: Suma dos números enteros
- `Restar(int a, int b)`: Resta dos números enteros
- `Multiplicar(int a, int b)`: Multiplica dos números enteros
- `Dividir(int a, int b)`: Divide dos números enteros (considerar división por cero)
- `Potencia(int a, int b)`: Calcula la potencia de un número
- `RaizCuadrada(int a)`: Calcula la raíz cuadrada de un número (valida números negativos)

## Guías para Generación de Código

### Al agregar nuevos métodos a la clase Calculadora

1. Usar tipos de retorno explícitos
2. Validar entradas cuando sea necesario
3. Lanzar excepciones apropiadas con mensajes en español
4. Mantener la firma de métodos simple (preferir tipos primitivos)
5. Usar conversiones explícitas cuando se trabaje con `Math` (ej: `(int)Math.Pow()`)
6. Mantener consistencia con el estilo existente en el archivo

### Validaciones Importantes

- **División por cero**: Siempre validar antes de dividir
- **Raíces cuadradas**: Validar que el número no sea negativo
- **Potencias**: Considerar overflow para exponentes grandes
- **Operaciones con enteros**: Tener en cuenta truncamiento en conversiones

### Al crear pruebas unitarias

1. **Framework**: Usar xUnit como framework de pruebas
2. **Patrón**: Seguir el patrón AAA (Arrange, Act, Assert)
3. **Nomenclatura**: Usar el formato `NombreMetodo_Escenario_ResultadoEsperado`
4. **Cobertura**: Probar casos normales, límite y excepcionales
5. **Clase de tests**: Usar la convención `LaCalculadoraDeberia` para la clase de tests
6. **Assertions**: Usar aserciones claras y descriptivas

### Ejemplo de método con documentación completa

```csharp
/// <summary>
/// Calcula el módulo de dos números enteros.
/// </summary>
/// <param name="a">El dividendo</param>
/// <param name="b">El divisor</param>
/// <returns>El resto de la división entre a y b</returns>
/// <exception cref="DivideByZeroException">Se lanza cuando b es igual a cero</exception>
public int Modulo(int a, int b)
{
    if (b == 0)
        throw new DivideByZeroException("No se puede calcular el módulo con divisor cero.");
    
    return a % b;
}
```

### Ejemplo de test unitario

```csharp
[Fact]
public void Modulo_CuandoSeCalculaElRestoDeUnaDivision_DeberiaRetornarElValorCorrecto()
{
    // Arrange
    var calculadora = new Calculadora();
    
    // Act
    var resultado = calculadora.Modulo(10, 3);
    
    // Assert
 Assert.Equal(1, resultado);
}

[Fact]
public void Modulo_CuandoElDivisorEsCero_DeberiaLanzarDivideByZeroException()
{
    // Arrange
    var calculadora = new Calculadora();
    
    // Act & Assert
    Assert.Throws<DivideByZeroException>(() => calculadora.Modulo(10, 0));
}
```

## Convenciones Específicas del Proyecto

### Idioma
- Todos los mensajes de error y comentarios deben estar en **español**
- Los nombres de métodos en español (Sumar, Restar, etc.)
- Los nombres de tests en español siguiendo el patrón BDD

### Tipos de datos
- Preferir `int` sobre `double` o `decimal` para mantener consistencia
- Usar conversiones explícitas cuando sea necesario (ej: `(int)Math.Pow()`)
- Retornar tipos concretos, no usar `var` en firmas de métodos

### Manejo de excepciones
- Usar excepciones del sistema cuando sea apropiado:
  - `System.DivideByZeroException` para divisiones por cero
  - `System.ArgumentException` para argumentos inválidos
  - `System.OverflowException` para desbordamientos
- Incluir mensajes descriptivos en español

### Comentarios
- NO dejar código comentado en commits finales
- Usar comentarios XML (`///`) para documentación pública
- Evitar comentarios innecesarios que expliquen lo obvio

## Mejoras Futuras Sugeridas

Al trabajar con este proyecto, considera:

1. **Tipos numéricos**:
   - Agregar soporte para operaciones con números decimales (`decimal` o `double`)
   - Crear sobrecargas de métodos para diferentes tipos numéricos

2. **Operaciones adicionales**:
   - Implementar más operaciones matemáticas (logaritmos, trigonometría, factorial)
   - Agregar operaciones con porcentajes
   - Implementar operaciones con fracciones

3. **Diseño**:
   - Crear una interfaz `ICalculadora` para facilitar testing y extensibilidad
   - Considerar el patrón Strategy para diferentes tipos de cálculos
   - Implementar un sistema de historial de operaciones

4. **Validaciones**:
   - Agregar validación de overflow/underflow
   - Implementar manejo de precisión en operaciones con decimales
   - Validar rangos de entrada para potencias y raíces

5. **Documentación**:
   - Agregar documentación XML completa para todos los métodos públicos
   - Incluir ejemplos de uso en los comentarios XML
   - Documentar excepciones lanzadas

6. **Testing**:
   - Aumentar cobertura de pruebas
   - Agregar pruebas de integración para la aplicación de consola
   - Implementar pruebas parametrizadas con `[Theory]` y `[InlineData]`

## Directrices para GitHub Copilot

Cuando generes código para este proyecto:

1. **Mantén la consistencia**: Sigue el estilo y estructura existente
2. **Valida siempre**: Incluye validaciones apropiadas para cada operación
3. **Documenta**: Agrega comentarios XML para métodos públicos
4. **Prueba**: Genera tests correspondientes para cada nueva funcionalidad
5. **Español**: Usa nombres y mensajes en español
6. **Simplicidad**: Mantén el código simple y legible
7. **Tipos explícitos**: No uses `var` en declaraciones de métodos o cuando el tipo no sea obvio

## Referencias y Buenas Prácticas

- Seguir las convenciones de código de C# (Microsoft Coding Conventions)
- Aplicar principios SOLID cuando sea apropiado
- Mantener métodos cortos y con una sola responsabilidad
- Escribir código auto-documentado complementado con comentarios XML
- Priorizar la legibilidad sobre la concisión excesiva

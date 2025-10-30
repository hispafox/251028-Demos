# Instrucciones para GitHub Copilot

## Contexto del Proyecto

Este proyecto es una soluci�n de calculadora matem�tica desarrollada en C# que incluye una biblioteca de clases, una aplicaci�n de consola y un proyecto de pruebas unitarias.

## Caracter�sticas del Proyecto

- **Lenguaje**: C# 12.0
- **Framework**: .NET 8
- **Tipo de soluci�n**: Soluci�n con m�ltiples proyectos
- **Proyectos incluidos**:
  - `BibliotecaCalculadora`: Biblioteca de clases con operaciones matem�ticas
  - `CalculadoraConsola`: Aplicaci�n de consola para usar la calculadora
  - `BibliotecaCalculadora.Tests`: Proyecto de pruebas unitarias

## Arquitectura y Est�ndares

### Estructura de la Soluci�n

```
CalculadoraSolucion/
??? BibliotecaCalculadora/      # Biblioteca principal
?   ??? Calculadora.cs      # Clase con operaciones matem�ticas
??? CalculadoraConsola/    # Aplicaci�n de consola
??? BibliotecaCalculadora.Tests/   # Pruebas unitarias
?   ??? LaCalculadoraDeberia.cs    # Tests con xUnit
??? .github/
    ??? agents.md                   # Este archivo
```

### Estilo de C�digo

1. **Nomenclatura**: 
   - Utilizar PascalCase para nombres de m�todos p�blicos y clases
   - Nombres descriptivos en espa�ol para m�todos
2. **Manejo de errores**: 
   - Lanzar excepciones espec�ficas para casos de error (ej: `ArgumentException`, `DivideByZeroException`)
   - Incluir mensajes descriptivos en espa�ol
3. **Documentaci�n**: Agregar comentarios XML para m�todos p�blicos siguiendo el formato est�ndar de C#

### Clase Principal: `Calculadora`

Ubicaci�n: `BibliotecaCalculadora\Calculadora.cs`

La clase `Calculadora` proporciona las siguientes operaciones:

- `Sumar(int a, int b)`: Suma dos n�meros enteros
- `Restar(int a, int b)`: Resta dos n�meros enteros
- `Multiplicar(int a, int b)`: Multiplica dos n�meros enteros
- `Dividir(int a, int b)`: Divide dos n�meros enteros (considerar divisi�n por cero)
- `Potencia(int a, int b)`: Calcula la potencia de un n�mero
- `RaizCuadrada(int a)`: Calcula la ra�z cuadrada de un n�mero (valida n�meros negativos)

## Gu�as para Generaci�n de C�digo

### Al agregar nuevos m�todos a la clase Calculadora

1. Usar tipos de retorno expl�citos
2. Validar entradas cuando sea necesario
3. Lanzar excepciones apropiadas con mensajes en espa�ol
4. Mantener la firma de m�todos simple (preferir tipos primitivos)
5. Usar conversiones expl�citas cuando se trabaje con `Math` (ej: `(int)Math.Pow()`)
6. Mantener consistencia con el estilo existente en el archivo

### Validaciones Importantes

- **Divisi�n por cero**: Siempre validar antes de dividir
- **Ra�ces cuadradas**: Validar que el n�mero no sea negativo
- **Potencias**: Considerar overflow para exponentes grandes
- **Operaciones con enteros**: Tener en cuenta truncamiento en conversiones

### Al crear pruebas unitarias

1. **Framework**: Usar xUnit como framework de pruebas
2. **Patr�n**: Seguir el patr�n AAA (Arrange, Act, Assert)
3. **Nomenclatura**: Usar el formato `NombreMetodo_Escenario_ResultadoEsperado`
4. **Cobertura**: Probar casos normales, l�mite y excepcionales
5. **Clase de tests**: Usar la convenci�n `LaCalculadoraDeberia` para la clase de tests
6. **Assertions**: Usar aserciones claras y descriptivas

### Ejemplo de m�todo con documentaci�n completa

```csharp
/// <summary>
/// Calcula el m�dulo de dos n�meros enteros.
/// </summary>
/// <param name="a">El dividendo</param>
/// <param name="b">El divisor</param>
/// <returns>El resto de la divisi�n entre a y b</returns>
/// <exception cref="DivideByZeroException">Se lanza cuando b es igual a cero</exception>
public int Modulo(int a, int b)
{
    if (b == 0)
        throw new DivideByZeroException("No se puede calcular el m�dulo con divisor cero.");
    
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

## Convenciones Espec�ficas del Proyecto

### Idioma
- Todos los mensajes de error y comentarios deben estar en **espa�ol**
- Los nombres de m�todos en espa�ol (Sumar, Restar, etc.)
- Los nombres de tests en espa�ol siguiendo el patr�n BDD

### Tipos de datos
- Preferir `int` sobre `double` o `decimal` para mantener consistencia
- Usar conversiones expl�citas cuando sea necesario (ej: `(int)Math.Pow()`)
- Retornar tipos concretos, no usar `var` en firmas de m�todos

### Manejo de excepciones
- Usar excepciones del sistema cuando sea apropiado:
  - `System.DivideByZeroException` para divisiones por cero
  - `System.ArgumentException` para argumentos inv�lidos
  - `System.OverflowException` para desbordamientos
- Incluir mensajes descriptivos en espa�ol

### Comentarios
- NO dejar c�digo comentado en commits finales
- Usar comentarios XML (`///`) para documentaci�n p�blica
- Evitar comentarios innecesarios que expliquen lo obvio

## Mejoras Futuras Sugeridas

Al trabajar con este proyecto, considera:

1. **Tipos num�ricos**:
   - Agregar soporte para operaciones con n�meros decimales (`decimal` o `double`)
   - Crear sobrecargas de m�todos para diferentes tipos num�ricos

2. **Operaciones adicionales**:
   - Implementar m�s operaciones matem�ticas (logaritmos, trigonometr�a, factorial)
   - Agregar operaciones con porcentajes
   - Implementar operaciones con fracciones

3. **Dise�o**:
   - Crear una interfaz `ICalculadora` para facilitar testing y extensibilidad
   - Considerar el patr�n Strategy para diferentes tipos de c�lculos
   - Implementar un sistema de historial de operaciones

4. **Validaciones**:
   - Agregar validaci�n de overflow/underflow
   - Implementar manejo de precisi�n en operaciones con decimales
   - Validar rangos de entrada para potencias y ra�ces

5. **Documentaci�n**:
   - Agregar documentaci�n XML completa para todos los m�todos p�blicos
   - Incluir ejemplos de uso en los comentarios XML
   - Documentar excepciones lanzadas

6. **Testing**:
   - Aumentar cobertura de pruebas
   - Agregar pruebas de integraci�n para la aplicaci�n de consola
   - Implementar pruebas parametrizadas con `[Theory]` y `[InlineData]`

## Directrices para GitHub Copilot

Cuando generes c�digo para este proyecto:

1. **Mant�n la consistencia**: Sigue el estilo y estructura existente
2. **Valida siempre**: Incluye validaciones apropiadas para cada operaci�n
3. **Documenta**: Agrega comentarios XML para m�todos p�blicos
4. **Prueba**: Genera tests correspondientes para cada nueva funcionalidad
5. **Espa�ol**: Usa nombres y mensajes en espa�ol
6. **Simplicidad**: Mant�n el c�digo simple y legible
7. **Tipos expl�citos**: No uses `var` en declaraciones de m�todos o cuando el tipo no sea obvio

## Referencias y Buenas Pr�cticas

- Seguir las convenciones de c�digo de C# (Microsoft Coding Conventions)
- Aplicar principios SOLID cuando sea apropiado
- Mantener m�todos cortos y con una sola responsabilidad
- Escribir c�digo auto-documentado complementado con comentarios XML
- Priorizar la legibilidad sobre la concisi�n excesiva

# Instrucciones para Pruebas Unitarias - BibliotecaCalculadora

## ?? Convenciones de Nomenclatura

### Nombres de M�todos de Prueba
```
[M�todo]_[Par�metros]_[ResultadoEsperado]
```

**Ejemplos:**
- `Al_Sumar_DosNumerosPositivos_DevolverSuma`
- `Dividir_DivisionPorCero_LanzaExcepcion`
- `Restar_VariosNumeros_DevuelveResta`

---

## ??? Estructura AAA (Arrange-Act-Assert)

Todas las pruebas deben seguir el patr�n AAA:

```csharp
[Fact]
public void NombreDelTest()
{
    // Arrange - Preparar los datos de prueba
    int a = 5;
  int b = 3;
    int esperado = 8;

    // Act - Ejecutar la acci�n a probar
    int resultado = _calculadora.Sumar(a, b);

    // Assert - Verificar el resultado
    Assert.Equal(esperado, resultado);
}
```

---

## ?? Sistema Bajo Prueba (SUT)

```csharp
/// <summary>
/// _calculadora es el SUT (System Under Test)
/// El sistema o componente que se est� probando
/// </summary>
private readonly Calculadora _calculadora;

public LaCalculadoraDeberia()
{
    // Arrange - Preparar el SUT para todas las pruebas
    _calculadora = new Calculadora();
}
```

---

## ?? Plan de Generaci�n de Tests Completos

### Paso 1: Analizar Cobertura Actual
- Identificar m�todos con cobertura insuficiente
- Revisar casos de prueba existentes
- Detectar escenarios no probados

### Paso 2: Dise�ar Casos de Prueba

#### 2.1 Casos B�sicos
- Operaciones con n�meros positivos
- Operaciones con n�meros negativos
- Operaciones con cero

#### 2.2 Casos Extremos
- L�mites de `int`:
  - `int.MaxValue` = `2,147,483,647`
- `int.MinValue` = `-2,147,483,648`

#### 2.3 Casos de Overflow
- Desbordamiento aritm�tico (overflow)
- Documentar comportamiento esperado

#### 2.4 Casos Mixtos
- Combinaciones positivo/negativo
- Operaciones con identidad (ej: `x + 0`, `x * 1`)

#### 2.5 Casos Excepcionales
- Divisi�n por cero
- Ra�z cuadrada de n�meros negativos
- Potencias negativas

### Paso 3: Implementar Tests con `[Theory]` y `[InlineData]`

#### Estructura Recomendada

```csharp
[Theory]
// Casos b�sicos
[InlineData(5, 3, 8)]
[InlineData(10, 7, 17)]

// Operaciones con cero
[InlineData(0, 0, 0)]
[InlineData(5, 0, 5)]
[InlineData(0, 5, 5)]

// N�meros negativos
[InlineData(-5, -3, -8)]
[InlineData(-10, -7, -17)]

// Casos mixtos
[InlineData(5, -3, 2)]
[InlineData(-10, 7, -3)]

// Casos extremos
[InlineData(int.MaxValue, 0, int.MaxValue)]
[InlineData(int.MinValue, 0, int.MinValue)]

// Casos de overflow (documentar)
// Nota: int.MaxValue + 1 produce overflow porque excede el rango de int.
// El resultado ser� int.MinValue debido al comportamiento de overflow en C#.
[InlineData(int.MaxValue, 1, int.MinValue)]
[InlineData(int.MinValue, -1, int.MaxValue)]
public void Metodo_VariosNumeros_DevuelveResultado(int a, int b, int esperado)
{
    // Arrange - Los datos vienen de InlineData
    
    // Act
    int resultado = _calculadora.Metodo(a, b);
    
    // Assert
    Assert.Equal(esperado, resultado);
}
```

### Paso 4: Documentar Comportamientos Especiales

#### Overflow en Operaciones Aritm�ticas

```csharp
// Nota: int.MaxValue - int.MinValue produce overflow porque el resultado excede el rango de int.
// El valor real es 2_147_483_647 - (-2_147_483_648) = 4_294_967_295,
// pero como int solo admite hasta 2_147_483_647,
// el resultado final es -1 debido al comportamiento de overflow en C#.
[InlineData(int.MaxValue, int.MinValue, -1)]
```

#### Pruebas de Excepciones

```csharp
[Fact]
public void Dividir_DivisionPorCero_LanzaExcepcion()
{
    // Arrange
    int a = 5;
    int b = 0;
    
  // Act & Assert
    var exception = Assert.Throws<DivideByZeroException>(() => _calculadora.Dividir(a, b));
    Assert.Equal("Attempted to divide by zero.", exception.Message);
}
```

### Paso 5: Ejecutar y Validar

1. Abrir __Test Explorer__ en Visual Studio
2. Ejecutar todos los tests: `Ctrl+R, A`
3. Verificar que todos pasen ?
4. Revisar cobertura de c�digo (opcional):
   - Usar herramientas como __Coverlet__ o __Fine Code Coverage__
   - Objetivo: >80% de cobertura

---

## ?? Checklist de Cobertura por M�todo

### ? Cobertura Completa (15+ tests)
- [x] `Restar` - 17 tests
- [x] `Dividir` - 19 tests (incluye excepci�n)
- [x] `Multiplicar` - 18 tests
- [x] `Potencia` - 19 tests

### ?? Cobertura M�nima (<5 tests)
- [ ] `Sumar` - 1 test (PENDIENTE AMPLIAR)

### ? Sin Cobertura
- [ ] `RaizCuadrada` - 0 tests (CR�TICO)

---

## ?? Plantilla para Nuevos Tests

```csharp
// Probar [OPERACI�N] con Theory
[Theory]
// Casos b�sicos
[InlineData(/* caso 1 */)]
[InlineData(/* caso 2 */)]

// Casos con cero
[InlineData(/* caso con cero */)]

// Casos extremos
[InlineData(int.MaxValue, /* ... */)]
[InlineData(int.MinValue, /* ... */)]

// Casos de n�meros negativos
[InlineData(/* n�meros negativos */)]

// Casos mixtos
[InlineData(/* positivo/negativo */)]

// Casos de overflow (documentar comportamiento)
// Nota: [Explicaci�n del overflow esperado]
[InlineData(/* caso overflow */)]
public void Metodo_VariosNumeros_DevuelveResultado(int a, int b, int esperado)
{
    // Arrange
    // Los datos de prueba se pasan como par�metros
    
    // Act
    int resultado = _calculadora.Metodo(a, b);
    
    // Assert
    Assert.Equal(esperado, resultado);
}
```

---

## ??? Herramientas y Comandos

### Ejecutar Tests
- **Todos los tests**: `Ctrl+R, A`
- **Tests del archivo actual**: `Ctrl+R, T`
- **Test bajo el cursor**: `Ctrl+R, Ctrl+T`

### Ver Resultados
- Abrir __Test Explorer__: `Ctrl+E, T`
- Ver detalles de fallos en __Output__ ? __Tests__

---

## ?? Recursos Adicionales

- [Documentaci�n de xUnit](https://xunit.net/)
- [Best Practices for Unit Testing](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices)
- [Teor�a de Testing: AAA Pattern](https://learn.microsoft.com/en-us/visualstudio/test/unit-test-basics)

---

## ?? Est�ndares del Proyecto

- **Framework de pruebas**: xUnit
- **Target Framework**: .NET 8
- **Lenguaje**: C# 12.0
- **Convenci�n de nombres**: PascalCase con guiones bajos para separar secciones
- **Cobertura objetivo**: >80%

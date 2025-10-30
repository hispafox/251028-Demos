namespace BibliotecaCalculadora.Tests
{
    public class LaCalculadoraDeberia
    {

        /// <summary>
        /// _calculadora es el SUT
        /// SUT = System Under Test
        /// </summary>
        private readonly Calculadora _calculadora; 

        public LaCalculadoraDeberia()
        {
            // El SUT es :
            // System Under Test (SUT) es el sistema o componente que se está probando
            // en un conjunto de pruebas unitarias.

            // Arrange - Preparamos los objetos necesarios para todas las pruebas
            _calculadora = new Calculadora(); // SUT 
        }

        /// <summary>
        /// Al_Sumar_DosNumerosPositivos_DevolverSuma
        /// -Nombre del método_Parametros de entrada_Resultado esperado
        /// </summary>
        [Fact] // Indica que este método es una prueba
        public void Al_Sumar_DosNumerosPositivos_DevolverSuma()
        {
            // Arrange - Preparamos los datos de prueba
            int a = 5;
            int b = 3;
            int esperado = 8;

            // Act - Ejecutamos la acción a probar
            int resultado = _calculadora.Sumar(a, b);

            // Assert - Verificamos el resultado
            Assert.Equal(esperado, resultado);
        }

        /// <summary>
        /// Pruebas parametrizadas para la operación Sumar con múltiples casos
        /// </summary>
        [Theory]
        [InlineData(5, 3, 8)]
        [InlineData(10, 7, 17)]
        [InlineData(20, 10, 30)]
        [InlineData(100, 200, 300)]
        // Pruebas con cero
        [InlineData(0, 0, 0)]
        [InlineData(0, 1, 1)]
        [InlineData(1, 0, 1)]
        [InlineData(5, 0, 5)]
        [InlineData(0, 10, 10)]
        // Pruebas con números negativos
        [InlineData(-5, -3, -8)]
        [InlineData(-10, -7, -17)]
        [InlineData(-20, -10, -30)]
        [InlineData(-1, -1, -2)]
        // Pruebas con números mixtos (positivo + negativo)
        [InlineData(5, -3, 2)]
        [InlineData(-10, 7, -3)]
        [InlineData(20, -10, 10)]
        [InlineData(-5, 3, -2)]
        [InlineData(15, -15, 0)]
        [InlineData(-8, 8, 0)]
        // Pruebas con casos extremos
        [InlineData(int.MaxValue, 0, int.MaxValue)]
        [InlineData(int.MinValue, 0, int.MinValue)]
        [InlineData(int.MaxValue, -1, int.MaxValue - 1)]
        [InlineData(int.MinValue, 1, int.MinValue + 1)]
        // Nota: int.MaxValue + int.MinValue = -1 debido a cómo están definidos estos valores
        [InlineData(int.MaxValue, int.MinValue, -1)]
        // Nota: int.MaxValue + 1 produce overflow y resulta en int.MinValue
        [InlineData(int.MaxValue, 1, int.MinValue)]
        // Nota: int.MinValue + (-1) produce overflow y resulta en int.MaxValue
        [InlineData(int.MinValue, -1, int.MaxValue)]
        public void Sumar_VariosNumeros_DevuelveSuma(int a, int b, int esperado)
        {
            // Arrange
            // Los datos de prueba se pasan como parámetros a la prueba

            // Act
            int resultado = _calculadora.Sumar(a, b);

            // Assert
            Assert.Equal(esperado, resultado);
        }

        // Prueba para la resta

        [Fact]
        public void Al_Restar_DosNumerosPositivos_DevolverResta()
        {
            // Arrange
            int a = 5;
            int b = 3;
            int esperado = 2;
            // Act
            int resultado = _calculadora.Restar(a, b);
            // Assert
            Assert.Equal(esperado, resultado);
        }

        // Implementar pruebas para restar varios numeros,
        // 5 - 3 devuelve 2, 10 -7 devuelve 3, etc.
        [Theory]
        [InlineData(5, 3, 2)]
        [InlineData(10, 7, 3)]
        [InlineData(20, 10, 10)]

        // Nota: int.MaxValue - int.MinValue en C# produce un desbordamiento aritmético (overflow) porque el resultado excede el rango de int.
        // El valor real es 2_147_483_647 - (-2_147_483_648) = 4_294_967_295, pero como int solo admite hasta 2_147_483_647,
        // el resultado final es -1 debido al comportamiento de overflow de los enteros en C#.
        [InlineData(int.MaxValue, int.MinValue, -1)]
        // Restar int.MinValue - int.MaxValue devuelve 1
        [InlineData(int.MinValue, int.MaxValue, 1)]
        // Restar 0 - 0 devuelve 0
        [InlineData(0, 0, 0)]
        // Restar 0 - 1 devuelve -1
        [InlineData(0, 1, -1)]
        // Restar 1 - 0 devuelve 1
        [InlineData(1, 0, 1)]
        // Comprueba casos extremos de resta
        [InlineData(int.MaxValue, 1, int.MaxValue - 1)]
        [InlineData(int.MinValue, -1, int.MinValue + 1)]
        // Comprueba casos de resta con números negativos
        [InlineData(-5, -3, -2)]
        [InlineData(-10, -7, -3)]
        [InlineData(-20, -10, -10)]
        // Comprueba casos de resta con números mixtos
        [InlineData(5, -3, 8)]
        [InlineData(-10, 7, -17)]
        [InlineData(20, -10, 30)]
        [InlineData(-5, 3, -8)]


        public void Restar_VariosNumeros_DevuelveResta(int a, int b, int esperado)
        {

            // Arrange
            // Los datos de prueba se pasan como parámetros a la prueba



            // Act
            int resultado = _calculadora.Restar(a, b);
            // Assert
            Assert.Equal(esperado, resultado);
        }

        // Probar la división por cero
        //[Fact]
        //public void Dividir_DivisionPorCero_LanzaExcepcion()
        //{
        //    // Arrange
        //    int a = 5;
        //    int b = 0;
        //    // Act & Assert
        //    //var exception = Assert.Throws<System.DivideByZeroException>(() => _calculadora.Dividir(a, b));
        //    //Assert.Equal("No se puede dividir por cero.", exception.Message);

        //    // Act 
        //    // Divide los dos numeros
        //    var resultado =  _calculadora.Dividir(a, b);


        //    // Assert
        //    Assert.Equal(2.5, resultado); // Verifica que el resultado sea 0
        //}

        // probar la división por cero
        [Fact]
        public void Dividir_DivisionPorCero_LanzaExcepcion()
        {
            // Arrange
            int a = 5;
            int b = 0;
            // Act & Assert
            var exception = Assert.Throws<System.DivideByZeroException>(() => _calculadora.Dividir(a, b));
            
            Assert.Equal("Attempted to divide by zero.", exception.Message);
        }

        // Probar la division con Theory
        [Theory]
        [InlineData(10, 2, 5)]
        [InlineData(20, 4, 5)]
        [InlineData(15, 3, 5)]
        [InlineData(0, 1, 0)]
        // Probar casos extremos de division
        [InlineData(int.MaxValue, 1, int.MaxValue)]
        //[InlineData(int.MinValue, -1, int.MinValue)]
        [InlineData(int.MaxValue, int.MaxValue, 1)]
        [InlineData(int.MinValue, int.MinValue, 1)]
        // Probar division con numeros negativos
        [InlineData(-10, -2, 5)]
        [InlineData(-20, 4, -5)]
        [InlineData(20, -4, -5)]
        [InlineData(-15, 3, -5)]
        // Probar division con numeros mixtos
        [InlineData(10, -2, -5)]
        [InlineData(-20, -4, 5)]
        [InlineData(-15, -3, 5)]
        [InlineData(0, -1, 0)]
        // Probar division cuyo resultado excede el rango de int
        //[InlineData(int.MaxValue, int.MinValue, -1)]


        //[InlineData(int.MinValue, int.MaxValue, -1)]
        public void Dividir_VariosNumeros_DevuelveDivision(int a, int b, int esperado)
        {
            // Arrange
            // Los datos de prueba se pasan como parámetros a la prueba
            // Act
            int resultado = _calculadora.Dividir(a, b);
            // Assert
            Assert.Equal(esperado, resultado);
        }

        // Probar la multiplicación con Theory
        [Theory]
        [InlineData(2, 3, 6)]
            
        [InlineData(5, 4, 20)]
        [InlineData(0, 1, 0)]
        [InlineData(1, 0, 0)]
        [InlineData(0, 0, 0)]
        // Probar casos extremos de multiplicación
        [InlineData(int.MaxValue, 1, int.MaxValue)]
        [InlineData(int.MinValue, -1, int.MinValue)]
        [InlineData(int.MaxValue, int.MaxValue, 1)]
        [InlineData(int.MinValue, int.MinValue, 0)]
        // Probar multiplicación con números negativos
        [InlineData(-2, -3, 6)]
        [InlineData(-5, 4, -20)]
        [InlineData(5, -4, -20)]
        [InlineData(-3, 2, -6)]
        // Probar multiplicación con números mixtos
        [InlineData(2, -3, -6)]
        [InlineData(-5, -4, 20)]
        [InlineData(-3, -2, 6)]
        [InlineData(0, -1, 0)]
        // Probar multiplicación cuyo resultado excede el rango de int
        //[InlineData(int.MaxValue, int.MinValue, -1)]
        //[InlineData(int.MinValue, int.MaxValue, -1)]
        public void Multiplicar_VariosNumeros_DevuelveMultiplicacion(int a, int b, int esperado)
        {
            // Arrange
            // Los datos de prueba se pasan como parámetros a la prueba
            // Act
            int resultado = _calculadora.Multiplicar(a, b);
            // Assert
            Assert.Equal(esperado, resultado);
        }

        // Probar la potencia con Theory
        [Theory]
        [InlineData(2, 3, 8)]
        [InlineData(5, 4, 625)]
        [InlineData(0, 1, 0)]
        [InlineData(1, 0, 1)]
        [InlineData(0, 0, 1)] // 0^0 se considera 1 en matemáticas
        // Probar casos extremos de potencia
        [InlineData(int.MaxValue, 1, int.MaxValue)]
        [InlineData(int.MinValue, -1, 0)] // Potencia negativa de un número negativo
            
        [InlineData(int.MaxValue, int.MaxValue, int.MinValue)] // Potencia de un número positivo elevado a sí mismo
        [InlineData(int.MinValue, int.MinValue, 0)] // Potencia de un número negativo elevado a sí mismo
        // Probar potencia con números negativos
        [InlineData(-2, -3, 0)] // Potencia negativa de un número negativo
        [InlineData(-5, 4, 625)] // Potencia par de un número negativo
        [InlineData(5, -4, 0)] // Potencia negativa de un número positivo
        [InlineData(-3, 2, 9)] // Potencia par de un número negativo
        // Probar potencia con números mixtos
        [InlineData(2, -3, 0)] // Potencia negativa de un número positivo
        [InlineData(-5, -4, 0)] // Potencia negativa de un número negativo
        [InlineData(-3, -2, 0)] // Potencia negativa de un número negativo
        [InlineData(0, -1, int.MinValue)] // Potencia negativa de cero
        // Probar potencia cuyo resultado excede el rango de int
        //[InlineData(int.MaxValue, int.MinValue, -1)]

        //[InlineData(int.MinValue, int.MaxValue, -1)]
        public void Potencia_VariosNumeros_DevuelvePotencia(int a, int b, int esperado)
        {
            // Arrange
            // Los datos de prueba se pasan como parámetros a la prueba
            // Act
            int resultado = _calculadora.Potencia(a, b);
            // Assert
            Assert.Equal(esperado, resultado);
        }

        // Probar la raíz cuadrada con números negativos que lanzan excepción
        [Fact]
        public void RaizCuadrada_NumeroNegativo_LanzaExcepcion()
        {
            // Arrange
            int a = -1;
            // Act & Assert
            var exception = Assert.Throws<System.ArgumentException>(() => _calculadora.RaizCuadrada(a));
            Assert.Equal("No se puede calcular la raíz cuadrada de un número negativo.", exception.Message);
        }

        // Probar la raíz cuadrada con varios números negativos
        [Theory]
        [InlineData(-1)]
        [InlineData(-9)]
        [InlineData(-16)]
        [InlineData(-100)]
        [InlineData(int.MinValue)]
        public void RaizCuadrada_VariosNumerosNegativos_LanzaExcepcion(int a)
        {
            // Arrange
            // Los datos de prueba se pasan como parámetros a la prueba
            // Act & Assert
            var exception = Assert.Throws<System.ArgumentException>(() => _calculadora.RaizCuadrada(a));
            Assert.Equal("No se puede calcular la raíz cuadrada de un número negativo.", exception.Message);
        }

        // Probar la raíz cuadrada con Theory
        [Theory]
        [InlineData(0, 0)] // Raíz cuadrada de 0 es 0
        [InlineData(1, 1)] // Raíz cuadrada de 1 es 1
        [InlineData(4, 2)] // Raíz cuadrada de 4 es 2
        [InlineData(9, 3)] // Raíz cuadrada de 9 es 3
        [InlineData(16, 4)] // Raíz cuadrada de 16 es 4
        [InlineData(25, 5)] // Raíz cuadrada de 25 es 5
        [InlineData(36, 6)] // Raíz cuadrada de 36 es 6
        [InlineData(49, 7)] // Raíz cuadrada de 49 es 7
        [InlineData(64, 8)] // Raíz cuadrada de 64 es 8
        [InlineData(81, 9)] // Raíz cuadrada de 81 es 9
        [InlineData(100, 10)] // Raíz cuadrada de 100 es 10
        [InlineData(144, 12)] // Raíz cuadrada de 144 es 12
        [InlineData(225, 15)] // Raíz cuadrada de 225 es 15
        [InlineData(400, 20)] // Raíz cuadrada de 400 es 20
        [InlineData(625, 25)] // Raíz cuadrada de 625 es 25
        [InlineData(10000, 100)] // Raíz cuadrada de 10000 es 100
        // Pruebas con números que no son cuadrados perfectos (resultado truncado)
        [InlineData(2, 1)] // Raíz cuadrada de 2 es aproximadamente 1.414, truncado a 1
        [InlineData(3, 1)] // Raíz cuadrada de 3 es aproximadamente 1.732, truncado a 1
        [InlineData(5, 2)] // Raíz cuadrada de 5 es aproximadamente 2.236, truncado a 2
        [InlineData(8, 2)] // Raíz cuadrada de 8 es aproximadamente 2.828, truncado a 2
        [InlineData(10, 3)] // Raíz cuadrada de 10 es aproximadamente 3.162, truncado a 3
        [InlineData(15, 3)] // Raíz cuadrada de 15 es aproximadamente 3.872, truncado a 3
        [InlineData(20, 4)] // Raíz cuadrada de 20 es aproximadamente 4.472, truncado a 4
        [InlineData(50, 7)] // Raíz cuadrada de 50 es aproximadamente 7.071, truncado a 7
        [InlineData(99, 9)] // Raíz cuadrada de 99 es aproximadamente 9.949, truncado a 9
        [InlineData(101, 10)] // Raíz cuadrada de 101 es aproximadamente 10.049, truncado a 10
        // Probar casos extremos
        [InlineData(int.MaxValue, 46340)] // Raíz cuadrada de int.MaxValue (2147483647) es aproximadamente 46340.95
        public void RaizCuadrada_VariosNumeros_DevuelveRaizCuadrada(int a, int esperado)
        {
            // Arrange
            // Los datos de prueba se pasan como parámetros a la prueba
            // Act
            int resultado = _calculadora.RaizCuadrada(a);
            // Assert
            Assert.Equal(esperado, resultado);
        }

// ========================================
// PRUEBAS PARA FACTORIAL
        // ========================================

        /// <summary>
  /// Prueba b?sica para verificar que Factorial calcula correctamente
        /// </summary>
        [Fact]
        public void Factorial_CincoDaResultadoCientoVeinte()
        {
// Arrange
            int n = 5;
   long esperado = 120;

      // Act
            long resultado = _calculadora.Factorial(n);

       // Assert
    Assert.Equal(esperado, resultado);
     }

        /// <summary>
        /// Pruebas parametrizadas para Factorial con m?ltiples casos v?lidos
        /// </summary>
        [Theory]
        // Casos b?sicos
        [InlineData(0, 1)]    // 0! = 1 (definici?n matem?tica)
 [InlineData(1, 1)] // 1! = 1
        [InlineData(2, 2)]          // 2! = 2
        [InlineData(3, 6)]      // 3! = 6
        [InlineData(4, 24)]   // 4! = 24
        [InlineData(5, 120)]       // 5! = 120
        [InlineData(6, 720)]      // 6! = 720
 [InlineData(7, 5040)]           // 7! = 5,040
        [InlineData(8, 40320)]          // 8! = 40,320
        [InlineData(9, 362880)]    // 9! = 362,880
        [InlineData(10, 3628800)]       // 10! = 3,628,800
        
        // Casos intermedios
        [InlineData(11, 39916800)]      // 11! = 39,916,800
        [InlineData(12, 479001600)]     // 12! = 479,001,600
        [InlineData(13, 6227020800)]    // 13! = 6,227,020,800 (requiere long)
[InlineData(14, 87178291200)]   // 14! = 87,178,291,200
        [InlineData(15, 1307674368000)] // 15! = 1,307,674,368,000
        
        // Casos extremos - valores grandes v?lidos
        [InlineData(16, 20922789888000)]        // 16! = 20,922,789,888,000
        [InlineData(17, 355687428096000)]       // 17! = 355,687,428,096,000
        [InlineData(18, 6402373705728000)]      // 18! = 6,402,373,705,728,000
 [InlineData(19, 121645100408832000)]  // 19! = 121,645,100,408,832,000
        [InlineData(20, 2432902008176640000)]   // 20! = 2,432,902,008,176,640,000 (m?ximo v?lido para long)
  public void Factorial_VariosNumeros_DevuelveResultado(int n, long esperado)
        {
   // Arrange
            // Los datos vienen de InlineData

            // Act
   long resultado = _calculadora.Factorial(n);

   // Assert
          Assert.Equal(esperado, resultado);
        }

        /// <summary>
        /// Prueba que Factorial lanza excepci?n con n?mero negativo
        /// </summary>
        [Fact]
        public void Factorial_NumeroNegativo_LanzaExcepcion()
        {
    // Arrange
       int n = -5;

// Act & Assert
var exception = Assert.Throws<System.ArgumentException>(() => _calculadora.Factorial(n));
  Assert.Contains("negativo", exception.Message);
        }

  /// <summary>
        /// Prueba que Factorial lanza excepci?n con varios n?meros negativos
    /// </summary>
    [Theory]
      [InlineData(-1)]
     [InlineData(-5)]
        [InlineData(-10)]
        [InlineData(-100)]
  [InlineData(int.MinValue)]
        public void Factorial_VariosNumerosNegativos_LanzaExcepcion(int n)
      {
            // Arrange
      // Los datos vienen de InlineData

// Act & Assert
     var exception = Assert.Throws<System.ArgumentException>(() => _calculadora.Factorial(n));
       Assert.Contains("negativo", exception.Message);
   }

        /// <summary>
        /// Prueba que Factorial lanza excepci?n cuando n es mayor que 20
        /// </summary>
 [Fact]
 public void Factorial_NumeroMayorA20_LanzaExcepcion()
        {
    // Arrange
     int n = 21;

 // Act & Assert
            var exception = Assert.Throws<System.ArgumentException>(() => _calculadora.Factorial(n));
  Assert.Contains("20", exception.Message);
          Assert.Contains("desbordamiento", exception.Message);
        }

        /// <summary>
        /// Prueba que Factorial lanza excepci?n con varios n?meros mayores a 20
        /// </summary>
        [Theory]
    [InlineData(21)]
        [InlineData(25)]
        [InlineData(50)]
        [InlineData(100)]
        [InlineData(1000)]
        [InlineData(int.MaxValue)]
     public void Factorial_VariosNumerosMayoresA20_LanzaExcepcion(int n)
        {
            // Arrange
  // Los datos vienen de InlineData

          // Act & Assert
  var exception = Assert.Throws<System.ArgumentException>(() => _calculadora.Factorial(n));
Assert.Contains("20", exception.Message);
        }

    }
}